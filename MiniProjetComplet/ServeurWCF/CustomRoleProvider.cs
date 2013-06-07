using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using LibrairieServeur;

namespace ServeurWCF
{
    class CustomRoleProvider : RoleProvider
    {
        private GestionBDD gest = new GestionBDD(new DataBase());

        public override String ApplicationName { get; set; }
        public override String[] GetRolesForUser(String username)
        {
            return new String[] { gest.getRole(username) };
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