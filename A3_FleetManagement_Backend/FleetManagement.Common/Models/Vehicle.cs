using System;
using System.Collections.Generic;
using System.Linq;
using FleetManagement.Common.Exceptions;
using FleetManagement.Common.Models;

public class Vehicle
{
    private string _chassisNumber;
    private string _make;
    private string _model;
    private string? _color;
    private int? _numberOfDoors;
    private string _licensePlate;
    private FuelType _fuelType;
    private VehicleType _vehicleType;
    private static List<Vehicle> _vehicles = new List<Vehicle>();

    public Vehicle(int vehicleId, string make, string model, string chassisNumber, string licensePlate,
        FuelType fuelType, VehicleType vehicleType, string? color, int? numberOfDoors)
    {
        VehicleId = vehicleId;
        Make = make;
        Model = model;
        ChassisNumber = chassisNumber;
        LicensePlate = licensePlate;
        FuelType = fuelType;
        VehicleType = vehicleType;
        Color = color;
        NumberOfDoors = numberOfDoors;
        CreatedAt = DateTime.UtcNow; // Set the creation time to the current UTC time
    }

    public Vehicle() { }

    public DateTime? DeletedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; private set; }

    public int VehicleId { get; set; }

    public string Make
    {
        get => _make;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ValidationException("Make cannot be null or consist of only whitespace.");
            _make = value;
        }
    }

    public string Model
    {
        get => _model;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ValidationException("Model cannot be null or consist of only whitespace.");
            _model = value;
        }
    }

    public FuelType FuelType
    {
        get => _fuelType;
        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "FuelType cannot be null.");
            _fuelType = value;
        }
    }

    public VehicleType VehicleType
    {
        get => _vehicleType;
        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "VehicleType cannot be null.");
            _vehicleType = value;
        }
    }

    public string ChassisNumber
    {
        get => _chassisNumber;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ValidationException("Chassis number cannot be null or consist of only whitespace.");

            if (IsChassisNumberAlreadyExists(value))
                throw new DuplicateEntryException("A vehicle with this chassis number already exists");

            _chassisNumber = value;
        }
    }

    public string LicensePlate
    {
        get => _licensePlate;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ValidationException("Number plate cannot be null or consist of only whitespace.");

            if (IsLicensePlateAlreadyExists(value))
                throw new DuplicateEntryException("A vehicle with this license plate already exists");

            _licensePlate = value.ToUpper();
        }
    }

    public string? Color { get; set; }

    public int? NumberOfDoors
    {
        get => _numberOfDoors;
        set
        {
            if (value.HasValue && (value < 1 || value > 5))
                throw new ValidationException("Vehicle cannot have less than 1 door or more than 5 doors.");
            _numberOfDoors = value;
        }
    }

    public static bool TryAddVehicle(Vehicle vehicle, out Exception? exception)
    {
        exception = null;
        try
        {
            if (vehicle == null)
            {
                exception = new ArgumentNullException(nameof(vehicle));
                return false;
            }

            if (IsVehicleAlreadyExists(vehicle))
            {
                exception = new DuplicateEntryException("This vehicle already exists");
                return false;
            }

            _vehicles.Add(vehicle);
            return true;
        }
        catch (Exception ex)
        {
            exception = ex;
            return false;
        }
    }

    public static bool TryRemoveVehicle(Vehicle vehicle, out Exception? exception)
    {
        exception = null;
        try
        {
            if (vehicle == null)
            {
                exception = new ArgumentNullException(nameof(vehicle));
                return false;
            }

            if (!_vehicles.Remove(vehicle))
            {
                exception = new ValidationException("This vehicle doesn't exist");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            exception = ex;
            return false;
        }
    }

    public static bool IsChassisNumberAlreadyExists(string chassisNumber)
    {
        return _vehicles.Any(vehicle => vehicle.ChassisNumber == chassisNumber);
    }

    public static bool IsLicensePlateAlreadyExists(string licensePlate)
    {
        return _vehicles.Any(vehicle => vehicle.LicensePlate == licensePlate.ToUpper());
    }

    public static bool IsVehicleAlreadyExists(Vehicle vehicle)
    {
        return _vehicles.Contains(vehicle);
    }
}
