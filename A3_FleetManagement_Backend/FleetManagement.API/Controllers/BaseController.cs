using Microsoft.AspNetCore.Authorization;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.API.Controllers
{
    [ApiController]
    //TODO [Authorize]
    public class BaseController : ControllerBase
    {
        protected ILogger<BaseController> Logger { get; }

        public BaseController(ILogger<BaseController> logger)
        {
            this.Logger = logger;
        }

        protected OkObjectResult Ok(object obj)
        {
            return base.Ok(obj);
        }

        protected OkObjectResult Ok<TResult>(TResult result)
            where TResult : IResult
        {
            return base.Ok(result);
        }

        protected IActionResult CustomResponse(ValidationResult result)
        {
            if (result == null || result.IsValid)
                return base.Ok(result);

            return base.BadRequest(result);
        }
    }
}
