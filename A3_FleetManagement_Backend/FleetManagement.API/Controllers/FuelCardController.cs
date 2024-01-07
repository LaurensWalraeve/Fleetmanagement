using FleetManagement.Common.Services;
using FleetManagement.Common.Services.Interfaces;
using FleetManagement.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FleetManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FuelCardController : BaseController
{
    private readonly IFuelCardService _fuelCardService;

    public FuelCardController(ILogger<FuelCardController> logger, IFuelCardService fuelCardService) : base(logger)
    {
        _fuelCardService = fuelCardService;
    }

    // Haalt alle brandstofkaarten op
    [HttpGet]
    public async Task<IActionResult> GetAllFuelCards()
    {
        try
        {
            var fuelCards = await _fuelCardService.GetAllFuelCards();
            return Ok(fuelCards);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Haalt de relatie op tussen een brandstofkaart en zijn gerelateerde entiteit
    [HttpGet("relations/{id}")]
    public async Task<IActionResult> GetFuelCardRelation(int id)
    {
        try
        {
            var fuelcardRelation = await _fuelCardService.GetFuelcardRelation(id);
            return Ok(fuelcardRelation);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Filtert brandstofkaarten op basis van een opgegeven filterwaarde
    [HttpGet("filter")]
    public async Task<IActionResult> GetFilteredFuelCards([FromQuery] string filter)
    {
        try
        {
            var fuelcards = await _fuelCardService.GetFilteredFuelcards(filter);
            return Ok(fuelcards);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }
    // Haalt het aantal brandstofkaarten op
    [HttpGet]
    [Route("Count")]
    public async Task<IActionResult> GetAllFuelCardsCount()
    {
        try
        {
            var fuelCards = await _fuelCardService.GetAllFuelCards();
            return Ok(fuelCards.Count());
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Haalt een brandstofkaart op basis van het opgegeven ID
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetFuelCardById(int id)
    {
        try
        {
            var fuelCard = await _fuelCardService.GetFuelCardById(id);
            if (fuelCard == null) return NotFound();
            return Ok(fuelCard);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Haalt alle brandstoftypes op
    [HttpGet]
    [Route("FuelTypes")]
    public async Task<IActionResult> GetAllFuelTypes()
    {
        try
        {
            var fuelTypes = await _fuelCardService.GetAllFuelTypes();
            return Ok(fuelTypes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Maakt een nieuwe brandstofkaart aan
    [HttpPost]
    public async Task<IActionResult> CreateFuelCard([FromBody] FuelCard fuelCard)
    {
        try
        {
            await _fuelCardService.CreateFuelCard(fuelCard);
            return CreatedAtAction(nameof(GetFuelCardById), new { id = fuelCard.FuelCardId }, fuelCard);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Werkt een bestaande brandstofkaart bij op basis van het opgegeven ID
    [HttpPatch]
    [Route("{id}")]
    public async Task<IActionResult> UpdateFuelCard(int id, [FromBody] FuelCard fuelCard)
    {
        try
        {
            if (id != fuelCard.FuelCardId) return BadRequest();
            await _fuelCardService.UpdateFuelCard(fuelCard);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    // Verwijdert een brandstofkaart op basis van het opgegeven ID
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteFuelCard(int id)
    {
        try
        {
            await _fuelCardService.DeleteFuelCard(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

}