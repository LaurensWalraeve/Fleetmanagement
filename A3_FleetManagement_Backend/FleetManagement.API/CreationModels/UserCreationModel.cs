using FleetManagement.Common.Models;
using System.ComponentModel.DataAnnotations;

public class UserCreationModel
{
    [Required]
    public string Password { get; set; }

    [Required]
    public string Username { get; set; }

    public int RoleID { get; set; }

    public int DriverID { get; set; }
    // Andere eigenschappen die je wilt accepteren
}
