using FleetManagement.Common.Exceptions;
using FleetManagement.Common.Models;
using FleetManagement.xUnitTest.UnitTestFleetManagement;
using Xunit;

namespace FleetManagement.xUnitTest.UnitTestFleetManagement
{
    public class UnitTestFuelType
    {
        [Fact]
        public void CreateFuelTypeWithValidValues()
        {
            // Arrange & Act
            var fuelType = new FuelType(1, "Gasoline");

            // Assert
            Assert.NotNull(fuelType);
            Assert.Equal(1, fuelType.FuelTypeID);
            Assert.Equal("Gasoline", fuelType.TypeName);
        }

        [Fact]
        public void CreateFuelTypeWithZeroID()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new FuelType(0, "Diesel"));
        }

        [Fact]
        public void CreateFuelTypeWithNegativeID()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new FuelType(-1, "Electric"));
        }

        [Fact]
        public void CreateFuelTypeWithNullTypeName()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new FuelType(2, null));
        }

        [Fact]
        public void CreateFuelTypeWithEmptyTypeName()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new FuelType(3, ""));
        }

        [Fact]
        public void CreateFuelTypeWithWhitespaceTypeName()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new FuelType(4, "   "));
        }
    }
}
