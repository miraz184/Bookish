using AuthenticationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Repository
{
     public interface IAuthRepository
    {
        bool CreateUser(User user);
        bool IsUserExists(string userId);
        bool LoginUser(User user);

    }
}
