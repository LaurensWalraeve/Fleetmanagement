namespace FleetManagement.Common.Models.Update;

public class DriverUpdateModel
{
    public int? DriverId { get; set; }

    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public DateOnly? Birthdate { get; set; }
    public long? SocialSecurityNumber { get; set; }
    public List<DriverLicense>? Licenses { get; set; }
    public AddressUpdateModel? Address { get; set; }
    public VehicleUpdateModel? Vehicle { get; set; }
    public FuelCardUpdateModel? FuelCard { get; set; }

    public DriverUpdateModel()
    {
    }
}