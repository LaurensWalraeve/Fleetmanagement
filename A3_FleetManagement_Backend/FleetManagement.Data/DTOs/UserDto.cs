using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.Data.DTOs
{
    public class UserDto
    {
        public string Password { get; set; }
        public string Username { get; set; }
        public int RoleID { get; set; }
        public int DriverID { get; set; }
    }
}
