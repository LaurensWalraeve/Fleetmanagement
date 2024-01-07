using FleetManagement.API.CreationModels;
using FleetManagement.Common.Exceptions;
using FleetManagement.Common.Models;
using FleetManagement.Common.Models.Update;
using FleetManagement.Common.Services;
using FleetManagement.Common.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace FleetManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : BaseController
{
    private readonly IUserService _userService;
    private readonly IDriverService _driverService;

    public UserController(ILogger<UserController> logger, IUserService userService, IDriverService driverService) : base(logger)
    {
        _userService = userService;
        _driverService = driverService;
    }
    // Logt een gebruiker in op basis van de verstrekte inloggegevens
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LogUserIn([FromBody] LoginModel loginModel)
    {
        try
        {
            var user = await _userService.LogUserIn(loginModel.UserName, loginModel.Password);
            if (user == null) return NotFound();
            return Ok(user.Role.RoleID);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Haalt alle gebruikers op
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Filtert gebruikers op basis van een opgegeven filterwaarde
    [HttpGet("filter/{filter}")]
    public async Task<IActionResult> GetFilteredUsers(string filter)
    {
        try
        {
            var users = await _userService.GetFilteredUsers(filter);
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Haalt een gebruiker op basis van de opgegeven gebruikersnaam
    [HttpGet]
    [Route("{userName}")]
    public async Task<IActionResult> GetUserByUserName(string userName)
    {
        try
        {
            var user = await _userService.GetUserByUserName(userName);
            if (user == null) return NotFound();
            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Maakt een nieuwe gebruiker aan
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserCreationModel user)
    {
        try
        {
            if (user == null) return BadRequest();

            // Converteer UserCreationModel naar User
            var driver = await _driverService.GetDriverById(user.DriverID);
            var role = await _userService.GetUserRole(user.RoleID);
            var newUser = new User(user.Password, user.Username, role, driver);

            // Verwerk het model als het geldig is
            if (ModelState.IsValid)
            {
                await _userService.CreateUser(newUser);
                return Ok("Gebruiker is aangemaakt.");
            }
            return BadRequest("Ongeldige invoer.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Werkt een bestaande gebruiker bij op basis van het opgegeven ID
    [HttpPatch]
    [Route("{id}")]
    [SwaggerRequestExample(typeof(UserUpdateModel), typeof(UserUpdateModelExample))]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] JObject userJson)
    {
        if (userJson == null)
        {
            return BadRequest("Invalid input data.");
        }
        try
        {
            var updatedDriver = await _userService.UpdateUser(id, userJson);
            return Ok(updatedDriver);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error" + ex.Message);
        }
    }

    // Verwijdert een gebruiker op basis van de opgegeven gebruikersnaam
    [HttpDelete]
    [Route("{userName}")]
    public async Task<IActionResult> DeleteDriver(string userName)
    {
        try
        {
            var user = await _userService.GetUserByUserName(userName);
            if (user == null) return NotFound();

            await _userService.DeleteUser(user);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

}