using System;
using FleetManagement.Common.Exceptions;

namespace FleetManagement.Common.Models
{
    public class VehicleType
    {
        private string _typeName;

        public VehicleType(int vehicleTypeID, string typeName)
        {
            ValidateID(vehicleTypeID, "VehicleTypeID");
            ValidateTypeName(typeName);

            VehicleTypeID = vehicleTypeID;
            TypeName = typeName;
        }

        public int VehicleTypeID { get; private set; }

        public string TypeName
        {
            get => _typeName;
            set => _typeName = ValidateTypeName(value);
        }

        private void ValidateID(int id, string propertyName)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(propertyName, $"{propertyName} must be greater than zero.");
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
