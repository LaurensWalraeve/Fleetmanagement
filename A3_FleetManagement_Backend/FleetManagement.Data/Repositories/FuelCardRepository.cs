using AutoMapper;
using FleetManagement.Common.Models;
using FleetManagement.Data.Entities;
using FleetManagement.Common.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using FleetManagement.Common.Exceptions;

public class FuelCardRepository : IFuelCardRepository
{
    private readonly FleetDbContext _context;
    private readonly IMapper _mapper;

    public FuelCardRepository(FleetDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<FuelCard>> GetAllFuelCards()
    {
        try
        {
            var fuelCards = await _context.FuelCards
                .Include(f => f.AcceptedFuels)
                .ThenInclude(fac => fac.FuelType)
                .Where(f => f.DeletedAt == null)
                .ToListAsync();

            return _mapper.Map<List<FuelCard>>(fuelCards);
        }
        catch (Exception ex)
        {
            throw new FuelCardException("Error while getting all fuelcards",ex) ;
        }
    }


    public async Task<List<FuelCard>> GetFilteredFuelcards(string filter)
    {
        try
        {
            var filterFuelCards = await _context.FuelCards
                .Include(f => f.AcceptedFuels)
                .ThenInclude(fac => fac.FuelType)
                .Where(fuelcard =>
                    fuelcard.CardNumber.Contains(filter) && fuelcard.DeletedAt == null)
                .ToListAsync();

            return _mapper.Map<List<FuelCard>>(filterFuelCards);
        }
        catch (Exception ex)
        {
            throw new FuelCardException("Error while getting filtered fuelcards", ex);
        }
    }

    public async Task<FuelCard> GetFuelCardById(int id)
    {
        try
        {
            var fuelCardValue = await _context.FuelCards
                .Include(f => f.AcceptedFuels)
                .ThenInclude(fac => fac.FuelType)
                .FirstOrDefaultAsync(f => f.FuelCardID == id && f.DeletedAt == null);

            return _mapper.Map<FuelCard>(fuelCardValue);
        }
        catch (Exception ex)
        {
            throw new FuelCardException("Error while getting fuelcard by ID", ex);
        }
    }

    public async Task<List<FuelType>> GetAllFuelTypes()
    {
        try
        {
            var fuelTypesValue = await _context.FuelTypes.ToListAsync();
            return _mapper.Map<List<FuelType>>(fuelTypesValue);
        }
        catch (Exception ex)
        {
            throw new FuelCardException("Error while getting all fueltypes", ex);
        }
    }

    public async Task CreateFuelCard(FuelCard fuelCard)
    {
        try
        {
            var currentFuelCards = await GetAllFuelCards();
            bool doesFuelCardExist = currentFuelCards.Any(fc => fc.CardNumber == fuelCard.CardNumber);
            if (doesFuelCardExist)
            {
                throw new ArgumentException(nameof(fuelCard), "This fuelcard already exists");
            }
            var fuelCardValue = _mapper.Map<FuelCardValue>(fuelCard);
            _context.FuelCards.Add(fuelCardValue);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new FuelCardException("Error while adding fuelcard " + fuelCard.CardNumber, ex);
        }
    }

    public async Task UpdateFuelCard(FuelCard fuelCard)
    {
        try
        {
            var fuelCardValue = _mapper.Map<FuelCardValue>(fuelCard);
            _context.Entry(fuelCardValue).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new FuelCardException("Error while updating fuelcard " + fuelCard.CardNumber, ex);
        }
    }

    public async Task DeleteFuelCard(int fuelcardId)
    {
        try
        {
            var fuelCardValue = await _context.FuelCards.FindAsync(fuelcardId);
            if (fuelCardValue != null)
            {
                //_context.FuelCards.Remove(fuelCardValue);
                fuelCardValue.DeletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            var fc = await GetFuelCardById(fuelcardId);
            throw new FuelCardException("Error while deleting fuelcard " + fc.CardNumber, ex);
        }
    }

    public async Task<int?> GetFuelcardRelation(int fuelcardId)
    {
        try
        {
            var driverFuelcard = await _context.DriverFuelCards.FirstOrDefaultAsync(df => df.FuelCardID == fuelcardId);

            if (driverFuelcard != null)
            {
                return driverFuelcard.DriverID;
            }

            return null; // Geen overeenkomende driverID gevonden voor de opgegeven fuelcardID
        }
        catch (Exception ex)
        {
            var fc = await GetFuelCardById(fuelcardId);
            throw new FuelCardException("Error while getting fuelcard Relation for " + fc.CardNumber, ex);
        }
    }

}