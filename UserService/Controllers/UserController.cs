using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Exceptions;
using UserService.Models;
using UserService.Service;
//using MongoAndMVC.Models;
using MongoDB.Bson;
using MongoDB.Driver;
//using MongoDB.Driver.Builders;
//using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
//using System.Web.Mvc;

namespace UserService.Controllers
{
    #region Annotation 
    /*
   As in this assignment, we are working with creating RESTful web service to create microservices, hence annotate
   the class with [ApiController] annotation and define the controller level route as per REST Api standard.
   */
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    #endregion
    public class UserController : Controller
    {
        #region Variable
        IUserService _userService;
        #endregion

        #region Constructor Injection
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region HttpGet GetUserById
        [HttpGet]
        [Route("{userId}")]
        public ActionResult Get(string userId)
        {
            try
            {
                var userIdStatus = _userService.GetUserById(userId);
                return Ok(userIdStatus);
            }
            catch (UserNotFoundException unfx)
            {
                return NotFound(unfx.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        #endregion

        #region HttpPost Create
        [HttpPost]
        public ActionResult Create([FromBody] User user)
        {
            try
            {
                var userStatus = _userService.RegisterUser(user);
                return Created("/api/user", userStatus);
            }
            catch (UserNotCreatedException unce)
            {
                return Conflict(unce.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
           
        }
        #endregion

        #region HttpPut Update
        [HttpPut]
        [Route("{userId}")]
        public ActionResult Update(string userId, [FromBody] User user)
        {
            try
            {
                var userStatus = _userService.UpdateUser(userId, user);
                return Ok(user);
            }
            catch (UserNotFoundException unfe)
            {
                return NotFound(unfe.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        #endregion

        #region HttpDelete Delete
        [HttpDelete]
        [Route("{userId}")]
        public ActionResult Delete(string userId)
        {
            try
            {
                var userStatus = _userService.DeleteUser(userId);
                return Ok(userStatus);
            }
            catch (UserNotFoundException unfe)
            {
                return NotFound(unfe.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        #endregion
         }
}
