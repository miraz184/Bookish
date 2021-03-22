using FavouriteService.Exceptions;
using FavouriteService.Models;
using FavouriteService.Repository;
using System.Collections.Generic;

namespace FavouriteService.Service
{
    public class FavouriteService : IFavouriteService
    {
        // Variable for constructor injection
        IFavouriteRepository favouriteRepository;
        #region Constructor Injection
        public FavouriteService(IFavouriteRepository favouriteRepository)
        {
            this.favouriteRepository = favouriteRepository;
        }
        #endregion

        #region public Favourite AddFavourite(Favourite favourite)
        public Favourite AddFavourite(Favourite favourite)
        {
            var userFav = favouriteRepository.GetFavouriteByBookIdUserName(favourite.BookId, favourite.CreatedBy);
            if (userFav == null)
            {
                return favouriteRepository.AddFavourite(favourite);
            }
            else
            {
                throw new FavouriteBookNotAddedException("This favourite book already exists");
            }
        }
        #endregion

        #region public bool DeleteFavourite(string bookId, string userName)
        public bool DeleteFavourite(string bookId, string userName)
        {
            if (favouriteRepository.GetFavouriteByBookIdUserName(bookId, userName) != null)
            {
                var deleteStatus = favouriteRepository.DeleteFavourite(bookId, userName);
                if (deleteStatus)
                {
                    return deleteStatus;
                }
                else
                {
                    throw new FavouriteBookNotFoundException("This favourite book not found for this userName");
                }
            }
            else
            {
                throw new FavouriteBookNotFoundException("This favourite book not found for this userName");
            }
        }
        #endregion

        #region public List<Favourite> GetAllFavourites()
        public List<Favourite> GetAllFavourites()
        {
            var favourites = favouriteRepository.GetAllFavourites();
            if (favourites == null)
            {
                throw new FavouriteBookNotFoundException("Cannot Find The Favourite Books");
            }
            else
            {
                return favourites;
            }
        }
        #endregion

        #region public List<Favourite> GetAllFavouritesByUserName(string userName)
        public List<Favourite> GetAllFavouritesByUserName(string userName)
        {
            var favourites = favouriteRepository.GetAllFavouritesByUserName(userName);
            if (favourites != null)
            {
                return favourites;
            }
            else
            {
                throw new FavouriteBookNotFoundException("This favourite book not found for this userName");
            }
        }
        #endregion
    }
}
