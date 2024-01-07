using FleetManagement.Common.Exceptions;
using FleetManagement.Common.Models;
using FleetManagement.xUnitTest.UnitTestFleetManagement;
using System;
using Xunit;

namespace FleetManagement.xUnitTest.UnitTestFleetManagement
{
    public class UnitTestVehicle
    {
        [Fact]
        public void CreateVehicleWithValidValues()
        {
            // Arrange & Act
            var fuelType = new FuelType(1, "Gasoline");
            var vehicleType = new VehicleType(1, "Sedan");
            var vehicle = new Vehicle(1, "Toyota", "Camry", "123ABC", "ABC123", fuelType, vehicleType, "Red", 4);

            // Assert
            Assert.NotNull(vehicle);
            Assert.Equal(1, vehicle.VehicleId);
            Assert.Equal("Toyota", vehicle.Make);
            Assert.Equal("Camry", vehicle.Model);
            Assert.Equal("123ABC", vehicle.ChassisNumber);
            Assert.Equal("ABC123", vehicle.LicensePlate);
            Assert.Equal(fuelType, vehicle.FuelType);
            Assert.Equal(vehicleType, vehicle.VehicleType);
            Assert.Equal("Red", vehicle.Color);
            Assert.Equal(4, vehicle.NumberOfDoors);
            Assert.NotNull(vehicle.CreatedAt);
        }

        [Fact]
        public void CreateVehicleWithNullMake()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new Vehicle(2, null, "Model", "456DEF", "DEF456", new FuelType(2, "Diesel"), new VehicleType(2, "Truck"), null, null));
        }

        [Fact]
        public void CreateVehicleWithEmptyMake()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new Vehicle(3, "", "Model", "789GHI", "GHI789", new FuelType(3, "Electric"), new VehicleType(3, "SUV"), null, null));
        }

        [Fact]
        public void CreateVehicleWithNullModel()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new Vehicle(4, "Honda", null, "101JKL", "JKL101", new FuelType(4, "Hybrid"), new VehicleType(4, "Hatchback"), null, null));
        }

        [Fact]
        public void CreateVehicleWithEmptyModel()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new Vehicle(5, "Ford", "", "121MNO", "MNO121", new FuelType(5, "Gasoline"), new VehicleType(5, "Coupe"), null, null));
        }

        [Fact]
        public void CreateVehicleWithNullChassisNumber()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new Vehicle(6, "Nissan", "Altima", null, "ABCDEF", new FuelType(6, "Electric"), new VehicleType(6, "Sedan"), null, null));
        }

        [Fact]
        public void CreateVehicleWithEmptyChassisNumber()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new Vehicle(7, "Chevrolet", "Malibu", "", "DEFABC", new FuelType(7, "Hybrid"), new VehicleType(7, "Convertible"), null, null));
        }

        [Fact]
        public void CreateVehicleWithWhitespaceChassisNumber()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new Vehicle(8, "Mercedes", "C-Class", "   ", "XYZ123", new FuelType(8, "Gasoline"), new VehicleType(8, "Luxury"), null, null));
        }

        [Fact]
        public void CreateVehicleWithNullLicensePlate()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new Vehicle(9, "BMW", "X5", "123XYZ", null, new FuelType(9, "Diesel"), new VehicleType(9, "SUV"), null, null));
        }

        [Fact]
        public void CreateVehicleWithEmptyLicensePlate()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new Vehicle(10, "Audi", "A4", "456ABC", "", new FuelType(10, "Electric"), new VehicleType(10, "Sedan"), null, null));
        }

        [Fact]
        public void CreateVehicleWithWhitespaceLicensePlate()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new Vehicle(11, "Volkswagen", "Golf", "789JKL", "   ", new FuelType(11, "Gasoline"), new VehicleType(11, "Hatchback"), null, null));
        }

        [Fact]
        public void CreateVehicleWithNullFuelType()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Vehicle(12, "Tesla", "Model 3", "101MNO", "MNO101", null!, new VehicleType(12, "Electric"), null, null));
        }

        [Fact]
        public void CreateVehicleWithNullVehicleType()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Vehicle(13, "Hyundai", "Santa Fe", "121PQR", "PQR121", new FuelType(13, "Hybrid"), null!, null, null));
        }

        [Fact]
        public void CreateVehicleWithInvalidNumberOfDoors()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new Vehicle(14, "Kia", "Sportage", "131STU", "STU131", new FuelType(14, "Gasoline"), new VehicleType(14, "SUV"), null, 6));
        }

        [Fact]
        public void TryAddVehicleWithNullVehicle()
        {
            // Arrange, Act & Assert
            Exception ex;
            var result = Vehicle.TryAddVehicle(null!, out ex);
            Assert.False(result);
            Assert.IsType<ArgumentNullException>(ex);
        }

        [Fact]
        public void TryAddVehicleWithExistingVehicle()
        {
            // Arrange
            var existingVehicle = new Vehicle(15, "Ford", "Focus", "141VWX", "VWX141", new FuelType(15, "Electric"), new VehicleType(15, "Compact"), null, null);
            _ = Vehicle.TryAddVehicle(existingVehicle, out _);

            // Act & Assert
            Exception ex;
            var result = Vehicle.TryAddVehicle(existingVehicle, out ex);
            Assert.False(result);
            Assert.IsType<DuplicateEntryException>(ex);
        }

        [Fact]
        public void TryRemoveVehicleWithNullVehicle()
        {
            // Arrange, Act & Assert
            Exception ex;
            var result = Vehicle.TryRemoveVehicle(null!, out ex);
            Assert.False(result);
            Assert.IsType<ArgumentNullException>(ex);
        }

        [Fact]
        public void TryRemoveVehicleWithNonExistingVehicle()
        {
            // Arrange
            var nonExistingVehicle = new Vehicle(16, "Tesla", "Model S", "161YZA", "YZA161", new FuelType(16, "Electric"), new VehicleType(16, "Luxury"), null, null);

            // Act & Assert
            Exception ex;
            var result = Vehicle.TryRemoveVehicle(nonExistingVehicle, out ex);
            Assert.False(result);
            Assert.IsType<ValidationException>(ex);
        }

        

        [Fact]
        public void IsChassisNumberAlreadyExistsWithNonExistingChassisNumber()
        {
            // Arrange
            var nonExistingChassisNumber = "456DEF";

            // Act
            var result = Vehicle.IsChassisNumberAlreadyExists(nonExistingChassisNumber);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsVehicleAlreadyExistsWithExistingVehicle()
        {
            // Arrange
            var existingVehicle = new Vehicle(19, "Chevrolet", "Cruze", "101JKL", "JKL101", new FuelType(19, "Gasoline"), new VehicleType(19, "Compact"), null, null);
            _ = Vehicle.TryAddVehicle(existingVehicle, out _);

            // Act
            var result = Vehicle.IsVehicleAlreadyExists(existingVehicle);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsVehicleAlreadyExistsWithNonExistingVehicle()
        {
            // Arrange
            var nonExistingVehicle = new Vehicle(20, "Mazda", "CX-5", "121MNO", "MNO121", new FuelType(20, "Hybrid"), new VehicleType(20, "SUV"), null, null);

            // Act
            var result = Vehicle.IsVehicleAlreadyExists(nonExistingVehicle);

            // Assert
            Assert.False(result);
        }
    }
}
