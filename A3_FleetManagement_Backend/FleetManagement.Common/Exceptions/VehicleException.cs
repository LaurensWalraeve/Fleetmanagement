using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.Common.Exceptions
{
    public class VehicleException : Exception
    {
        public VehicleException(string? message) : base(message)
        {
        }

        public VehicleException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
