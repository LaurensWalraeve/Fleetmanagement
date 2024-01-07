using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FleetManagement.Common.Models;
using FleetManagement.Data.Contexts;

namespace FleetManagement.Data.Entities
{
    [Table("User")]
    public class UserValue : Auditable
    {
        [Key]
        public int? UserID { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public RoleValue? Role { get; set; }

        public int? DriverID { get; set; }
        [ForeignKey("DriverID")]

        public DriverValue? Driver { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
