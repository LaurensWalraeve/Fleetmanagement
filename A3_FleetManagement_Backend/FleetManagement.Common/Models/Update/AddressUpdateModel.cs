namespace FleetManagement.Common.Models.Update;

public class AddressUpdateModel
{
    public int? AddressId { get; set; }

    public string? City { get; set; }
    public string? HouseNumber { get; set; }
    public string? Street { get; set; }
    public string? ZipCode { get; set; }

    public AddressUpdateModel()
    {
    }
}