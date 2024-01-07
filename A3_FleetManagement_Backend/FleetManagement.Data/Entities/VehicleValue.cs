using FleetManagement.Data.Contexts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetManagement.Data.Entities
{
    [Table("Vehicle")] // Specify the name of the database table
    public class VehicleValue : Auditable
    {
        [Key]
        public int VehicleID { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public string ChassisNumber { get; set; }
        public string LicensePlate { get; set; }

        // Vreemde sleutel naar FuelTypeValue
        [ForeignKey("FuelType")]
        public int FuelTypeID { get; set; }
        public FuelTypeValue FuelType { get; set; }

        // Vreemde sleutel naar VehicleTypeValue
        [ForeignKey("VehicleType")]
        public int VehicleTypeID { get; set; }
        public VehicleTypeValue VehicleType { get; set; }

        public string? Color { get; set; }
        public int? NumberOfDoors { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
