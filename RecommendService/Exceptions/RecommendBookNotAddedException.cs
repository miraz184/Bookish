using System;

namespace RecommendService.Exceptions
{
    public class RecommendBookNotAddedException : ApplicationException
    {
        public RecommendBookNotAddedException() { }
        public RecommendBookNotAddedException(string message) : base(message) { }
    }
}
