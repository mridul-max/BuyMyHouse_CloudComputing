using BuyMyHouse.Model;
using BuyMyHouse.Repositories;

namespace BuyMyHouse.Services
{
    public class HouseService : IHouseService
    {
        private readonly IHouseRepository _HouseRepository;

        public HouseService(IHouseRepository houseRepository)
        {
            _HouseRepository = houseRepository;
        }

        public IEnumerable<House> GetHouses()
        {
            return _HouseRepository.GetHouses();
        }

        public House GetHouseById(Guid id)
        {
            return _HouseRepository.GetHouseById(id);
        }

        public IEnumerable<House> GetHousesBetweenPrice(int priceFrom, int priceTo)
        {
            return _HouseRepository.GetHousesByPriceRange(priceFrom, priceTo);
        }

        public async Task<int> AddHouse(House house)
        {
            house.Id = Guid.NewGuid();
            return await _HouseRepository.AddHouse(house);
        }
    }
}
