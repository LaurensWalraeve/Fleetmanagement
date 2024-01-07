using FleetManagement.API.CreationModels;
using FleetManagement.Common.Exceptions;
using FleetManagement.Common.Models;
using FleetManagement.Common.Models.Update;
using FleetManagement.Common.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace FleetManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriverController : BaseController
{
    private readonly IDriverService _driverService;

    public DriverController(ILogger<DriverController> logger, IDriverService driverService) : base(logger)
    {
        _driverService = driverService;
    }

    // Haalt alle chauffeurs op
    [HttpGet]
    public async Task<IActionResult> GetAllDrivers()
    {
        try
        {
            var drivers = await _driverService.GetAllDrivers();
            return Ok(drivers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Haalt het aantal chauffeurs op
    [HttpGet]
    [Route("Count")]
    public async Task<IActionResult> GetAllDriversCount()
    {
        try
        {
            var drivers = await _driverService.GetAllDrivers();
            return Ok(drivers.Count());
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Haalt alle typen rijbewijzen op
    [HttpGet]
    [Route("LicenseTypes")]
    public async Task<IActionResult> GetAllLicenseTypes()
    {
        try
        {
            var licenseTypes = await _driverService.GetAllLicenseTypes();
            return Ok(licenseTypes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Haalt een chauffeur op basis van het opgegeven ID
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetDriverById(int id)
    {
        try
        {
            var driver = await _driverService.GetDriverById(id);
            if (driver == null) return NotFound();
            return Ok(driver);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Filtert chauffeurs op basis van een opgegeven filterwaarde
    [HttpGet("filter")]
    public async Task<IActionResult> GetFilteredDrivers([FromQuery] string filter)
    {
        try
        {
            var drivers = await _driverService.GetFilteredDrivers(filter);
            return Ok(drivers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }
    // Creëert een nieuwe chauffeur
    [HttpPost]
    public async Task<IActionResult> CreateDriver([FromBody] Driver driver)
    {
        try
        {
            if (driver == null) return BadRequest();
            return Ok(await _driverService.CreateDriver(driver));
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Werkt een bestaande chauffeur bij op basis van het opgegeven ID
    [HttpPatch]
    [Route("{id}")]
    [SwaggerRequestExample(typeof(DriverUpdateModel), typeof(DriverUpdateModelExample))]
    public async Task<IActionResult> UpdateDriver(int id, JObject driverJson)
    {
        if (driverJson == null)
        {
            return BadRequest("Invalid input data.");
        }

        try
        {
            var updatedDriver = await _driverService.UpdateDriver(id, driverJson);
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
            // Log the exception here (use a proper logging framework)
            return StatusCode(500, "Internal Server Error " + ex.Message + ex.InnerException.Message);
        }
    }

    // Verwijdert een chauffeur op basis van het opgegeven ID
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteDriver(int id)
    {
        try
        {
            var driver = await _driverService.GetDriverById(id);
            if (driver == null) return NotFound();

            await _driverService.DeleteDriver(driver);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

}