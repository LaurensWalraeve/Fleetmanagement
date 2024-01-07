using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Data.Entities
{
    [Table("FuelType")]
    public class FuelTypeValue
    {
        [Key]
        public int FuelTypeID { get; set; }
        public string TypeName { get; set; }

        // Navigatie-eigenschappen (indien nodig)
        // Hier voeg je navigatie-eigenschappen toe voor eventuele relaties met andere entiteiten
    }
}
