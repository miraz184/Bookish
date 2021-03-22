using UserService.Models;

namespace UserService.Repository
{
    public interface IUserRepository
    {
        User RegisterUser(User user);
        bool UpdateUser(string userName, User user);
        bool DeleteUser(string userName);
        User GetUserById(string userName);
    }
}
