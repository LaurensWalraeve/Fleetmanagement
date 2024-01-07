using FleetManagement.Common.Models;
using FleetManagement.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetManagement.Data.Entities
{
    [Table("Driver")]
    public class DriverValue : Auditable
    {
        [Key]
        public int DriverID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateOnly? Birthdate { get; set; }
        public string SocialSecurityNumber { get; set; }
        public DateTime? DeletedAt { get; set; }

        public List<DriverLicenseValue> Licenses { get; set; }

        public int? AddressID { get; set; }

        [ForeignKey("AddressID")]
        public AddressValue? Address { get; set; }

        // Add these navigation properties to support the optimized query
        public List<DriverVehicleValue>? Vehicles { get; set; }
        public List<DriverFuelCardValue>? FuelCards { get; set; }

    }
}
