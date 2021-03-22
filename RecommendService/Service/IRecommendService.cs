using System;
using System.Threading.Tasks;

namespace RecommendService.Service
{
    public interface IRecommendService
    {
        Task<Object> GetAllRecommendedBooks(string token);
    }
}
