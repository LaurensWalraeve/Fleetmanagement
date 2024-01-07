using FleetManagement.Common.Models;
using FleetManagement.Data.Contexts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetManagement.Data.Entities
{
    [Table("FuelCard")]
    public class FuelCardValue : Auditable
    {
        [Key]
        public int FuelCardID { get; set; }

        public string CardNumber { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public string? PinCode { get; set; }
        public bool? IsBlocked { get; set; }
        public DateTime? DeletedAt { get; set; }

        public List<FuelCardAcceptedFuelsValue>? AcceptedFuels { get; set; }
        public List<DriverFuelCardValue>? DriverFuelCards { get; set; }

    }
}
