using FleetManagement.Common.Services.Interfaces;
using FleetManagement.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using FleetManagement.Common.Services;

namespace FleetManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehicleController : BaseController
{
    private readonly IVehicleService _vehicleService;

    public VehicleController(ILogger<VehicleController> logger, IVehicleService vehicleService) : base(logger)
    {
        _vehicleService = vehicleService;
    }

    // Haalt alle voertuigen op
    [HttpGet]
    public async Task<IActionResult> GetAllVehicles()
    {
        try
        {
            var vehicles = await _vehicleService.GetAllVehicles();
            return Ok(vehicles);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Haalt de relatie van een voertuig op
    [HttpGet("relations/{id}")]
    public async Task<IActionResult> GetVehicleRelation(int id)
    {
        try
        {
            var vehicleRelation = await _vehicleService.GetVehicleRelation(id);
            return Ok(vehicleRelation);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Filtert voertuigen op basis van een opgegeven filterwaarde
    [HttpGet("filter")]
    public async Task<IActionResult> GetFilteredVehicles([FromQuery] string filter)
    {
        try
        {
            var vehicles = await _vehicleService.GetFilteredVehicles(filter);
            return Ok(vehicles);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Haalt alle voertuigtypes op
    [HttpGet]
    [Route("Types")]
    public async Task<IActionResult> GetAllVehicleTypes()
    {
        try
        {
            var vehicleTypes = await _vehicleService.GetAllVehicleTypes();
            return Ok(vehicleTypes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Haalt het aantal voertuigen op
    [HttpGet]
    [Route("Count")]
    public async Task<IActionResult> GetAllVehiclesCount()
    {
        try
        {
            var vehicles = await _vehicleService.GetAllVehicles();
            return Ok(vehicles.Count());
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Haalt een voertuig op basis van het opgegeven ID
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetVehicleById(int id)
    {
        try
        {
            var vehicle = await _vehicleService.GetVehicleById(id);
            if (vehicle == null) return NotFound();
            return Ok(vehicle);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Maakt een nieuw voertuig aan
    [HttpPost]
    public async Task<IActionResult> CreateVehicle([FromBody] Vehicle vehicle)
    {
        try
        {
            await _vehicleService.CreateVehicle(vehicle);
            return CreatedAtAction(nameof(GetVehicleById), new { id = vehicle.VehicleId }, vehicle);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Werkt een bestaand voertuig bij op basis van het opgegeven ID
    [HttpPatch]
    [Route("{id}")]
    public async Task<IActionResult> UpdateVehicle(int id, [FromBody] Vehicle vehicle)
    {
        try
        {
            if (id != vehicle.VehicleId) return BadRequest();
            await _vehicleService.UpdateVehicle(vehicle);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Verwijdert een voertuig op basis van het opgegeven ID
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteVehicle(int id)
    {
        try
        {
            await _vehicleService.DeleteVehicle(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

}