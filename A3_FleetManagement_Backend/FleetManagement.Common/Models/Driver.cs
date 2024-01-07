using FleetManagement.Common.Exceptions;
using System;
using System.Collections.Generic;

namespace FleetManagement.Common.Models
{
    public class Driver
    {
        private DateOnly _birthDate;
        private int _driverId;
        private string _firstName;
        private string _lastName;
        private string _socialSecurityNumber;
        private List<DriverLicense> _licenses;

        public Driver(string lastName, string firstName, Address address, DateOnly birthDate,
            string socialSecurityNumber, List<DriverLicense> licenses)
        {
            LastName = lastName;
            FirstName = firstName;
            Address = address;
            BirthDate = birthDate;
            SocialSecurityNumber = socialSecurityNumber;
            Licenses = licenses;
            CreatedAt = DateTime.UtcNow; // Set the creation time to the current UTC time
        }

        public Driver()
        {
            CreatedAt = DateTime.UtcNow; // Set the creation time to the current UTC time
        }

        public int DriverId
        {
            get => _driverId;
            set
            {
                if (_drivers.Exists(driver => driver.DriverId == value))
                    throw new DuplicateEntryException("A driver with this Driver ID already exists");

                _driverId = value;
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("Last name cannot be null or consist of only whitespace.");

                _lastName = value;
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("First name cannot be null or consist of only whitespace.");

                _firstName = value;
            }
        }

        public DateOnly BirthDate
        {
            get => _birthDate;
            set
            {
                // Set a default minimum age (e.g., 18)
                var minAllowedDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-18));

                if (value > minAllowedDate)
                    throw new ValidationException("Driver age cannot be less than 18");

                _birthDate = value;
            }
        }

        public string SocialSecurityNumber
        {
            get => _socialSecurityNumber;
            set
            {
                // Check if SSN is 11 numbers long
                if (value.Length != 11)
                    throw new ValidationException("Invalid Social Security Number format or length.");

                if (_drivers.Exists(driver => driver.SocialSecurityNumber == value))
                    throw new DuplicateEntryException("A driver with this National Identification Number already exists");

                _socialSecurityNumber = value;
            }
        }

        public DateTime? DeletedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<DriverLicense> Licenses
        {
            get => _licenses;
            set => _licenses = value ?? new List<DriverLicense>();
        }

        public Address? Address { get; set; }
        public List<Vehicle>? Vehicles { get; set; }
        public List<FuelCard>? FuelCards { get; set; }

        private static List<Driver> _drivers = new List<Driver>();

        // Add a method to validate and process Belgian SSN
        public bool ValidateAndProcessBelgianSSN(string ssnString)
        {
            // Convert the SocialSecurityNumber to a string for processing

            // Check the length of the SSN (it should be 11 characters)
            if (ssnString.Length != 11)
            {
                throw new ValidationException("Invalid Belgian SSN format");
            }

            // Split the SSN into parts
            string birthDatePart = ssnString.Substring(0, 6);
            string genderPart = ssnString.Substring(6, 3);
            string checksumPart = ssnString.Substring(9, 2);

            // Validate the birthdate part
            if (!DateTime.TryParseExact(birthDatePart, "yyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime birthDate))
            {
                throw new ValidationException("Invalid birthdate in Belgian SSN");
            }

            // Extract gender information
            string gender = int.Parse(genderPart) % 2 == 0 ? "female" : "male";

            // Calculate and validate the checksum
            int checksum = int.Parse(checksumPart);

            string nineDigitPart = ssnString.Substring(0, 9);
            int calculatedChecksum;

            if (birthDate.Year >= 2000)
            {
                calculatedChecksum = 97 - (int.Parse("2" + nineDigitPart) % 97);
            }
            else
            {
                calculatedChecksum = 97 - (int.Parse(nineDigitPart) % 97);
            }

            if (checksum != calculatedChecksum)
            {
                throw new ValidationException("Invalid checksum in Belgian SSN");
            }
            return true;
        }
    }
}

