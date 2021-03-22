using AuthenticationService.Exceptions;
using AuthenticationService.Models;
using AuthenticationService.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Variables
        private IAuthService _authService;
        private ITokenGenerator _tokenGenerator;
        #endregion'

        public AuthController(IAuthService authService,ITokenGenerator tokenGenerator)
        {
            _authService = authService;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost]
        [Route("register")]
        public ActionResult Register([FromBody] User user)
        {
            try
            {
                var userStatus = _authService.RegisterUser(user);
                return Created("api/auth/register", userStatus);
            }
            catch (UserAlreadyExistsException uaex)
            {
                return Conflict(uaex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }



        [HttpPost]
        [Route("login")]
        public ActionResult Login([FromBody] User user)
        {
            try
            {
                if (_authService.LoginUser(user))
                {
                    var token = _tokenGenerator.JWTToken(user.username);
                    //string[] a = new string[] { token, user.username };
                    return Ok(token);
                }
                else
                {
                    return StatusCode(401, "Invalid user id or password");
                }

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpPost]
        [Route("isAuthenticated")]
        public IActionResult IsAuthenticated()
        {
            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
            bool isAuthenticated = false;
            if (authHeader.StartsWith("Bearer"))
            {
                string token = authHeader.Substring(7);
                isAuthenticated = Startup.IsTokenValid(token);
            }

            Dictionary<string, bool> result = new Dictionary<string, bool>();
            result.Add("isAuthenticated", isAuthenticated);
            return Ok(result);
        }


    }
}
