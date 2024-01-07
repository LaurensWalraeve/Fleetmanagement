using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetManagement.Data.Entities;

[Table("DriverVehicle")]
public class DriverVehicleValue
{
    public int DriverID { get; set; }
    public int VehicleID { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set;}

    [ForeignKey("DriverID")] public DriverValue Driver { get; set; }

    [ForeignKey("VehicleID")] public VehicleValue Vehicle { get; set; }
}