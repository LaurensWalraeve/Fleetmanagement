using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Data.Entities
{
    [Table("LicenseType")]
    public class LicenseTypeValue
    {
        [Key]
        public int LicenseTypeID { get; set; }
        public string TypeName { get; set; }
        public string? Description { get; set; }
        public List<DriverLicenseValue> DriverLicenses { get; set; }
    }
}
