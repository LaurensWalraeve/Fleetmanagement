using System;
using FleetManagement.Common.Exceptions;
using FleetManagement.Common.Models;

namespace FleetManagement.xUnitTest.UnitTestFleetManagement
{
    public class UnitTestLicenseType
    {
        // Valid test cases

        [Fact]
        public void CreateLicenseTypeWithValidData()
        {
            var licenseType = new LicenseType(1, "Standard car license", "B");
            Assert.NotNull(licenseType);
            Assert.Equal(1, licenseType.LicenseTypeID);
            Assert.Equal("Standard car license", licenseType.Description);
            Assert.Equal("B", licenseType.TypeName);
        }

        [Fact]
        public void CreateLicenseTypeWithNullDescription()
        {
            Assert.Throws<ValidationException>(() =>
                new LicenseType(2, null, "A"));
        }


        // Invalid test cases

        [Fact]
        public void CreateLicenseTypeWithInvalidID()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new LicenseType(0, "InvalidDescription", "InvalidType"));
        }

        [Fact]
        public void CreateLicenseTypeWithNullTypeName()
        {
            Assert.Throws<ValidationException>(() =>
                new LicenseType(3, "Description", null));
        }

        [Fact]
        public void CreateLicenseTypeWithWhitespaceTypeName()
        {
            Assert.Throws<ValidationException>(() =>
                new LicenseType(4, "Description", "   "));
        }

        [Fact]
        public void CreateLicenseTypeWithNullDescriptionAndTypeName()
        {
            Assert.Throws<ValidationException>(() =>
                new LicenseType(5, null, null));
        }
    }

}

