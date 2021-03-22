using System;

namespace FavouriteService.Exceptions
{
    public class FavouriteBookNotFoundException : ApplicationException
    {
        public FavouriteBookNotFoundException() { }
        public FavouriteBookNotFoundException(string message) : base(message) { }
    }
}
