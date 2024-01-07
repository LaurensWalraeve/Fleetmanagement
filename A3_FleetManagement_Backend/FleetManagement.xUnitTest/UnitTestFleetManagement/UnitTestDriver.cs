using System;
using System.Collections.Generic;
using FleetManagement.Common.Exceptions;
using FleetManagement.Common.Models;
using Xunit;

namespace FleetManagement.xUnitTest.UnitTestFleetManagement
{
    public class UnitTestDriver
    {
        private static DateOnly? GetNullableDateOnly()
        {
            return null;
        }

        [Fact]
        public void CreateDriverWithValidData()
        {
            // Arrange
            var lastName = "Doe";
            var firstName = "John";
            var address = new Address(1,"Street 1","1", "City", "12345");
            var birthDate = new DateOnly(1990, 1, 1);
            var ssn = "90010112345";
            var licenses = new List<DriverLicense> { new DriverLicense() };

            // Act & Assert
            Assert.NotNull(new Driver(lastName, firstName, address, birthDate, ssn, licenses));
        }

        [Fact]
        public void CreateDriverWithInvalidLastName()
        {
            // Arrange
            var lastName = "  "; // Invalid: Empty
            var firstName = "John";
            var address = new Address(1, "Street 1", "1", "City", "12345");
            var birthDate = new DateOnly(1990, 1, 1);
            var ssn = "90010112345";
            var licenses = new List<DriverLicense> { new DriverLicense() };

            // Act & Assert
            Assert.Throws<ValidationException>(() => new Driver(lastName, firstName, address, birthDate, ssn, licenses));
        }

        [Fact]
        public void CreateDriverWithInvalidFirstName()
        {
            // Arrange
            var lastName = "Doe";
            var firstName = "  "; // Invalid: Empty
            var address = new Address(1, "Street 1", "1", "City", "12345");
            var birthDate = new DateOnly(1990, 1, 1);
            var ssn = "90010112345";
            var licenses = new List<DriverLicense> { new DriverLicense() };

            // Act & Assert
            Assert.Throws<ValidationException>(() => new Driver(lastName, firstName, address, birthDate, ssn, licenses));
        }

        [Fact]
        public void CreateDriverWithInvalidBirthDate()
        {
            // Arrange
            var lastName = "Doe";
            var firstName = "John";
            var address = new Address(1, "Street 1", "1", "City", "12345");
            var birthDate = new DateOnly(DateTime.Now.Year + 1, 1, 1); // Invalid: Future birth date
            var ssn = "90010112345";
            var licenses = new List<DriverLicense> { new DriverLicense() };

            // Act & Assert
            Assert.Throws<ValidationException>(() => new Driver(lastName, firstName, address, birthDate, ssn, licenses));
        }

        [Fact]
        public void CreateDriverWithInvalidSSNLength()
        {
            // Arrange
            var lastName = "Doe";
            var firstName = "John";
            var address = new Address(1, "Street 1", "1", "City", "12345");
            var birthDate = new DateOnly(1990, 1, 1);
            var ssn = "123456789012345"; // Invalid: SSN length is not 11
            var licenses = new List<DriverLicense> { new DriverLicense() };

            // Act & Assert
            Assert.Throws<ValidationException>(() => new Driver(lastName, firstName, address, birthDate, ssn, licenses));
        }

        //[Fact]
        //public void CreateDriverWithDuplicateSSN()
        //{

        //    // Arrange
        //    var lastName1 = "Doe";
        //    var firstName1 = "John";
        //    var address1 = new Address(1, "Street 1", "1", "City", "12345");
        //    var birthDate1 = new DateOnly(1990, 1, 1);
        //    var ssn1 = "11111111111";
        //    var licenses1 = new List<DriverLicense> { new DriverLicense() };
        //    var driver1 = new Driver(lastName1, firstName1, address1, birthDate1, ssn1, licenses1);

        //    var lastName2 = "Smith";
        //    var firstName2 = "Alice";
        //    var address2 = new Address(2, "Street 2", "2", "City", "12345");
        //    var birthDate2 = new DateOnly(1985, 6, 15);
        //    var ssn2 = "11111111111"; // Same SSN as driver1
        //    var licenses2 = new List<DriverLicense> { new DriverLicense() };

        //    // Act & Assert
        //    Assert.Throws<DuplicateEntryException>(() => new Driver(lastName2, firstName2, address2, birthDate2, ssn2, licenses2));
        //}


        [Fact]
        public void CreateDriverWithInvalidSSNChecksum()
        {
            // Arrange
            var lastName = "Doe";
            var firstName = "John";
            var address = new Address(1, "Street 1", "1", "City", "12345");
            var birthDate = new DateOnly(1990, 1, 1);
            var ssn = "9001015432111111111"; // Invalid: Incorrect checksum
            var licenses = new List<DriverLicense> { new DriverLicense() };

            // Act & Assert
            Assert.Throws<ValidationException>(() => new Driver(lastName, firstName, address, birthDate, ssn, licenses));
        }

        [Fact]
        public void CreateDriverWithValidBelgianSSN()
        {
            // Arrange
            var lastName = "Doe";
            var firstName = "John";
            var address = new Address(1, "Street 1", "1", "City", "12345");
            var birthDate = new DateOnly(1985, 1, 1);
            var ssn = "85010112345"; // Valid Belgian SSN
            var licenses = new List<DriverLicense> { new DriverLicense() };

            // Act & Assert
            Assert.NotNull(new Driver(lastName, firstName, address, birthDate, ssn, licenses));
        }

        [Fact]
        public void CreateDriverWithInvalidBelgianSSN()
        {
            // Arrange
            var lastName = "Doe";
            var firstName = "John";
            var address = new Address(1, "Street 1", "1", "City", "12345");
            var birthDate = new DateOnly(1985, 1, 1);
            var ssn = "850101543211111111"; // Invalid Belgian SSN (incorrect checksum)
            var licenses = new List<DriverLicense> { new DriverLicense() };

            // Act & Assert
            Assert.Throws<ValidationException>(() => new Driver(lastName, firstName, address, birthDate, ssn, licenses));
        }

        [Fact]
        public void ValidateAndProcessBelgianSSNWithValidSSN()
        {
            // Arrange
            var driver = new Driver();
            var ssn = "93051822361"; // Valid Belgian SSN

            // Act
            var result = driver.ValidateAndProcessBelgianSSN(ssn);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateAndProcessBelgianSSNWithInvalidSSNFormat()
        {
            // Arrange
            var driver = new Driver();
            var ssn = "85010154321"; // Invalid Belgian SSN (incorrect checksum)

            // Act & Assert
            Assert.Throws<ValidationException>(() => driver.ValidateAndProcessBelgianSSN(ssn));
        }

        [Fact]
        public void ValidateAndProcessBelgianSSNWithInvalidBirthdate()
        {
            // Arrange
            var driver = new Driver();
            var ssn = "95010154321"; // Invalid birthdate (future date)

            // Act & Assert
            Assert.Throws<ValidationException>(() => driver.ValidateAndProcessBelgianSSN(ssn));
        }

        // Add more tests for other properties and validation scenarios...
    }
}
