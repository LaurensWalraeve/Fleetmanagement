namespace FleetManagement.Common.Models.Update;

public class VehicleUpdateModel
{
    public int? VehicleId { get; set; }

    public string? Make { get; set; }
    public string? Model { get; set; }
    public string? ChassisNumber { get; set; }
    public string? LicensePlate { get; set; }
    public FuelType FuelType { get; set; }
    public VehicleType VehicleType { get; set; }
    public string? Color { get; set; }
    public int NumberOfDoors { get; set; }

    public VehicleUpdateModel()
    {
    }
}