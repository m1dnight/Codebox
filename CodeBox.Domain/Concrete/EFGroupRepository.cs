using System.Collections.Generic;
using System.Data;
using System.Linq;
using CodeBox.Domain.Abstract;
using CodeBox.Domain.Concrete.ORM;

namespace CodeBox.Domain.Concrete
{
    public class EFGroupRepository : IGroupRepository
    {

        private CodeBoxEntities context = DataContextFactory.CodeBoxEntities;

        public IQueryable<Group> Groups
        {
            get { return context.Groups; }
        }

        public void SaveGroup(Group group)
        {
            if (group.Id == 0)
            {
                context.Groups.AddObject(group);
            }
            else
            {
                if (group.ImageData == null)
                {
                    context.ObjectStateManager.ChangeObjectState(group, EntityState.Detached);

                    var dbGroup = context.Groups.FirstOrDefault(g => g.Id == group.Id);
                    if (dbGroup != null && dbGroup.ImageData != null)
                    {
                        group.ImageData = dbGroup.ImageData;
                        group.ImageMimeType = dbGroup.ImageMimeType;
                    }
                    if (dbGroup != null) context.ObjectStateManager.ChangeObjectState(dbGroup, EntityState.Detached);
                    context.Groups.Attach(group);
                }
                context.Groups.Attach(group);
                context.ObjectStateManager.ChangeObjectState(group, EntityState.Modified);
            }
            context.SaveChanges();
        }

        public void DeleteGroup(Group group)
        {
            context.Groups.DeleteObject(group);
            context.SaveChanges();
        }

        public void AddGroupAdmin(Group @group, User user)
        {
            var newAdmin = new GroupAdmin {User = user};

            group.GroupAdmins.Add(newAdmin);
            context.SaveChanges();

        }

        public void RemoveGroupAdmin(Group @group, User user)
        {
            var admin = group.GroupAdmins.FirstOrDefault(ga => ga.User.UserId == user.UserId);
            group.GroupAdmins.Remove(admin);
            context.SaveChanges();
        }

        public void AddUserToGroup(User user, Group @group)
        {
            group.Users.Add(user);
            context.SaveChanges();
        }

        public void AddUserByMail(string mail, Group @group)
        {
            var user = context.Users.FirstOrDefault(u => u.Mail == mail);
            if (user != null)
            {
                group.Users.Add(user);
                context.SaveChanges();
            }
        }

        public void DeleteUserToGroup(User user, Group @group)
        {
            group.Users.Remove(user);
            if (group.GroupAdmins.Any(a => a.User.UserId == user.UserId))
            {
                var admin = group.GroupAdmins.FirstOrDefault(x => x.User.Username == user.Username);
                context.GroupAdmins.DeleteObject(admin);
            }

            if (group.Users.Count == 0)
            {
                context.Groups.DeleteObject(group);
            }

            if (group.GroupAdmins.Count == 0 && group.Users.Count > 0)
            {
                var useridwithmostsnippets = group.Users.First().UserId;

                foreach (var u in @group.Users.Where(u => @group.Snippets.Count(s => s.UserId == u .UserId) > @group.Snippets.Count(s => s.UserId == useridwithmostsnippets)))
                {
                    useridwithmostsnippets = user.UserId;
                }
                var ga = new GroupAdmin {User = context.Users.FirstOrDefault(u => u.UserId == useridwithmostsnippets)};
                group.GroupAdmins.Add(ga);
            }
            context.SaveChanges();
        }

        public IQueryable<Group> GetGroupsForUser(User user)
        {
            return user.Groups.AsQueryable();
        }

        public IQueryable<Group> GetGroupsForUsername(string username)
        {
            var user = context.Users.FirstOrDefault(u => u.Username == username);
            return user.Groups.AsQueryable();
        }

        public void AddSnippetToGroup(int groupId, int snippetId)
        {
            var snippet = context.Snippets.FirstOrDefault(s => s.SnippetId == snippetId);
            var group = context.Groups.FirstOrDefault(g => g.Id == groupId);

            if (snippet != null && group != null)
            {
                if (group.Snippets.All(s => s.SnippetId != snippetId))
                {
                    group.Snippets.Add(snippet);
                    context.SaveChanges();
                }
            }
        }

        public void UpdateGroupsWithSnippet(int?[] selectedGroupId, Snippet snippet)
        {
            //Get all the groups that contain this snippet
            var snippetFromDb = context.Snippets.FirstOrDefault(s => s.SnippetId == snippet.SnippetId);
            var groups = context.Groups.Where(g => g.Snippets.Any(s => s.SnippetId == snippet.SnippetId)).ToList();
            foreach (var g in groups)
            {
                g.Snippets.Remove(snippetFromDb);
            }
            context.SaveChanges();

            foreach (int gid in selectedGroupId)
            {
                var groupFromDb = context.Groups.FirstOrDefault(g => g.Id == gid);
                if(groupFromDb != null)
                    snippetFromDb.Groups.Add(groupFromDb);
            }
            context.SaveChanges();
        }
    }
}
