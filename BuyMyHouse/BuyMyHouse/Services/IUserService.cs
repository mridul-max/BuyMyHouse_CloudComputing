using BuyMyHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyMyHouse.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(Guid id);
        Task AddUser(User user);
        Task<User> UpdateUser(User user);
        Task UpdateUsers(IEnumerable<User> users);
    }
}
