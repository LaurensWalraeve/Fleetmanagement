using System;
using FleetManagement.Common.Exceptions;
using FleetManagement.Common.Models;
using Xunit;

namespace FleetManagement.xUnitTest.UnitTestFleetManagement
{
    public class UnitTestDriverFuelCard
    {
        [Fact]
        public void CreateDriverFuelCardWithValidData()
        {
            // Arrange
            var fuelCard = new FuelCard("123456", new DateOnly(2023, 12, 31), null, null);
            var driver = new Driver();
            var startDate = new DateTime(2023, 1, 1);
            var endDate = new DateTime(2023, 12, 31);

            // Act
            var driverFuelCard = new DriverFuelCard(fuelCard, driver, startDate, endDate);

            // Assert
            Assert.Equal(fuelCard, driverFuelCard.FuelCard);
            Assert.Equal(driver, driverFuelCard.Driver);
            Assert.Equal(startDate, driverFuelCard.StartDate);
            Assert.Equal(endDate, driverFuelCard.EndDate);
        }

        [Fact]
        public void CreateDriverFuelCardWithNullFuelCard()
        {
            // Arrange
            var driver = new Driver();
            var startDate = new DateTime(2023, 1, 1);
            var endDate = new DateTime(2023, 12, 31);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new DriverFuelCard(null, driver, startDate, endDate));
        }

        [Fact]
        public void CreateDriverFuelCardWithNullDriver()
        {
            // Arrange
            var fuelCard = new FuelCard("123456", new DateOnly(2023, 12, 31), null, null);
            var startDate = new DateTime(2023, 1, 1);
            var endDate = new DateTime(2023, 12, 31);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new DriverFuelCard(fuelCard, null, startDate, endDate));
        }

        [Fact]
        public void CreateDriverFuelCardWithInvalidDateRange()
        {
            // Arrange
            var fuelCard = new FuelCard("123456", new DateOnly(2023, 12, 31), null, null);
            var driver = new Driver();
            var endDate = new DateTime(2023, 1, 1); // Invalid date range

            // Act & Assert
            Assert.Throws<ValidationException>(() => new DriverFuelCard(fuelCard, driver, DateTime.Now, endDate));
        }

        //[Fact]
        //public void CreateDriverFuelCardWithOverlappingDates()
        //{
        //    // Arrange
        //    var fuelCard1 = new FuelCard("123456", new DateOnly(2023, 12, 31), null, null);
        //    var driver1 = new Driver();
        //    var startDate1 = new DateTime(2023, 1, 1);
        //    var endDate1 = new DateTime(2023, 12, 31);

        //    var fuelCard2 = new FuelCard("789012", new DateOnly(2023, 12, 31), null, null);
        //    var driver2 = new Driver();
        //    var startDate2 = new DateTime(2023, 6, 1); // Overlapping date range
        //    var endDate2 = new DateTime(2023, 12, 31);

        //    // Act
        //    var driverFuelCard1 = new DriverFuelCard(fuelCard1, driver1, startDate1, endDate1);

        //    // Assert
        //    Assert.Throws<ValidationException>(() => new DriverFuelCard(fuelCard2, driver2, startDate2, endDate2));
        //}

        // Add more tests as needed
    }
}
