using FavouriteService.Models;
using System.Collections.Generic;

namespace FavouriteService.Service
{
    public interface IFavouriteService
    {
        Favourite AddFavourite(Favourite favourite);
        bool DeleteFavourite(string bookId, string userName);
        List<Favourite> GetAllFavouritesByUserName(string userName);
        List<Favourite> GetAllFavourites();
    }
}
