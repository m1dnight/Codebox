using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CodeBox.Domain.Abstract;
using CodeBox.Domain.Concrete.ORM;

namespace CodeBox.Domain.Concrete
{
    public class EFUserRepository : IUserRepository
    {
        private CodeBoxEntities context = DataContextFactory.CodeBoxEntities;

        public User GetUserByUsername(string username, string pass)
        {
            return context.Users.SingleOrDefault(u => u.Username == username && u.Password == pass);
        }

        public User GetUserByUsername(string username)
        {
            return context.Users.SingleOrDefault(u => u.Username == username);
        }

        public IQueryable<User> Users
        {
            get { return context.Users; }
        }

        public IEnumerable<SelectListItem> UserOptions
        {
            get
            {
                var selectlist = new List<SelectListItem>();
                selectlist.AddRange(context.Users.ToList().Select(x => new SelectListItem
                                                                           {
                                                                               Value = x.UserId.ToString(),
                                                                               Text = x.Username
                                                                           }).ToList());
                return selectlist;
            }
        }

        public int RegisterUser(User user)
        {
            context.Users.AddObject(user);
            context.SaveChanges();
            return user.UserId;
        }

        public string GetUserNameByMail(string mail)
        {
            var user = context.Users.FirstOrDefault(u => u.Mail == mail);
            if (user != null)
                return user.Username;
            return null;
        }

        public void UpdateUser(User user)
        {
            //All the following dates get set to DateTime.MinValue to work properly,
            //Because SQL supports nullables, they are set to null before the user gets updated
            if (user.LastSeen == DateTime.MinValue)
                user.LastSeen = null;

            if (user.lastLoginDate == DateTime.MinValue)
                user.lastLoginDate = null;

            if (user.LastPasswordChangeDate == DateTime.MinValue)
                user.LastPasswordChangeDate = null;

            if (user.LastLockOutDate == DateTime.MinValue)
                user.LastLockOutDate = null;


            //The image data is not sent back and forth between the client and the server
            //If the following code is not executed the image data is overwritten with null each time ther eis an update
            //So, detach the user object, fetch a fresh object to see if the user has an image
            //If so, put it in the detached object
            //Detach the newest one, and attach the old one back with imagedata
            //Update

            

            if (user.ImageData == null)
            {
                context.ObjectStateManager.ChangeObjectState(user, EntityState.Detached);

                var dbUser = context.Users.FirstOrDefault(u => u.Username == user.Username);
                if (dbUser != null && dbUser.ImageData != null)
                {
                    user.ImageData = dbUser.ImageData;
                    user.ImageMimeType = dbUser.ImageMimeType;
                }
                context.ObjectStateManager.ChangeObjectState(dbUser, EntityState.Detached);
                context.Users.Attach(user);
            }

            user.LastActivityDate = DateTime.Now;
            user.LastSeen = DateTime.Now;

            context.ObjectStateManager.ChangeObjectState(user, EntityState.Modified);
            context.SaveChanges();
        }

        public void AddUserToRole(User u, string role)
        {
            context.Users.Attach(u);
            var roleFromDb = context.Roles.FirstOrDefault(r => r.Name == role);

            //If there is no role for this name
            //If the user is already enrolled for this role
            if (roleFromDb == null || u.Roles.Contains(roleFromDb)) return;

            //The user is not in the role, and it exists
            u.Roles.Add(context.Roles.FirstOrDefault(r => r.Name == role));
            context.SaveChanges();
        }

        public void RemoveRoleFromUser(User user, string role)
        {
            context.Users.Attach(user);
            var roleFromDb = context.Roles.FirstOrDefault(r => r.Name == role);

            //if the role does not exists, return
            if (roleFromDb == null) return;
            if (user.Roles.Contains(roleFromDb))
            {
                user.Roles.Remove(roleFromDb);
                context.SaveChanges();
            }
        }

        public void DeleteUser(User user)
        {
            context.Users.DeleteObject(user);
            context.SaveChanges();
        }

        public void UpdateRolesForUser(User user, int[] roleIds)
        {
            //Should not be detached, but just to be sure
            if(user.EntityState == EntityState.Detached)
                context.Users.Attach(user);

            var roleList = roleIds.Select
                (id => context.Roles.FirstOrDefault(x => x.RoleId == id))
                .Where(roleFromDb => roleFromDb != null)
                .ToList();
            user.Roles.Clear();
            foreach (var r in roleList)
                user.Roles.Add(r);

            context.SaveChanges();
        }

        public void ChangeLockedStatus(User user, bool locked)
        {
            context.Users.Attach(user);
            user.LockedOut = locked;
            context.SaveChanges();
        }

        public bool IsUserLockedOut(string username)
        {
            var user = context.Users.FirstOrDefault(u => u.Username == username);
            return user != null && user.LockedOut;
        }

    }
}
