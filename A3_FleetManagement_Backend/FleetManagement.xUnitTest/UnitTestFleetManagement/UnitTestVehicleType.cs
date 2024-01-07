using FleetManagement.Common.Exceptions;
using FleetManagement.Common.Models;
using FleetManagement.xUnitTest.UnitTestFleetManagement;
using Xunit;

namespace FleetManagement.xUnitTest.UnitTestFleetManagement
{
    public class UnitTestVehicleType
    {
        [Fact]
        public void CreateVehicleTypeWithValidValues()
        {
            // Arrange & Act
            var vehicleType = new VehicleType(1, "Sedan");

            // Assert
            Assert.NotNull(vehicleType);
            Assert.Equal(1, vehicleType.VehicleTypeID);
            Assert.Equal("Sedan", vehicleType.TypeName);
        }

        [Fact]
        public void CreateVehicleTypeWithZeroID()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new VehicleType(0, "SUV"));
        }

        [Fact]
        public void CreateVehicleTypeWithNegativeID()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new VehicleType(-1, "Truck"));
        }

        [Fact]
        public void CreateVehicleTypeWithNullTypeName()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new VehicleType(2, null));
        }

        [Fact]
        public void CreateVehicleTypeWithEmptyTypeName()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new VehicleType(3, ""));
        }

        [Fact]
        public void CreateVehicleTypeWithWhitespaceTypeName()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new VehicleType(4, "   "));
        }
    }
}
