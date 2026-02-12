
using Entities;

namespace Repositories
{
    public interface IUserRepository
    {
         Task<User?> GetUserById(int id);
         Task<List<User>> GetUsers();
         Task<User?> LogIn(User user);
         Task<User> AddUser(User user);
         Task UpdateUser(User updateUser);
    }
}