using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
namespace WCFSecurite
{
    class CustomRoleProvider : RoleProvider
    {
        public override String ApplicationName { get; set; }
        public override String[] GetRolesForUser(String username)
        {
            // La classe XXXXXX peut par exemple définir un accès à une base de données où seraient stockés les rôles pour chaque utilisateur.
            return new String[] { XXXXXX.getRoles(username) };
        }
        public override bool IsUserInRole(String username, String roleName)
        {
            return GetRolesForUser(username).Contains(roleName);
        }
        public override void AddUsersToRoles(String[] usernames, String[]
        roleNames)
        {
            throw new Exception("Unable to perform this action");
        }
        public override void CreateRole(String roleName)
        {
            throw new Exception("Unable to perform this action");
        }
        public override bool DeleteRole(String roleName, bool throwOnPopulatedRole)
        {
            throw new Exception("Unable to perform this action");
        }
        public override String[] FindUsersInRole(String roleName, String
        usernameToMatch)
        {
            throw new Exception("Unable to perform this action");
        }
        public override String[] GetAllRoles()
        {
            throw new Exception("Unable to perform this action");
        }
        public override String[] GetUsersInRole(String roleName)
        {
            throw new Exception("Unable to perform this action");
        }
        public override void RemoveUsersFromRoles(String[] usernames, String[]
        roleNames)
        {
            throw new Exception("Unable to perform this action");
        }
        public override bool RoleExists(String roleName)
        {
            throw new Exception("Unable to perform this action");
        }
    }
}