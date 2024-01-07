using FleetManagement.Common.Exceptions;

public class Address
{
    private string _city;
    private string _houseNumber;
    private string _street;
    private string _zipCode;

    public Address(int addressID, string street, string houseNumber, string zipCode, string city)
    {
        AddressId = addressID;
        Street = street;
        HouseNumber = houseNumber;
        ZipCode = zipCode;
        City = city;
    }

    public Address() { }

    public int AddressId { get; set; }

    public string Street
    {
        get => _street;
        set => _street = ValidateString(value, "Street");
    }

    public string HouseNumber
    {
        get => _houseNumber;
        set => _houseNumber = ValidateString(value, "House number");
    }

    public string ZipCode
    {
        get => _zipCode;
        set => _zipCode = ValidateString(value, "Zip code");
    }

    public string City
    {
        get => _city;
        set => _city = ValidateString(value, "City");
    }

    private string ValidateString(string value, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ValidationException($"{propertyName} cannot be null or consist of only whitespace.");

        return value;
    }

    public static bool IsAddressAlreadyExists(List<Address> existingAddresses, Address newAddress)
    {
        return existingAddresses.Any(a => AreAddressesEqual(a, newAddress));
    }

    private static bool AreAddressesEqual(Address address1, Address address2)
    {
        return string.Equals(address1.Street, address2.Street, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(address1.HouseNumber, address2.HouseNumber, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(address1.ZipCode, address2.ZipCode, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(address1.City, address2.City, StringComparison.OrdinalIgnoreCase);
    }
}
