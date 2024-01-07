using System;
using System.Collections.Generic;
using System.Linq;
using FleetManagement.Common.Exceptions;
using FleetManagement.Common.Models;
using Moq;
using Xunit;

namespace FleetManagement.xUnitTest.UnitTestFleetManagement
{
    public class UnitTestFuelCard
    {
        [Fact]
        public void CreateFuelCardWithValidData()
        {
            // Arrange
            var cardNumber = "123456";
            var expirationDate = new DateOnly(2023, 12, 31);
            var pinCode = "1234";
            var acceptedFuels = new List<FuelCardAcceptedFuels>
            {
                new FuelCardAcceptedFuels { FuelType = new FuelType(1,"Gasoline") }
            };

            // Act
            var fuelCard = new FuelCard(cardNumber, expirationDate, pinCode, acceptedFuels);

            // Assert
            Assert.Equal(cardNumber, fuelCard.CardNumber);
            Assert.Equal(expirationDate, fuelCard.ExpirationDate);
            Assert.Equal(pinCode, fuelCard.PinCode);
            Assert.Equal(acceptedFuels, fuelCard.AcceptedFuels);
            Assert.False(fuelCard.Blocked);
        }

        [Fact]
        public void CreateFuelCardWithInvalidCardNumber()
        {
            // Arrange
            var invalidCardNumber = string.Empty;
            var expirationDate = new DateOnly(2023, 12, 31);

            // Act & Assert
            Assert.Throws<ValidationException>(() => new FuelCard(invalidCardNumber, expirationDate, null, null));
        }

        //[Fact]
        //public void CreateFuelCardWithDuplicateCardNumber()
        //{
        //    // Arrange
        //    var cardNumber1 = "123456";
        //    var expirationDate1 = new DateOnly(2023, 12, 31);
        //    var fuelCard1 = new FuelCard(cardNumber1, expirationDate1, null, null);

        //    var cardNumber2 = "123456"; // Same as cardNumber1
        //    var expirationDate2 = new DateOnly(2024, 12, 31);

        //    // Act & Assert
        //    Assert.Throws<DuplicateEntryException>(() => new FuelCard(cardNumber2, expirationDate2, null, null));
        //}

        [Fact]
        public void CreateFuelCardWithDefaultExpirationDate()
        {
            // Arrange
            var cardNumber = "123456";
            var defaultExpirationDate = default(DateOnly);

            // Act & Assert
            Assert.Throws<ValidationException>(() => new FuelCard(cardNumber, defaultExpirationDate, null, null));
        }

        [Fact]
        public void BlockFuelCard()
        {
            // Arrange
            var fuelCard = new FuelCard("123456", new DateOnly(2023, 12, 31), null, null);

            // Act
            fuelCard.Blocked = true;

            // Assert
            Assert.True(fuelCard.Blocked);
        }

        [Fact]
        public void SetFuelCardAcceptedFuelsWithValidData()
        {
            // Arrange
            var fuelCard = new FuelCard();
            var acceptedFuels = new List<FuelCardAcceptedFuels>
            {
                new FuelCardAcceptedFuels { FuelType = new FuelType(1,"Gasoline") }
            };

            // Act
            fuelCard.AcceptedFuels = acceptedFuels;

            // Assert
            Assert.Equal(acceptedFuels, fuelCard.AcceptedFuels);
        }

        [Fact]
        public void SetFuelCardAcceptedFuelsWithNull()
        {
            // Arrange
            var fuelCard = new FuelCard();

            // Act & Assert
            Assert.Null(fuelCard.AcceptedFuels);
        }
    }
}
