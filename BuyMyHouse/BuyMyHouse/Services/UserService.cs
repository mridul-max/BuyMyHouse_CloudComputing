using BuyMyHouse.Model;
using BuyMyHouse.Repositories;

namespace BuyMyHouse.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;

        public UserService(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _UserRepository.GetUsers();
        }

        public async Task<User> GetUserById(Guid userid)
        {
            return await _UserRepository.GetUserById(userid);
        }

        public async Task AddUser(User user)
        {
            user.Id = Guid.NewGuid();
            var createUser = await _UserRepository.AddUser(user);
        }

        public async Task<User> UpdateUser(User user)
        {
            return await _UserRepository.UpdateUser(user);
        }

        public async Task UpdateUsers(IEnumerable<User> users)
        {
            await _UserRepository.UpdateUsers(users);
        }
    }
}
