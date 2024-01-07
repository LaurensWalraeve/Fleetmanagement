using System;
using FleetManagement.Common.Exceptions;

namespace FleetManagement.Common.Models
{
    public class LicenseType
    {
        private string? _typeName;
        private string? _description;

        public LicenseType(int licenseTypeID, string description, string typeName)
        {
            ValidateID(licenseTypeID, "LicenseTypeID");
            ValidateDescription(description);
            ValidateTypeName(typeName);

            LicenseTypeID = licenseTypeID;
            Description = description;
            TypeName = typeName;
        }

        public LicenseType()
        {
        }

        public int LicenseTypeID { get; set; }

        public string? TypeName
        {
            get => _typeName;
            set => _typeName = ValidateTypeName(value);
        }

        public string? Description
        {
            get => _description;
            set => _description = ValidateDescription(value);
        }

        private void ValidateID(int id, string propertyName)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(propertyName, $"{propertyName} must be greater than zero.");
            }
        }

        private string? ValidateTypeName(string? typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
            {
                throw new ValidationException("Type name cannot be null or consist of only whitespace.");
            }

            return typeName;
        }

        private string? ValidateDescription(string? description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ValidationException("Description cannot be null or consist of only whitespace.");
            }

            return description;
        }
    }
}
