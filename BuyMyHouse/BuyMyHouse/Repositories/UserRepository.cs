using BuyMyHouse.DataAccess;
using BuyMyHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyMyHouse.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IBuyMyHouseDbContext _buyMyHouseDbContext { get; }

        public UserRepository(IBuyMyHouseDbContext buyMyHouseDbContext)
        {
            _buyMyHouseDbContext = buyMyHouseDbContext;
        }
        public async Task<int> AddUser(User user)
        {
            _buyMyHouseDbContext.Users.Add(user);
            var result = await _buyMyHouseDbContext.SaveChangesAsync();
            return result;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = _buyMyHouseDbContext.Users.ToList();
            return users;
        }

        public async Task<User> GetUserById(Guid Id)
        {
            return _buyMyHouseDbContext.Users.Find(Id);
        }

        public async Task<User> UpdateUser(User user)
        {
            _buyMyHouseDbContext.Users.Update(user);
            await _buyMyHouseDbContext.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUsers(IEnumerable<User> users)
        {
            _buyMyHouseDbContext.Users.UpdateRange(users);
            await _buyMyHouseDbContext.SaveChangesAsync();
        }

    }
}
