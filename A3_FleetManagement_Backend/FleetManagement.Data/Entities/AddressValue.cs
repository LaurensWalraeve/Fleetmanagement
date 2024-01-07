using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Data.Entities;

[Table("Address")]
public class AddressValue
{
    [Key]
    public int AddressID { get; set; }
    public string? Street { get; set; }
    public string? HouseNumber { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }
}