using System.Web.Security;
using BdJobsOnline.BdJobsDatabase;
using System.Linq;

namespace BdJobsOnline.Memberships
{
    public class BdJobsRoleProvider : RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            var entities = new BdJobsEntities();
            var user = entities.Users.First(u => u.UserName == username);
            var usersRole = user.Role;

            return usersRole.RoleName == roleName;
        }

        public override string[] GetRolesForUser(string username)
        {
            var entities = new BdJobsEntities();
            var user = entities.Users.First(u => u.UserName == username);
            var usersRole = user.Role;

            return new[] { usersRole.RoleName };
        }

        public override void CreateRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new System.NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new System.NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new System.NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}