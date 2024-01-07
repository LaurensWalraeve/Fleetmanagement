using FleetManagement.Common.Exceptions;
using FleetManagement.Common.Services.Interfaces;
using FleetManagement.Common.Models;
using FleetManagement.Common.Repositories.Interfaces;

namespace FleetManagement.Common.Services
{
    public class FuelCardService : IFuelCardService
    {
        private readonly IFuelCardRepository _fuelCardRepository;

        public FuelCardService(IFuelCardRepository fuelCardRepository)
        {
            _fuelCardRepository = fuelCardRepository;
        }

        public async Task<List<FuelCard>> GetAllFuelCards()
        {
            try
            {
                return await _fuelCardRepository.GetAllFuelCards();
            }
            catch (Exception ex)
            {
                throw new FuelCardException(ex.Message, ex);
            }
        }

        public async Task<List<FuelCard>> GetFilteredFuelcards(string filter)
        {
            try
            {
                return await _fuelCardRepository.GetFilteredFuelcards(filter);
            }
            catch (Exception ex)
            {
                throw new FuelCardException(ex.Message, ex);
            }
        }

        public async Task<FuelCard> GetFuelCardById(int id)
        {
            try
            {
                return await _fuelCardRepository.GetFuelCardById(id);
            }
            catch (Exception ex)
            {
                throw new FuelCardException(ex.Message, ex);
            }
        }

        public async Task<List<FuelType>> GetAllFuelTypes()
        {
            try
            {
                return await _fuelCardRepository.GetAllFuelTypes();
            }
            catch (Exception ex)
            {
                throw new FuelCardException(ex.Message, ex);
            }
        }

        public async Task CreateFuelCard(FuelCard fuelCard)
        {
            try
            {
                await _fuelCardRepository.CreateFuelCard(fuelCard);
            }
            catch (Exception ex)
            {
                throw new FuelCardException(ex.Message, ex);
            }
        }

        public async Task UpdateFuelCard(FuelCard fuelCard)
        {
            try
            {
                await _fuelCardRepository.UpdateFuelCard(fuelCard);
            }
            catch (Exception ex)
            {
                throw new FuelCardException(ex.Message, ex);
            }
        }

        public async Task DeleteFuelCard(int id)
        {
            try
            {
                await _fuelCardRepository.DeleteFuelCard(id);
            }
            catch (Exception ex)
            {
                throw new FuelCardException(ex.Message, ex);
            }
        }

        public async Task<int?> GetFuelcardRelation(int FuelcardID)
        {
            try
            {
                return await _fuelCardRepository.GetFuelcardRelation(FuelcardID);
            }
            catch (Exception ex)
            {
                throw new FuelCardException(ex.Message, ex);
            }
        }
    }
}
