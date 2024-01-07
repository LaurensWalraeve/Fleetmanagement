using System;
using System.Collections.Generic;
using FleetManagement.Common.Exceptions;

namespace FleetManagement.Common.Models
{
    public class FuelType
    {
        private string _typeName;

        public FuelType(int fuelTypeID, string typeName)
        {
            ValidateFuelTypeID(fuelTypeID);
            ValidateTypeName(typeName);

            FuelTypeID = fuelTypeID;
            TypeName = typeName;
        }

        public int FuelTypeID { get; }

        public string TypeName
        {
            get => _typeName;
            set => _typeName = ValidateTypeName(value);
        }

        private void ValidateFuelTypeID(int fuelTypeID)
        {
            if (fuelTypeID <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(fuelTypeID), "FuelTypeID must be greater than zero.");
            }
        }

        private string ValidateTypeName(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
            {
                throw new ValidationException("Type name cannot be null or consist of only whitespace.");
            }

            return typeName;
        }
    }
}
