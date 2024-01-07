using System;
using Xunit;
using FleetManagement.Common.Exceptions;
using FleetManagement.Common.Models;

namespace FleetManagement.xUnitTest.UnitTestFleetManagement
{
    public class UnitTestUser
    {
        [Fact]
        public void CreateValidUser()
        {
            var role = new Role(1, "Admin");
            var user = new User("StrongPassword1", "john.doe", role);
            Assert.NotNull(user);
        }

        [Fact]
        public void CreateValidUserWithDriver()
        {
            var role = new Role(2, "Driver");
            var driver = new Driver("Doe", "John", new Address(1, "Main Street", "123", "2000", "Antwerp"), new DateOnly(1980, 1, 1), "90010112345", new List<DriverLicense>());
            var user = new User("SecurePass123", "john.driver", role, driver);
            Assert.NotNull(user);
        }

        [Fact]
        public void VerifyPassword()
        {
            var role = new Role(1, "Admin");
            var user = new User("StrongPassword1", "john.doe", role);
            var result = user.VerifyPassword("StrongPassword1");
            Assert.True(result);
        }

        [Fact]
        public void CreateUserWithWeakPassword()
        {
            var role = new Role(1, "Admin");
            Assert.Throws<ValidationException>(() =>
                new User("weakpass", "john.doe", role));
        }

        [Fact]
        public void CreateUserWithEmptyUsername()
        {
            var role = new Role(1, "Admin");
            Assert.Throws<ValidationException>(() =>
                new User("StrongPassword1", "", role));
        }

        [Fact]
        public void CreateUserWithNullRole()
        {
            var user = new User("SecurePass123", "john.admin", null);
            Assert.NotNull(user);
            Assert.Null(user.Role); // Ensure that the Role property is null
        }


        [Fact]
        public void CreateUserWithEmptyPassword()
        {
            var role = new Role(1, "Admin");
            Assert.Throws<ValidationException>(() =>
                new User("", "john.doe", role));
        }

        [Fact]
        public void VerifyIncorrectPassword()
        {
            var role = new Role(1, "Admin");
            var user = new User("StrongPassword1", "john.doe", role);
            var result = user.VerifyPassword("WrongPassword123");
            Assert.False(result);
        }
    }
}
