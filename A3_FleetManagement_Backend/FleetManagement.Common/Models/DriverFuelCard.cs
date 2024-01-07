using FleetManagement.Common.Exceptions;
using System;
using System.Collections.Generic;

namespace FleetManagement.Common.Models
{
    public class DriverFuelCard
    {
        private static List<DriverFuelCard> _driverFuelCards = new List<DriverFuelCard>();

        public DriverFuelCard(FuelCard fuelCard, Driver driver, DateTime startDate, DateTime endDate)
        {
            FuelCard = fuelCard ?? throw new ArgumentNullException(nameof(fuelCard), "FuelCard cannot be null.");
            Driver = driver ?? throw new ArgumentNullException(nameof(driver), "Driver cannot be null.");

            ValidateDateRange(startDate, endDate);

            if (IsOverlap(driver, startDate, endDate))
            {
                throw new ValidationException("Driver already has an overlapping fuel card for the given date range.");
            }

            FuelCard = fuelCard;
            Driver = driver;
            StartDate = startDate;
            EndDate = endDate;

            _driverFuelCards.Add(this); // Add the new DriverFuelCard to the list
        }

        public FuelCard FuelCard { get; }
        public Driver Driver { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        private void ValidateDateRange(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                throw new ValidationException("Start date cannot be after the end date.");
            }
        }

        private bool IsOverlap(Driver driver, DateTime startDate, DateTime endDate)
        {
            foreach (var existingCard in _driverFuelCards)
            {
                // Check if the existing card is for the same driver
                if (existingCard.Driver == driver)
                {
                    // Check for date range overlap
                    if (startDate <= existingCard.EndDate && endDate >= existingCard.StartDate)
                    {
                        // Overlapping fuel cards found
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
