using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecommendService.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecommendService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendController : ControllerBase
    {
        // Variable for constructor injection
        IRecommendService recommendService;

        #region Constructor Injection
        public RecommendController(IRecommendService recommendService)
        {
            this.recommendService = recommendService;
        }
        #endregion

        #region [HttpGet("{token}/getRecommends")]
        [HttpGet("{token}/getRecommends")]
        public async Task<IActionResult> GetAllRecommendedBooks(string token)
        {
            var recommends = await recommendService.GetAllRecommendedBooks(token);
            return new OkObjectResult(recommends);
        }
        #endregion

    }
}
