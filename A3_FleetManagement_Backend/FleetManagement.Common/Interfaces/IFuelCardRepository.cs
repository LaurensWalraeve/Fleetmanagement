using FleetManagement.Common.Models;

namespace FleetManagement.Common.Repositories.Interfaces;

public interface IFuelCardRepository
{
    Task<List<FuelCard>> GetAllFuelCards();
    Task<List<FuelCard>> GetFilteredFuelcards(string filter);
    Task<int?> GetFuelcardRelation(int FuelcardID);
    Task<FuelCard> GetFuelCardById(int id);
    Task<List<FuelType>> GetAllFuelTypes();
    Task CreateFuelCard(FuelCard fuelCard);
    Task UpdateFuelCard(FuelCard fuelCard);
    Task DeleteFuelCard(int id);
}