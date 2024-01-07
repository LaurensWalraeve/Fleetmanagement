using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetManagement.Data.Entities
{
    public class FuelCardAcceptedFuelsValue
    {

        public int FuelCardId { get; set; }
        public int FuelTypeId { get; set; }

        [ForeignKey("FuelCardId")] // CHANGED THIS FROM ID to Id
        public FuelCardValue FuelCard { get; set; }

        [ForeignKey("FuelTypeId")] // CHANGED THIS FROM ID TO Id
        public FuelTypeValue FuelType { get; set; }

    }
}
