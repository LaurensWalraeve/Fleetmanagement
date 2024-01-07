using System;
using FleetManagement.Common.Models;
using Moq;
using Xunit;

namespace FleetManagement.xUnitTest.UnitTestFleetManagement
{
    public class UnitTestDriverLicense
    {
        [Fact]
        public void CreateDriverLicenseWithValidLicenseType()
        {
            // Arrange
            var validLicenseType = new LicenseType();

            // Act
            var driverLicense = new DriverLicense { LicenseType = validLicenseType };

            // Assert
            Assert.NotNull(driverLicense);
            Assert.Equal(validLicenseType, driverLicense.LicenseType);
        }

        [Fact]
        public void CreateDriverLicenseWithNullLicenseType()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new DriverLicense { LicenseType = null });
        }

        [Fact]
        public void CreateDriverLicenseWithDefaultConstructor()
        {
            // Act
            var driverLicense = new DriverLicense();

            // Assert
            Assert.NotNull(driverLicense);
            Assert.NotNull(driverLicense.LicenseType);
        }

        [Fact]
        public void SetLicenseTypeWithValidValue()
        {
            // Arrange
            var validLicenseType = new LicenseType();
            var driverLicense = new DriverLicense();

            // Act
            driverLicense.LicenseType = validLicenseType;

            // Assert
            Assert.Equal(validLicenseType, driverLicense.LicenseType);
        }

        [Fact]
        public void SetLicenseTypeWithNullValue()
        {
            // Arrange
            var driverLicense = new DriverLicense();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => driverLicense.LicenseType = null);
        }
    }
}
