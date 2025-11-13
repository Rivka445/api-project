using Entities;
using Repositories;
namespace Services
{
    public class UserService
    {
        public UserRipository userRipo =new UserRipository();
        public List<User> GetUsers()
        {
            return userRipo.GetUsers();
        }
        public User GetUserById(int id)
        {
            return userRipo.GetUserById(id);
        }
        public User addUser(User user)
        {
            return userRipo.addUser(user);
        }
        public User logIn(User user)
        {
            return userRipo.logIn(user);
        }
        public void updateUser(int id, User updateUser)
        {
             userRipo.updateUser(id, updateUser);   
        }

    }
}
