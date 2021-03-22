using FavouriteService.Models;
using System.Collections.Generic;

namespace FavouriteService.Repository
{
    public interface IFavouriteRepository
    {
        Favourite AddFavourite(Favourite favourite);
        bool DeleteFavourite(string bookId, string userName);
        Favourite GetFavouriteByBookIdUserName(string bookId, string userName);
        List<Favourite> GetAllFavouritesByUserName(string userName);
        List<Favourite> GetAllFavourites();
    }
}
