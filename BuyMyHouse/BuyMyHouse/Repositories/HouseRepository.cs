using BuyMyHouse.DataAccess;
using BuyMyHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyMyHouse.Repositories
{
    public class HouseRepository : IHouseRepository
    {
        private IBuyMyHouseDbContext _buyMyHouseDbContext { get; }

        public HouseRepository(IBuyMyHouseDbContext buyMyHouseDbContext)
        {
            _buyMyHouseDbContext = buyMyHouseDbContext;
        }

        public IEnumerable<House> GetHouses()
        {
            var houses = _buyMyHouseDbContext.Houses.ToList();
            return houses;
        }

        public House GetHouseById(Guid Id)
        {
            return _buyMyHouseDbContext.Houses.Find(Id);
        }

        public IEnumerable<House> GetHousesByPriceRange(int priceFrom, int priceTo)
        {
            return _buyMyHouseDbContext.Houses.Where(h => priceFrom < h.Price && h.Price < priceTo).ToList();
        }

        public async Task<int> AddHouse(House house)
        {
            _buyMyHouseDbContext.Houses.Add(house);
            var savedHouse = await _buyMyHouseDbContext.SaveChangesAsync();
            return savedHouse;
        }
    }
}
