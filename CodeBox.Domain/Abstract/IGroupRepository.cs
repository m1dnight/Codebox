using System.Linq;
using CodeBox.Domain.Concrete.ORM;

namespace CodeBox.Domain.Abstract
{
    public interface IGroupRepository
    {
        IQueryable<Group> Groups { get; }

        void SaveGroup(Group group);
        void DeleteGroup(Group group);

        void AddGroupAdmin(Group group, User user);
        void RemoveGroupAdmin(Group group, User user);

        void AddUserToGroup(User user, Group group);
        void AddUserByMail(string mail, Group group);
        void DeleteUserToGroup(User user, Group group);

        IQueryable<Group> GetGroupsForUser(User user);
        IQueryable<Group> GetGroupsForUsername(string username);

        void AddSnippetToGroup(int groupId, int snippetId);
        void UpdateGroupsWithSnippet(int?[] selectedGroupId, Snippet snippet);
    }
}
