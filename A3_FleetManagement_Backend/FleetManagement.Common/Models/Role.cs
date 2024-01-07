using System;
using FleetManagement.Common.Exceptions;

namespace FleetManagement.Common.Models
{
    public class Role
    {
        private string _roleName;

        public Role(int roleID, string roleName)
        {
            ValidateID(roleID, "RoleID");
            ValidateRoleName(roleName);

            RoleID = roleID;
            RoleName = roleName;
        }

        public int RoleID { get; private set; }

        public string RoleName
        {
            get => _roleName;
            private set => _roleName = ValidateRoleName(value);
        }

        private void ValidateID(int id, string propertyName)
        {
            if (id < 1 || id > 5)
            {
                throw new ArgumentOutOfRangeException(propertyName, $"{propertyName} must be between 1 and 5.");
            }
        }

        private string ValidateRoleName(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ValidationException("Role name cannot be null or consist of only whitespace.");
            }

            return roleName;
        }
    }
}
