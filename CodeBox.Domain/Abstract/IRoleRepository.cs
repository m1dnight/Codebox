using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CodeBox.Domain.Concrete.ORM;

namespace CodeBox.Domain.Abstract
{
    public interface IRoleRepository
    {
        string[] GetRolesForUser(string username);
        bool IsUserInRole(string username, string rolename);
        Role GetRoleByName(string roleName);
        IEnumerable<SelectListItem> RoleOptions { get; }
        IQueryable<Role> Roles { get; }
        void SaveRole(Role role);
        void DeleteRole(Role role);
    }
}
