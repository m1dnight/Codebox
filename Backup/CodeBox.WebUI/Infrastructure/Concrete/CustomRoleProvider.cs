using System;
using System.Web.Security;
using CodeBox.Domain.Abstract;
using Ninject;

namespace CodeBox.WebUI.Infrastructure.Concrete
{
    public class CustomRoleProvider : RoleProvider
    {
        [Inject]
        public IRoleRepository RoleRepo { get; set; }


        public override bool IsUserInRole(string username, string roleName)
        {
            return RoleRepo.IsUserInRole(username, roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            var list = RoleRepo.GetRolesForUser(username);
            return list;
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}