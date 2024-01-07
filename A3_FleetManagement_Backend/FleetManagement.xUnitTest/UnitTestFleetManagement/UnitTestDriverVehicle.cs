using System;
using FleetManagement.Common.Models;
using Xunit;

namespace FleetManagement.xUnitTest.UnitTestFleetManagement
{
    public class UnitTestDriverVehicle
    {
        private static DateOnly? GetNullableDateOnly()
        {
            return null;
        }
        [Fact]
        public void CreateDriverVehicleWithValidDates()
        {
            // Arrange
            var driver = new Driver();
            var vehicle = new Vehicle();
            var startDate = new DateOnly(2023, 1, 1);
            var endDate = new DateOnly(2023, 12, 31);

            // Act & Assert
            Assert.NotNull(new DriverVehicle(driver, vehicle, startDate, endDate));
        }

        [Fact]
        public void CreateDriverVehicleWithNullDriver()
        {
            // Arrange
            var vehicle = new Vehicle();
            var startDate = new DateOnly(2023, 1, 1);
            var endDate = new DateOnly(2023, 12, 31);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new DriverVehicle(null, vehicle, startDate, endDate));
        }

        [Fact]
        public void CreateDriverVehicleWithNullVehicle()
        {
            // Arrange
            var driver = new Driver();
            var startDate = new DateOnly(2023, 1, 1);
            var endDate = new DateOnly(2023, 12, 31);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new DriverVehicle(driver, null, startDate, endDate));
        }

        //[Fact]
        //public void CreateDriverVehicleWithNullStartDate()
        //{
        //    // Arrange
        //    var driver = new Driver();
        //    var vehicle = new Vehicle();
        //    DateOnly? startDate = GetNullableDateOnly();
        //    DateOnly endDate = new DateOnly(2023, 12, 31);

        //    // Act & Assert
        //    Assert.Throws<ArgumentException>(() => new DriverVehicle(driver, vehicle, startDate, endDate));
        //}


        [Fact]
        public void CreateDriverVehicleWithNullEndDate()
        {
            // Arrange
            var driver = new Driver();
            var vehicle = new Vehicle();
            var startDate = new DateOnly(2023, 1, 1);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new DriverVehicle(driver, vehicle, startDate, default));
        }

        [Fact]
        public void CreateDriverVehicleWithEndDateBeforeStartDate()
        {
            // Arrange
            var driver = new Driver();
            var vehicle = new Vehicle();
            var startDate = new DateOnly(2023, 12, 31);
            var endDate = new DateOnly(2023, 1, 1);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new DriverVehicle(driver, vehicle, startDate, endDate));
        }

        [Fact]
        public void IsOverlapWithNoOverlap()
        {
            // Arrange
            var existingAssignment = new DriverVehicle(new Driver(), new Vehicle(), new DateOnly(2023, 1, 1), new DateOnly(2023, 12, 31));
            var newAssignment = new DriverVehicle(new Driver(), new Vehicle(), new DateOnly(2024, 1, 1), new DateOnly(2024, 12, 31));

            // Act
            var result = existingAssignment.IsOverlap(newAssignment);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsOverlapWithOverlap()
        {
            // Arrange
            var existingAssignment = new DriverVehicle(new Driver(), new Vehicle(), new DateOnly(2023, 1, 1), new DateOnly(2023, 12, 31));
            var newAssignment = new DriverVehicle(new Driver(), new Vehicle(), new DateOnly(2023, 6, 1), new DateOnly(2023, 12, 31));

            // Act
            var result = existingAssignment.IsOverlap(newAssignment);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsOverlapWithSameDates()
        {
            // Arrange
            var existingAssignment = new DriverVehicle(new Driver(), new Vehicle(), new DateOnly(2023, 1, 1), new DateOnly(2023, 12, 31));
            var newAssignment = new DriverVehicle(new Driver(), new Vehicle(), new DateOnly(2023, 1, 1), new DateOnly(2023, 12, 31));

            // Act
            var result = existingAssignment.IsOverlap(newAssignment);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsOverlapWithPartialOverlap()
        {
            // Arrange
            var existingAssignment = new DriverVehicle(new Driver(), new Vehicle(), new DateOnly(2023, 1, 1), new DateOnly(2023, 12, 31));
            var newAssignment = new DriverVehicle(new Driver(), new Vehicle(), new DateOnly(2023, 6, 1), new DateOnly(2024, 6, 1));

            // Act
            var result = existingAssignment.IsOverlap(newAssignment);

            // Assert
            Assert.True(result);
        }

    }
}
