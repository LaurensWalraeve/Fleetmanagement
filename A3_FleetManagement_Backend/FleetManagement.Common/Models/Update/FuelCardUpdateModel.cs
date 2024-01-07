namespace FleetManagement.Common.Models.Update;

public class FuelCardUpdateModel
{
    public int? FuelCardId { get; set; }

    public string? CardNumber { get; set; }
    public DateOnly? ExpirationDate { get; set; }
    public string? PinCode { get; set; }
    public bool? Blocked { get; set; }
    public List<FuelCardAcceptedFuels>? AcceptedFuels { get; set; }

    public FuelCardUpdateModel()
    {
    }
}