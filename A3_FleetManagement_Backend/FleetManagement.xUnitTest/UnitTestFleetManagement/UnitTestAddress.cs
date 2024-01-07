using FleetManagement.Common.Exceptions;
using FleetManagement.xUnitTest.UnitTestFleetManagement;
using System.Collections.Generic;
using Xunit;

namespace FleetManagement.xUnitTest.UnitTestFleetManagement
{
    public class UnitTestAddress
    {
        [Fact]
        public void CreateAddressWithValidValues()
        {
            // Arrange & Act
            var address = new Address(1, "Main Street", "123", "2000", "Antwerp");

            // Assert
            Assert.NotNull(address);
            Assert.Equal(1, address.AddressId);
            Assert.Equal("Main Street", address.Street);
            Assert.Equal("123", address.HouseNumber);
            Assert.Equal("2000", address.ZipCode);
            Assert.Equal("Antwerp", address.City);
        }

        [Fact]
        public void CreateAddressWithNullStreet()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new Address(2, null, "456", "3000", "Brussels"));
        }

        [Fact]
        public void CreateAddressWithEmptyStreet()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new Address(3, "", "789", "4000", "Ghent"));
        }

        [Fact]
        public void CreateAddressWithWhitespaceStreet()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new Address(4, "   ", "1011", "5000", "Bruges"));
        }

        [Fact]
        public void CreateAddressWithNullHouseNumber()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new Address(5, "Market Street", null, "6000", "Leuven"));
        }

        [Fact]
        public void CreateAddressWithEmptyHouseNumber()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new Address(6, "Park Avenue", "", "7000", "Namur"));
        }

        [Fact]
        public void CreateAddressWithWhitespaceHouseNumber()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new Address(7, "Broadway", "   ", "8000", "Liege"));
        }

        [Fact]
        public void CreateAddressWithNullZipCode()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new Address(8, "Sunset Boulevard", "222", null, "Mons"));
        }

        [Fact]
        public void CreateAddressWithEmptyZipCode()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new Address(9, "King Street", "333", "", "Charleroi"));
        }

        [Fact]
        public void CreateAddressWithWhitespaceZipCode()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new Address(10, "Elm Street", "444", "   ", "Tournai"));
        }

        [Fact]
        public void CreateAddressWithNullCity()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new Address(11, "High Street", "555", "9000", null));
        }

        [Fact]
        public void CreateAddressWithEmptyCity()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new Address(12, "Low Street", "666", "10000", ""));
        }

        [Fact]
        public void CreateAddressWithWhitespaceCity()
        {
            // Arrange, Act & Assert
            Assert.Throws<ValidationException>(() => new Address(13, "Green Street", "777", "11000", "   "));
        }

        [Fact]
        public void IsAddressAlreadyExistsWithSameAddress()
        {
            // Arrange
            var existingAddresses = new List<Address>
            {
                new Address(14, "Unique Street", "888", "12000", "Ostend")
            };
            var newAddress = new Address(15, "Unique Street", "888", "12000", "Ostend");

            // Act
            var result = Address.IsAddressAlreadyExists(existingAddresses, newAddress);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsAddressAlreadyExistsWithDifferentAddress()
        {
            // Arrange
            var existingAddresses = new List<Address>
            {
                new Address(16, "Different Street", "999", "13000", "Bruges")
            };
            var newAddress = new Address(17, "Unique Street", "888", "12000", "Ostend");

            // Act
            var result = Address.IsAddressAlreadyExists(existingAddresses, newAddress);

            // Assert
            Assert.False(result);
        }
    }
}
