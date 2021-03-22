using RecommendService.Repository;
using System.Threading.Tasks;

namespace RecommendService.Service
{
    public class RecommendService : IRecommendService
    {
        // Variable for constructor injection
        IRecommendRepository recommendRepository;

        #region Constructor Injection
        public RecommendService(IRecommendRepository recommendRepository)
        {
            this.recommendRepository = recommendRepository;
        }
        #endregion

        #region public Task<object> GetAllRecommendedBooks()
        public Task<object> GetAllRecommendedBooks(string token)
        {
            return recommendRepository.GetAllRecommendedBooks(token);

        }
        #endregion
    }
}
