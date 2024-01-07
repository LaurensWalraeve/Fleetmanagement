using AutoMapper;
using FleetManagement.Common.Exceptions;
using FleetManagement.Common.Models;
using FleetManagement.Common.Models.Update;
using FleetManagement.Common.Repositories.Interfaces;
using FleetManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace FleetManagement.Data.Repositories;

public class DriverRepository : IDriverRepository
{
    private readonly FleetDbContext _context;
    private readonly IMapper _mapper;

    public DriverRepository(FleetDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Driver>> GetAllDrivers()
    {
        try
        {
            var drivers = await _context.Drivers
                .Include(d => d.Address)
                .Include(d => d.Licenses)
                    .ThenInclude(dl => dl.LicenseType)
                .Include(d => d.Vehicles)
                    .ThenInclude(dv => dv.Vehicle)
                        .ThenInclude(v => v.FuelType)
                .Include(d => d.Vehicles)
                    .ThenInclude(dv => dv.Vehicle)
                        .ThenInclude(v => v.VehicleType)
                .Include(d => d.FuelCards)
                    .ThenInclude(df => df.FuelCard)
                        .ThenInclude(fc => fc.AcceptedFuels)
                            .ThenInclude(fca => fca.FuelType)
                .Where(driver => driver.DeletedAt == null)
                .ToListAsync();

            var mappedDrivers = drivers.Select(driverValue =>
            {
                var driver = _mapper.Map<Driver>(driverValue);
                driver.Address = _mapper.Map<Address>(driverValue.Address);
                driver.Vehicles = _mapper.Map<List<Vehicle>>(driverValue.Vehicles.Select(dv => dv.Vehicle).ToList());
                driver.FuelCards = _mapper.Map<List<FuelCard>>(driverValue.FuelCards.Select(fc => fc.FuelCard).ToList());
                driver.Licenses = driverValue.Licenses
                    .Select(dl => _mapper.Map<DriverLicense>(dl))
                    .ToList();

                return driver;
            }).ToList();

            return mappedDrivers;
        }
        catch (Exception ex)
        {
            throw new DriverException("Failed to retrieve all drivers.", ex);
        }
    }

    public async Task<Driver> GetDriverById(int id)
    {
        try
        {
            var driverValue = await _context.Drivers
                .Include(d => d.Address)
                .Include(d => d.Licenses)
                    .ThenInclude(dl => dl.LicenseType)
                .Include(d => d.Vehicles)
                    .ThenInclude(dv => dv.Vehicle)
                        .ThenInclude(v => v.FuelType)
                .Include(d => d.Vehicles)
                    .ThenInclude(dv => dv.Vehicle)
                        .ThenInclude(v => v.VehicleType)
                .Include(d => d.FuelCards)
                    .ThenInclude(df => df.FuelCard)
                        .ThenInclude(fc => fc.AcceptedFuels)
                            .ThenInclude(fca => fca.FuelType)
                .FirstOrDefaultAsync(d => d.DriverID == id && d.DeletedAt == null);

            if (driverValue == null)
                return null;

            var driver = _mapper.Map<Driver>(driverValue);
            driver.Address = _mapper.Map<Address>(driverValue.Address);
            driver.Vehicles = _mapper.Map<List<Vehicle>>(driverValue.Vehicles.Select(dv => dv.Vehicle).ToList());
            driver.FuelCards = _mapper.Map<List<FuelCard>>(driverValue.FuelCards.Select(fc => fc.FuelCard).ToList());
            driver.Licenses = driverValue.Licenses
                .Select(dl => _mapper.Map<DriverLicense>(dl))
                .ToList();

            return driver;
        }
        catch (Exception ex)
        {
            throw new DriverException("Failed to retrieve the driver by ID.", ex);
        }
    }

    public async Task<List<LicenseType>> GetAllLicenseTypes()
    {
        try
        {
            var licenseTypes = await _context.LicenseTypes.ToListAsync();
            return _mapper.Map<List<LicenseType>>(licenseTypes);
        }
        catch (Exception ex)
        {
            throw new DriverException("Failed to retrieve all license types.", ex);
        }
    }


    public async Task<Driver> CreateDriver(Driver driver)
    {
        if (driver == null)
            throw new ArgumentNullException(nameof(driver), "Driver data must be provided.");

        var currentDrivers = await GetAllDrivers();

        bool doesDriverExist = currentDrivers.Any(d => d.SocialSecurityNumber == driver.SocialSecurityNumber);

        if (doesDriverExist)
        {
            throw new ArgumentException(nameof(driver), "This driver already exists");
        }        
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                // Map the driver DTO to the driver entity
                var driverEntity = _mapper.Map<DriverValue>(driver);
                _context.Drivers.Add(driverEntity);
                await _context.SaveChangesAsync();

                // Address handling
                if (driver.Address != null)
                {
                    var existingAddress = await _context.Addresses
                        .FirstOrDefaultAsync(a =>
                            a.City == driver.Address.City &&
                            a.HouseNumber == driver.Address.HouseNumber &&
                            a.Street == driver.Address.Street &&
                            a.ZipCode == driver.Address.ZipCode);

                    if (existingAddress != null)
                    {
                        driverEntity.AddressID = existingAddress.AddressID;
                    }
                    else
                    {
                        var addressEntity = _mapper.Map<AddressValue>(driver.Address);
                        _context.Addresses.Add(addressEntity);
                        await _context.SaveChangesAsync();
                        driverEntity.AddressID = addressEntity.AddressID;
                    }
                }

                // License handling: Fetch LicenseTypeValue entities from the database
                foreach (var licenseDto in driver.Licenses)
                {
                    // Make sure you're only working with the LicenseTypeID
                    var driverLicense = new DriverLicenseValue
                    {
                        DriverID = driverEntity.DriverID,
                        LicenseTypeID = licenseDto.LicenseType.LicenseTypeID
                    };

                    _context.DriverLicenses.Add(driverLicense);
                }

                // Vehicle handling
                if (driver.Vehicles != null)
                {
                    VehicleValue vehicleEntity = null;
                    foreach (var v in driver.Vehicles) { 
                    if (v.VehicleId != null)
                    {
                        vehicleEntity = await _context.Vehicles.FindAsync(v.VehicleId);
                        if (vehicleEntity == null)
                        {
                            throw new ArgumentException("VehicleId provided does not exist.");
                        }
                       
                    }
                    
                    else
                    {
                        vehicleEntity = _mapper.Map<VehicleValue>(driver.Vehicles);
                        _context.Vehicles.Add(vehicleEntity);
                        await _context.SaveChangesAsync();
                    }
                    }
                    var driverVehicle = new DriverVehicleValue
                    {
                        DriverID = driverEntity.DriverID,
                        VehicleID = vehicleEntity.VehicleID
                    };
                    _context.DriverVehicles.Add(driverVehicle);
                }

                // FuelCard handling
                if (driver.FuelCards != null)
                {
                    FuelCardValue fuelCardEntity = null;
                    foreach(var f in driver.FuelCards) { 
                    if (f.FuelCardId != null)
                    {
                        fuelCardEntity = await _context.FuelCards.FindAsync(f.FuelCardId);
                        if (fuelCardEntity == null)
                        {
                            throw new ArgumentException("FuelCardId provided does not exist.");
                        }
                    }
                    else
                    {
                        fuelCardEntity = _mapper.Map<FuelCardValue>(driver.FuelCards);
                        _context.FuelCards.Add(fuelCardEntity);
                        await _context.SaveChangesAsync();
                    }
                    }
                    var driverFuelCard = new DriverFuelCardValue
                    {
                        DriverID = driverEntity.DriverID,
                        FuelCardID = fuelCardEntity.FuelCardID
                    };
                    _context.DriverFuelCards.Add(driverFuelCard);
                }

                // Final save to commit previous insertions
                await _context.SaveChangesAsync();

                // Commit the transaction
                await transaction.CommitAsync();

                // Retrieve the complete driver object from the database
                var createdDriver = await _context.Drivers
                    .Include(d => d.Address)
                    .Include(d => d.Licenses).ThenInclude(dl => dl.LicenseType)
                    .Include(d => d.Vehicles).ThenInclude(dv => dv.Vehicle)
                    .Include(d => d.FuelCards).ThenInclude(df => df.FuelCard)
                    .FirstOrDefaultAsync(d => d.DriverID == driverEntity.DriverID);

                if (createdDriver == null)
                    throw new InvalidOperationException("Driver could not be loaded after creation.");

                return _mapper.Map<Driver>(createdDriver);
            }
            catch (Exception)
            {
                // Rollback the transaction if any exception occurs
                await transaction.RollbackAsync();
                throw; // Rethrow the exception to handle it as per your error handling policy
            }
        }
    }

    public async Task<Driver> UpdateDriver(int id, DriverUpdateModel updateModel, JObject driverJson)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var driverFromDb = await _context.Drivers
                .Include(d => d.Address)
                .Include(d => d.Licenses)
                .Include(d => d.Vehicles)
                    .ThenInclude(dv => dv.Vehicle)
                .Include(d => d.FuelCards)
                    .ThenInclude(dfc => dfc.FuelCard)
                .FirstOrDefaultAsync(d => d.DriverID == id);

            if (driverFromDb == null)
            {
                throw new NotFoundException("Driver not found.");
            }

            // Apply the partial update to the driver
            ApplyPartialUpdate(driverFromDb, updateModel, driverJson);

            // Handle updates for related entities
            await HandleAddressUpdate(driverFromDb, updateModel);
            await HandleVehicleUpdate(driverFromDb, updateModel);
            await HandleFuelCardUpdate(driverFromDb, updateModel);

            // Apply the changes to the database
            _context.Drivers.Update(driverFromDb);
            await _context.SaveChangesAsync();

            // Commit the transaction
            await transaction.CommitAsync();

            // Replace ConvertToDomainModel with the appropriate AutoMapper mapping
            var updatedDriver = _mapper.Map<Driver>(driverFromDb);
            return updatedDriver;
        }
        catch
        {
            // Rollback the transaction if anything goes wrong
            await transaction.RollbackAsync();
            throw;
        }
    }

    private void ApplyPartialUpdate(DriverValue driverFromDb, DriverUpdateModel updateModel, JObject driverJson)
    {
        // Loop through JSON properties
        foreach (var jsonProperty in driverJson.Properties())
        {
            var propName = jsonProperty.Name;
            var propValue = jsonProperty.Value;

            // Find the corresponding property in DriverValue using case-insensitive search
            var driverValueProperty = typeof(DriverValue).GetProperty(propName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (driverValueProperty == null) continue;

            // If the JSON property value is null, set the property to null if it's nullable
            if (propValue.Type == JTokenType.Null &&
                Nullable.GetUnderlyingType(driverValueProperty.PropertyType) != null)
            {
                driverValueProperty.SetValue(driverFromDb, null);
            }
            // If the JSON property value is a simple type, set the property directly
            else if (!IsComplexType(propValue.Type))
            {
                driverValueProperty.SetValue(driverFromDb, propValue.ToObject(driverValueProperty.PropertyType));
            }
        }
    }

        private bool IsComplexType(JTokenType type)
        {
            return type != JTokenType.String && type != JTokenType.Integer && type != JTokenType.Float && type != JTokenType.Boolean && type != JTokenType.Date && type != JTokenType.Date;
        }


        private async Task HandleAddressUpdate(DriverValue driverFromDb, DriverUpdateModel updateModel)
    {
        if (updateModel.Address == null) return; // If no address update is provided, return

        var addressUpdate = updateModel.Address;
        var addressUpdateValue = _mapper.Map<AddressValue>(addressUpdate);

        AddressValue addressEntity;

        if (addressUpdate.AddressId.HasValue)
        {
            // If AddressId is provided, check if other drivers share the same address
            var existingAddress = await _context.Addresses.FindAsync(addressUpdate.AddressId.Value);
            if (existingAddress != null && AreAddressesEqual(addressUpdateValue, existingAddress))
            {
                // The provided address is the same as the existing one, do nothing
                driverFromDb.AddressID = existingAddress.AddressID;
                return;
            }
            if (existingAddress != null)
            {
                addressUpdate.AddressId = null; // Clear the AddressId to create a new Address

                // If shared, create a new Address and update the Driver's AddressId
                addressEntity = _mapper.Map<AddressValue>(addressUpdate);
                await _context.Addresses.AddAsync(addressEntity);
                await _context.SaveChangesAsync();
                driverFromDb.AddressID = addressEntity.AddressID; // Assign the generated ID
            }
            else
            {
                // If not shared, update the existing Address entity
                if (existingAddress != null)
                {
                    _mapper.Map(addressUpdate, existingAddress);
                    // Note: SaveChangesAsync will be called later to save this update
                }
            }
        }
        else
        {
            // If AddressId is not provided, also still check for existing Address
            var existingAddress = await _context.Addresses.FirstOrDefaultAsync(a =>
                a.Street == addressUpdate.Street &&
                a.HouseNumber == addressUpdate.HouseNumber &&
                a.City == addressUpdate.City &&
                a.ZipCode == addressUpdate.ZipCode);

            if (existingAddress != null)
            {
                driverFromDb.AddressID = existingAddress.AddressID; // Assign the existing AddressId
                return;
            }

            // If not create a new Address
            addressEntity = _mapper.Map<AddressValue>(addressUpdate);
            await _context.Addresses.AddAsync(addressEntity);
            await _context.SaveChangesAsync();
            driverFromDb.AddressID = addressEntity.AddressID; // Assign the generated ID
        }
    }

    private bool AreAddressesEqual(AddressValue address1, AddressValue address2)
    {
        if (address1 == null || address2 == null)
        {
            return false;
        }

        // Compare the properties of the two addresses
        return string.Equals(address1.Street, address2.Street, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(address1.HouseNumber, address2.HouseNumber, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(address1.City, address2.City, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(address1.ZipCode, address2.ZipCode, StringComparison.OrdinalIgnoreCase);
    }


    private async Task HandleVehicleUpdate(DriverValue driverFromDb, DriverUpdateModel updateModel)
    {
        if (updateModel.Vehicle == null) return; // If no vehicle update is provided, return

        var vehicleUpdate = updateModel.Vehicle;
        if (vehicleUpdate.VehicleId.HasValue)
        {
            // If VehicleId is provided, link the existing vehicle to the driver
            var existingVehicle = await _context.Vehicles.FindAsync(vehicleUpdate.VehicleId.Value);
            if (existingVehicle != null)
            {
                var driverVehicle = new DriverVehicleValue
                {
                    DriverID = driverFromDb.DriverID,
                    VehicleID = existingVehicle.VehicleID,
                    StartDate = DateTime.Now
                };
                await _context.DriverVehicles.AddAsync(driverVehicle);
                // Note: SaveChangesAsync will be called later to save this addition
            }
        }
        else
        {
            // If VehicleId is not provided, create a new vehicle and link it to the driver
            var newVehicle = _mapper.Map<VehicleValue>(vehicleUpdate);

            // Check if the provided FuelType and VehicleType IDs exist in the database
            if (vehicleUpdate.FuelType != null && vehicleUpdate.FuelType.FuelTypeID != null)
            {
                var existingFuelType = await _context.FuelTypes.FindAsync(vehicleUpdate.FuelType.FuelTypeID);
                if (existingFuelType != null)
                {
                    newVehicle.FuelTypeID = existingFuelType.FuelTypeID;
                    newVehicle.FuelType = existingFuelType;
                }
                else
                {
                    // Handle the case where the provided FuelType ID does not exist
                    throw new ArgumentException("FuelType ID provided does not exist.");
                }
            }

            if (vehicleUpdate.VehicleType != null && vehicleUpdate.VehicleType.VehicleTypeID != null)
            {
                var existingVehicleType = await _context.VehicleTypes.FindAsync(vehicleUpdate.VehicleType.VehicleTypeID);
                if (existingVehicleType != null)
                {
                    newVehicle.VehicleTypeID = existingVehicleType.VehicleTypeID;
                    newVehicle.VehicleType = existingVehicleType;
                }
                else
                {
                    // Handle the case where the provided VehicleType ID does not exist
                    throw new ArgumentException("VehicleType ID provided does not exist.");
                }
            }

            await _context.Vehicles.AddAsync(newVehicle);
            await _context.SaveChangesAsync();

            var driverVehicle = new DriverVehicleValue
            {
                DriverID = driverFromDb.DriverID,
                VehicleID = newVehicle.VehicleID, // Use the generated VehicleID
                StartDate = DateTime.Now
            };
            await _context.DriverVehicles.AddAsync(driverVehicle);
            // Note: SaveChangesAsync will be called later to save this addition
        }
    }

    private async Task HandleFuelCardUpdate(DriverValue driverFromDb, DriverUpdateModel updateModel)
    {
        if (updateModel.FuelCard == null) return; // Controleert of er een brandstofkaartupdate is

        var fuelCardUpdate = updateModel.FuelCard; // Haalt de bijgewerkte brandstofkaart op

        var newFuelCard = _mapper.Map<FuelCardValue>(fuelCardUpdate); // Maakt een nieuwe brandstofkaart aan op basis van de update
        _context.FuelCards.Add(newFuelCard); // Voegt de nieuwe brandstofkaart toe aan de database
        await _context.SaveChangesAsync(); // Slaat de wijzigingen op

        if (fuelCardUpdate.FuelCardId.HasValue) // Controleert of er een brandstofkaart-ID is opgegeven in de update
        {
            var existingFuelCard = await _context.FuelCards.FindAsync(fuelCardUpdate.FuelCardId.Value); // Zoekt een bestaande brandstofkaart op basis van ID
            if (existingFuelCard != null) // Controleert of de bestaande brandstofkaart gevonden is
            {
                var driverFuelCard = new DriverFuelCardValue // Maakt een koppeling tussen de chauffeur en de bestaande brandstofkaart
                {
                    DriverID = driverFromDb.DriverID,
                    FuelCardID = existingFuelCard.FuelCardID,
                    StartDate = DateTime.Now
                };
                await _context.DriverFuelCards.AddAsync(driverFuelCard); // Voegt de koppeling toe aan de database
            }
        }
        else
        {
            var newAcceptedFuels = new List<FuelCardAcceptedFuelsValue>(); // Maakt een lijst voor nieuwe geaccepteerde brandstoffen

            if (fuelCardUpdate.AcceptedFuels != null) // Controleert of geaccepteerde brandstoffen zijn opgegeven in de update
            {
                foreach (var fuelType in fuelCardUpdate.AcceptedFuels) // Itereert door de geaccepteerde brandstoffen in de update
                {
                    var acceptedFuelValue = _mapper.Map<FuelCardAcceptedFuelsValue>(fuelType); // Maakt een nieuwe geaccepteerde brandstof aan

                    var existingFuelType = await _context.FuelTypes.FindAsync(fuelType.FuelType.FuelTypeID); // Zoekt een bestaand brandstoftype op basis van ID
                    if (existingFuelType != null) // Controleert of het bestaande brandstoftype is gevonden
                    {
                        // Koppelt het bestaande brandstoftype en de nieuwe brandstofkaart aan de nieuwe geaccepteerde brandstof
                        acceptedFuelValue.FuelTypeId = existingFuelType.FuelTypeID;
                        acceptedFuelValue.FuelType = existingFuelType;

                        acceptedFuelValue.FuelCardId = newFuelCard.FuelCardID; // Koppelt de nieuwe brandstofkaart aan de geaccepteerde brandstof
                        acceptedFuelValue.FuelCard = newFuelCard;

                        newAcceptedFuels.Add(acceptedFuelValue); // Voegt de nieuwe geaccepteerde brandstof toe aan de lijst
                    }
                    else
                    {
                        // Handelt het geval af waarbij het opgegeven FuelType-ID niet bestaat
                        throw new ArgumentException("FuelType ID provided does not exist.");
                    }
                }
                newFuelCard.AcceptedFuels = newAcceptedFuels; // Koppelt de nieuwe geaccepteerde brandstoffen aan de nieuwe brandstofkaart
            }

            var driverFuelCard = new DriverFuelCardValue // Maakt een koppeling tussen de chauffeur en de nieuwe brandstofkaart
            {
                DriverID = driverFromDb.DriverID,
                FuelCardID = newFuelCard.FuelCardID,
                StartDate = DateTime.Now
            };
            await _context.DriverFuelCards.AddAsync(driverFuelCard); // Voegt de koppeling toe aan de database
        }
        await _context.SaveChangesAsync(); // Slaat eventuele wijzigingen op
    }


    public async Task DeleteDriver(Driver driver)
    {
        try
        {
            // Fetch the existing DriverValue from the database.
            var existingDriverValue = await _context.Drivers.FindAsync(driver.DriverId);

            if (existingDriverValue != null)
            {
                //_context.Drivers.Remove(existingDriverValue);
                existingDriverValue.DeletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            throw new DriverException("Failed to delete driver " + driver.FirstName+" " + driver.LastName, ex);
        }
    }

    public async Task<List<Driver>> GetFilteredDrivers(string filter)
    {
        try
        {
            var filteredDrivers = await _context.Drivers
                .Include(d => d.Address)
                .Include(d => d.Licenses)
                    .ThenInclude(dl => dl.LicenseType)
                .Include(d => d.Vehicles)
                    .ThenInclude(dv => dv.Vehicle)
                        .ThenInclude(v => v.FuelType)
                .Include(d => d.Vehicles)
                    .ThenInclude(dv => dv.Vehicle)
                        .ThenInclude(v => v.VehicleType)
                .Include(d => d.FuelCards)
                    .ThenInclude(df => df.FuelCard)
                        .ThenInclude(fc => fc.AcceptedFuels)
                            .ThenInclude(fca => fca.FuelType)
                .Where(driver =>
                    (driver.FirstName.Contains(filter) || driver.LastName.Contains(filter)) && driver.DeletedAt == null)
                .ToListAsync();

            var mappedDrivers = filteredDrivers.Select(driverValue =>
            {
                var driver = _mapper.Map<Driver>(driverValue);
                driver.Address = _mapper.Map<Address>(driverValue.Address);
                driver.Vehicles = _mapper.Map<List<Vehicle>>(driverValue.Vehicles.Select(v => v.Vehicle).ToList());
                driver.FuelCards = _mapper.Map<List<FuelCard>>(driverValue.FuelCards.Select(fc => fc.FuelCard).ToList());
                driver.Licenses = driverValue.Licenses
                    .Select(dl => _mapper.Map<DriverLicense>(dl))
                    .ToList();

                return driver;
            }).ToList();

            return mappedDrivers;
        }
        catch (Exception ex)
        {
            throw new DriverException("Failed to retrieve filtered drivers.", ex);
        }
    }

}
