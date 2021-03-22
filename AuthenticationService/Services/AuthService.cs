using AuthenticationService.Exceptions;
using AuthenticationService.Models;
using AuthenticationService.Repository;

namespace AuthenticationService.Services
{
    public class AuthService : IAuthService
    {
         private IAuthRepository repository;
        public AuthService(IAuthRepository _repository)
        {
            repository = _repository;
        }
        public bool LoginUser(User user)
        {
            var loginInfo = repository.LoginUser(user);
            if(loginInfo)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RegisterUser(User user)
        {
            var userstatus = repository.IsUserExists(user.username);
            if(!userstatus)
            {
                repository.CreateUser(user);
                return true;
            }
            else
            {
                throw new UserAlreadyExistsException($"This userId {user.username} already in use");
            }
        }
    }
}
