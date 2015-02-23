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
    public class EFRoleRepository : IRoleRepository
    {
        private CodeBoxEntities context = DataContextFactory.CodeBoxEntities;

        public string[] GetRolesForUser(string username)
        {
            var rolesList = new List<string>();
            var cx = new CodeBoxEntities();
            User user = cx.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                foreach (var r in user.Roles)
                {
                    rolesList.Add(r.Name);
                }
                return rolesList.ToArray();
            }
            return null;
        }

        public bool IsUserInRole(string username, string rolename)
        {
            Role ur = context.Roles.FirstOrDefault(r => r.Name == rolename);

            if (ur != null)
            {
                return ur.Users.Any(u => u.Username == username);
            }
            return false;
        }

        public Role GetRoleByName(string roleName)
        {
            var role = context.Roles.FirstOrDefault(r => r.Name == roleName);
            context.Roles.Detach(role);
            return role;
        }

        public IEnumerable<SelectListItem> RoleOptions
        {
            get
            {
                var selectlist = new List<SelectListItem>();
                selectlist.AddRange(
                    context.Roles.ToList().Select(x => new SelectListItem
                                                           {
                                                               Text = x.Name,
                                                               Value = x.RoleId.ToString()
                                                           }).ToList());
                return selectlist;
            }
        }

        public IQueryable<Role> Roles
        {
            get { return context.Roles; }
        }

        public void SaveRole(Role role)
        {
            if (role.RoleId == 0)
            {
                context.Roles.AddObject(role);
            }
            else
            {
                if(role.EntityState == EntityState.Detached)
                    context.Roles.Attach(role);
                context.ObjectStateManager.ChangeObjectState(role, EntityState.Modified);
            }
            context.SaveChanges();
        }

        public void DeleteRole(Role role)
        {
            context.Roles.DeleteObject(role);
            context.SaveChanges();
        }
    }
}
