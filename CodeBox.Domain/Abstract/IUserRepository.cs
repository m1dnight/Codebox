using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CodeBox.Domain.Concrete.ORM;

namespace CodeBox.Domain.Abstract
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username, string password);
        User GetUserByUsername(string username);
        IQueryable<User> Users { get; }
        IEnumerable<SelectListItem> UserOptions { get; }
        int RegisterUser(User user);
        string GetUserNameByMail(string username);
        void UpdateUser(User user);
        void AddUserToRole(User user, string role);
        void RemoveRoleFromUser(User user, string role);
        void DeleteUser(User user);
        void UpdateRolesForUser(User user, int[] roleIds);
        void ChangeLockedStatus(User user, bool locked);
        bool IsUserLockedOut(string username);
    }
}
