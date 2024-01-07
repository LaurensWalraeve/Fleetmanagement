using System;

namespace FleetManagement.Common.Models
{
    public class FuelCardAcceptedFuels
    {
        private FuelType _fuelType;

        public FuelType FuelType
        {
            get => _fuelType;
            set => _fuelType = value ?? throw new ArgumentNullException(nameof(value), "FuelType cannot be null.");
        }

        public FuelCardAcceptedFuels()
        {
        }
    }
}
