using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Data.Entities
{
    [Table("VehicleType")]
    public class VehicleTypeValue
    {
        [Key]
        public int VehicleTypeID { get; set; }
        public string TypeName { get; set; }

        // Navigatie-eigenschappen (indien nodig)
        // Hier voeg je navigatie-eigenschappen toe voor eventuele relaties met andere entiteiten
    }
}
