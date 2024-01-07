using FleetManagement.Common.Models;

namespace FleetManagement.Common.Services.Interfaces;

public interface IFuelCardService
{
    Task<List<FuelCard>> GetAllFuelCards();
    Task<int?> GetFuelcardRelation(int FuelcardID);
    Task<List<FuelCard>> GetFilteredFuelcards(string filter);
    Task<FuelCard> GetFuelCardById(int id);
    Task<List<FuelType>> GetAllFuelTypes();
    Task CreateFuelCard(FuelCard fuelCard);
    Task UpdateFuelCard(FuelCard fuelCard);
    Task DeleteFuelCard(int id);
}