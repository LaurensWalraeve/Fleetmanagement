using FleetManagement.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FleetManagement.Common.Models
{
    public class FuelCard
    {
        private string _cardNumber;
        private DateOnly _expirationDate;
        private List<FuelCard> _fuelCards { get; set; } = new();

        // Constructor to create a new fuel card
        public FuelCard(string cardNumber, DateOnly expirationDate, string? pinCode, List<FuelCardAcceptedFuels>? acceptedFuels)
        {
            CardNumber = cardNumber;
            ExpirationDate = expirationDate;
            PinCode = pinCode;
            AcceptedFuels = acceptedFuels;
            Blocked = false;
        }

        public FuelCard() { }

        public DateTime? DeletedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }

        // Properties
        public int FuelCardId { get; set; }

        public string CardNumber
        {
            get => _cardNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("Card number cannot be null or consist of only whitespace.");

                if (_fuelCards.Any(fuelCard => fuelCard.CardNumber == value))
                    throw new DuplicateEntryException("A card with this card number already exists");

                _cardNumber = value;
            }
        }

        public DateOnly ExpirationDate
        {
            get => _expirationDate;
            set
            {
                if (value == default)
                    throw new ValidationException("Expiration date cannot be null or the default value.");

                _expirationDate = value;
            }
        }

        public string? PinCode { get; set; }
        public bool Blocked { get; set; }
        public List<FuelCardAcceptedFuels>? AcceptedFuels { get; set; }
    }
}
