using FavouriteService.Models;
using System;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace FavouriteService.Repository
{
    public class FavouriteRepository : IFavouriteRepository
    {
        // Variable for constructor injection
        FavouriteContext favouriteContext;

        #region Constructor Injection
        public FavouriteRepository(FavouriteContext favouriteContext)
        {
            this.favouriteContext = favouriteContext;
        }
        #endregion

        #region public Favourite AddFavourite(Favourite favourite)
        public Favourite AddFavourite(Favourite favourite)
        {
            try
            {
                favouriteContext.Favourites.InsertOne(favourite);
                return favourite;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region public bool DeleteFavourite(string bookId, string userName)
        public bool DeleteFavourite(string bookId, string userName)
        {
            try
            {
                favouriteContext.Favourites.DeleteOne(f => f.BookId == bookId && f.CreatedBy == userName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region public List<Favourite> GetAllFavourites()
        public List<Favourite> GetAllFavourites()
        {
            try
            {
                return favouriteContext.Favourites.Find(books => true).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region public List<Favourite> GetAllFavouritesByUserName(string userName)
        public List<Favourite> GetAllFavouritesByUserName(string userName)
        {
            try
            {
                var favouriteList = favouriteContext.Favourites.Find(u => u.CreatedBy == userName).ToList();
                return favouriteList;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region public Favourite GetFavouriteByBookIdUserName(string bookId, string userName)
        public Favourite GetFavouriteByBookIdUserName(string bookId, string userName)
        {
            try
            {
                var favourite = favouriteContext.Favourites.Find(b => b.BookId == bookId && b.CreatedBy == userName).FirstOrDefault();
                return favourite;
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
