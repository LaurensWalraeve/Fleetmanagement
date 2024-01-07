using FleetManagement.Common.Models.Update;
using FleetManagement.Common.Models;
using Swashbuckle.AspNetCore.Filters;

namespace FleetManagement.API.CreationModels;

public class DriverUpdateModelExample : IExamplesProvider<DriverUpdateModel>
{
    public DriverUpdateModel GetExamples()
    {
        return new DriverUpdateModel
        {
            DriverId = 1,
            LastName = "Smith",
            FirstName = "John",
            Birthdate = new DateOnly(1990, 1, 15),
            SocialSecurityNumber = 90011599704,
            Licenses = new List<DriverLicense>
            {
                new DriverLicense
                {
                    LicenseType = new LicenseType
                    {
                        LicenseTypeID = 1,
                        TypeName = "B",
                        Description = "Standard car license"
                    },
                    //StartDate = DateOnly.Parse("0001-01-01T00:00:00"),
                    //EndDate = DateOnly.Parse("0001-01-01T00:00:00")
                },
                new DriverLicense
                {
                    LicenseType = new LicenseType
                    {
                        LicenseTypeID = 2,
                        TypeName = "A",
                        Description = "Motorcycle license (all motorcycles)"
                    },
                    //StartDate = DateOnly.Parse("0001-01-01T00:00:00"),
                    //EndDate = DateOnly.Parse("0001-01-01T00:00:00")
                }
            },
            Address = new AddressUpdateModel
            {
                AddressId = 1,
                Street = "Main Street",
                HouseNumber = "123",
                ZipCode = "2000",
                City = "Antwerp"
            },
            Vehicle = new VehicleUpdateModel
            {
                VehicleId = 1,
                Make = "Toyota",
                Model = "Camry",
                FuelType = new FuelType(1, "Gasoline"),
                VehicleType = new VehicleType(1, "Sedan"),
                ChassisNumber = "1HGCM82633A123456",
                LicensePlate = "1-ABC-123",
                Color = "Silver",
                NumberOfDoors = 4
            },
            FuelCard = new FuelCardUpdateModel
            {
                FuelCardId = 1,
                CardNumber = "1234567890123450",
                ExpirationDate = DateOnly.Parse("2025-12-31T00:00:00"),
                PinCode = "1234",
                Blocked = false,
                AcceptedFuels = new List<FuelCardAcceptedFuels>
                {
                    new FuelCardAcceptedFuels
                    {
                        FuelType = new FuelType(1, "Gasoline")
                    },

                    new FuelCardAcceptedFuels
                    {
                        FuelType = new FuelType(2, "Diesel")
                    }
                }
            }
        };
    }
}
