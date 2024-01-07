using FleetManagement.Common.Models.Update;
using Swashbuckle.AspNetCore.Filters;

namespace FleetManagement.API.CreationModels
{
    public class UserUpdateModelExample : IExamplesProvider<UserUpdateModel>
    {
        public UserUpdateModel GetExamples()
        {
            return new UserUpdateModel
            {
                Username = "exampleUsername",
                Password = "examplePassword",
                RoleId = 1,
                DriverId = 2
            };
        }
    }
}
