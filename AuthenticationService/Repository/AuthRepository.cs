using AuthenticationService.Models;
using System.Linq;

namespace AuthenticationService.Repository
{
    public class AuthRepository : IAuthRepository
    {
        AuthDbContext context;
        public AuthRepository(AuthDbContext _context)
        {
            context = _context;
        }
        public bool CreateUser(User user)
        {
            context.users.Add(user);
            context.SaveChanges();
            return true;
        }

        public bool IsUserExists(string username)
        {
            var isExists = context.users.Find(username);
            if (isExists != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool LoginUser(User user)
        {
            if(context.users.Find(user.username)!=null)
            {
                context.users.FirstOrDefault(u => u.username == user.username && u.password == user.password);
                return true;
             }
            else
            {
                return false;
            }
        }
    }
}
