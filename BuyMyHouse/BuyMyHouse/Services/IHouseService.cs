using BuyMyHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyMyHouse.Services
{
    public interface IHouseService
    {
        IEnumerable<House> GetHouses();
        House GetHouseById(Guid id);
        IEnumerable<House> GetHousesBetweenPrice(int priceFrom, int priceTo);
        Task<int> AddHouse(House house);
    }
}
