using System;

namespace FavouriteService.Exceptions
{
    public class FavouriteBookNotAddedException: ApplicationException
    {
        public FavouriteBookNotAddedException() { }
        public FavouriteBookNotAddedException(string message) : base(message) { }
    }
}
