using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.Common.Exceptions
{
    public class FuelCardException : Exception
    {
        public FuelCardException(string? message) : base(message)
        {
        }

        public FuelCardException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
