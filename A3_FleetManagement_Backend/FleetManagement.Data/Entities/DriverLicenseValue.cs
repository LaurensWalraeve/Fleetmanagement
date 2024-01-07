using FleetManagement.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

public class DriverLicenseValue
{

    public int DriverID { get; set; }
    public int LicenseTypeID { get; set; }

    [ForeignKey("DriverID")]
    public DriverValue Driver { get; set; }

    [ForeignKey("LicenseTypeID")]
    public LicenseTypeValue LicenseType { get; set; }
        

}
