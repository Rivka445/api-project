using Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using System.Text.Json;
namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EventDressRentalContext _eventDressRentalContext;
        public UserRepository(EventDressRentalContext eventDressRentalContext)
        {
            _eventDressRentalContext = eventDressRentalContext;
        }
        public async Task<List<User>> GetUsers()
        {
            return await _eventDressRentalContext.Users.ToListAsync();   
        }
        public async Task<User> GetUserById(int id)
        {   
            return await _eventDressRentalContext.Users.FindAsync(id);
        }
        public async Task<User> AddUser(User user)
        {
            await _eventDressRentalContext.Users.AddAsync(user);
            await _eventDressRentalContext.SaveChangesAsync();
            return user;
        }
        public async Task<User> LogIn(User user)
        {
            User? currentUser = await _eventDressRentalContext.Users.FirstOrDefaultAsync(u=> u.FirstName == user.FirstName &&
                                                                     u.Password == user.Password && u.LastName==user.LastName);
            if (currentUser !=null )
                return currentUser;
             return null;
        }
        public async Task UpdateUser(int id, User updateUser)
        {
            _eventDressRentalContext.Users.Update(updateUser);
            await _eventDressRentalContext.SaveChangesAsync();
        }
    }
}
