using System.Linq;
using MongoDB.Driver;
using UserService.Models;

namespace UserService.Repository
{
    public class UserRepository:IUserRepository
    {
        #region Variale
        UserContext context;
        #endregion

        #region Constructor Injection
        public UserRepository(UserContext _context)
        {
            context = _context;
        }
        #endregion

        #region DeleteUser
        public bool DeleteUser(string userName)
        {
            var getUserId= context.Users.Find(u => u.UserName == userName).FirstOrDefault<User>();
            if (getUserId != null)
            {
                context.Users.DeleteOne(u => u.UserName == userName);
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region GetUserById
        public User GetUserById(string userName)
        {
           return context.Users.Find(u => u.UserName == userName).FirstOrDefault<User>();
            
        }
        #endregion

        #region RegisterUser
        public User RegisterUser(User user)
        {
            context.Users.InsertOne(user);
            return user;
        }
        #endregion

        #region UpdateUser
        public bool UpdateUser(string userName, User user)
        {
            var getUserId = context.Users.Find(u => u.UserName == userName).FirstOrDefault<User>();
            if (getUserId != null)
            {
                context.Users.ReplaceOne(u => u.UserName == userName, user);
                return true;
            }
            else
            {
                return false;
            }          
        }
        #endregion
    }
}
