using AuthenticationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Services
{
    public interface IAuthService
    {
        bool LoginUser(User user);
        bool RegisterUser(User user);


    }
}
