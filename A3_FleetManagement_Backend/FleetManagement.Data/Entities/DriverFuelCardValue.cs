using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetManagement.Data.Entities;

[Table("DriverFuelCard")]
public class DriverFuelCardValue
{
    //[Key] public int DriverFuelCardID { get; set; }

    public int DriverID { get; set; }
    public int FuelCardID { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set;}

    [ForeignKey("DriverID")] public DriverValue Driver { get; set; }

    [ForeignKey("FuelCardID")] public FuelCardValue FuelCard { get; set; }
}