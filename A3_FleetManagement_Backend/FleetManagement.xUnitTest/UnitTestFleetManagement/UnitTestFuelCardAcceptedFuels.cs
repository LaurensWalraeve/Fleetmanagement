using FleetManagement.Common.Models;
using Xunit;

namespace FleetManagement.xUnitTest.UnitTestFleetManagement
{
    public class UnitTestFuelCardAcceptedFuels
    {
        [Fact]
        public void CreateFuelCardAcceptedFuelsWithValidFuelType()
        {
            // Arrange
            var fuelCardAcceptedFuels = new FuelCardAcceptedFuels();
            var fuelType = new FuelType(1,"Gasoline"); // Use a concrete instance of FuelType

            // Act
            fuelCardAcceptedFuels.FuelType = fuelType;

            // Assert
            Assert.Equal(fuelType, fuelCardAcceptedFuels.FuelType);
        }

        [Fact]
        public void CreateFuelCardAcceptedFuelsWithNullFuelType()
        {
            // Arrange
            var fuelCardAcceptedFuels = new FuelCardAcceptedFuels();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => fuelCardAcceptedFuels.FuelType = null);
        }
    }
}
