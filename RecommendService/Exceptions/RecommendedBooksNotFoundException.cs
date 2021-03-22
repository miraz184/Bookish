using System;

namespace RecommendService.Exceptions
{
    public class RecommendedBooksNotFoundException : ApplicationException
    {
        public RecommendedBooksNotFoundException() { }
        public RecommendedBooksNotFoundException(string message) : base(message) { }
    }
}
