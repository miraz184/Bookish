using FavouriteService.Exceptions;
using FavouriteService.Models;
using FavouriteService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FavouriteService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {
        // Variable for constructor injection
        IFavouriteService favouriteService;
        #region Constructor Injection
        public FavouriteController(IFavouriteService favouriteService)
        {
            this.favouriteService = favouriteService;
        }
        #endregion

        #region [HttpPost]
        [HttpPost]
        public ActionResult Add([FromBody] Favourite favourite)
        {
            try
            {
                Favourite fav = favouriteService.AddFavourite(favourite);
                return Created("/api/favourite", fav);
            }
            catch (FavouriteBookNotAddedException favouriteException)
            {
                return Conflict(favouriteException.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        #endregion

        #region [HttpDelete("{bookId}/{userName}")]
        [HttpDelete("{bookId}/{userName}")]
        public ActionResult Delete(string bookId,string userName)
        {
            try
            {
                var deleteStatus = favouriteService.DeleteFavourite(bookId, userName);
                return Ok(deleteStatus);
            }
            catch (FavouriteBookNotFoundException favException)
            {
                return NotFound(favException.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        #endregion

        #region [HttpGet("{userName}")]
        [HttpGet("{userName}")]
        public ActionResult GetAllFavouritesByUserName(string userName)
        {
            try
            {
                return Ok(favouriteService.GetAllFavouritesByUserName(userName));
            }
            catch (FavouriteBookNotFoundException favException)
            {
                return NotFound(favException.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        #endregion

        #region [HttpGet("getFavouritesFromFavouriteService")]
        [HttpGet("getFavouritesFromFavouriteService")]
        public ActionResult GetAllFavourites()
        {
            try
            {
                return Ok(favouriteService.GetAllFavourites());
            }
            catch (FavouriteBookNotFoundException favException)
            {
                return NotFound(favException.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        #endregion
    }
}
