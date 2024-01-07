using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Data.Entities
{
    [Table("Role")]
    public class RoleValue
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}
