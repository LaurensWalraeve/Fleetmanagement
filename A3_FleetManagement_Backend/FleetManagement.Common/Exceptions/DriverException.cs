using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.Common.Exceptions
{
    public class DriverException : Exception
    
    {
        public DriverException(string message, Exception innerException)
        {
        }
    }
}
