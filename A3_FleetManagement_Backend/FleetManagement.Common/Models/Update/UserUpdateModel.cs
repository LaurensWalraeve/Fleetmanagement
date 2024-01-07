using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.Common.Models.Update
{
    public class UserUpdateModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public int DriverId { get; set; }
    }

}
