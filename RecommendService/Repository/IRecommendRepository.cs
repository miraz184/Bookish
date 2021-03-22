using RecommendService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecommendService.Repository
{
    public interface IRecommendRepository
    {
        List<Recommend> GetRecommendedBooks();
        Task<Object> GetAllRecommendedBooks(string token);
    }
}
