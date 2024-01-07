using System;
using FleetManagement.Common.Exceptions;
using FleetManagement.Common.Models;

namespace FleetManagement.xUnitTest.UnitTestFleetManagement
{
    public class UnitTestRole
    {
        // Valid test cases

        [Fact]
        public void CreateRoleWithValidData()
        {
            var role = new Role(1, "Administrator");
            Assert.NotNull(role);
            Assert.Equal(1, role.RoleID);
            Assert.Equal("Administrator", role.RoleName);
        }

        // Invalid test cases

        [Fact]
        public void CreateRoleWithInvalidID()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Role(0, "InvalidRole"));
        }

        [Fact]
        public void CreateRoleWithInvalidRoleName()
        {
            Assert.Throws<ValidationException>(() =>
                new Role(2, null));
        }

        [Fact]
        public void CreateRoleWithWhitespaceRoleName()
        {
            Assert.Throws<ValidationException>(() =>
                new Role(3, "   "));
        }

        [Fact]
        public void CreateRoleWithInvalidIDAndRoleName()
        {
            Assert.ThrowsAny<Exception>(() =>
                new Role(0, null));
        }
    }

}

