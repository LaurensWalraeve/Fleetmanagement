using System;
using System.Collections.Generic;

namespace FleetManagement.Common.Models
{
    public class DriverVehicle
    {
        private static List<DriverVehicle> _driverVehicles = new List<DriverVehicle>();

        public DriverVehicle(Driver driver, Vehicle vehicle, DateOnly startDate, DateOnly endDate)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver), "Driver cannot be null.");
            Vehicle = vehicle ?? throw new ArgumentNullException(nameof(vehicle), "Vehicle cannot be null.");

            // Check for null start date
            if (startDate == null)
                throw new ArgumentNullException(nameof(startDate), "Start date cannot be null.");

            // Check for null end date
            if (endDate == null)
                throw new ArgumentNullException(nameof(endDate), "End date cannot be null.");

            // Check if start date is after end date
            if (startDate > endDate)
                throw new ArgumentException("Start date cannot be after the end date.");

            // Check for date range overlap
            if (IsOverlap(this))
                throw new ArgumentException("Driver already has the same vehicle with overlapping dates.");

            StartDate = startDate;
            EndDate = endDate;

            _driverVehicles.Add(this); // Add the new DriverVehicle to the list
        }

        public Driver Driver { get; }
        public Vehicle Vehicle { get; }
        public DateOnly StartDate { get; }
        public DateOnly EndDate { get; set; }

        public bool IsOverlap(DriverVehicle newAssignment)
        {
            return _driverVehicles
                .Where(existingAssignment =>
                    existingAssignment.Driver == newAssignment.Driver &&
                    existingAssignment.Vehicle == newAssignment.Vehicle)
                .Any(existingAssignment =>
                    newAssignment.StartDate <= existingAssignment.EndDate &&
                    newAssignment.EndDate >= existingAssignment.StartDate);
        }



    }
}
