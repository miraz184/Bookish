using MongoDB.Driver;
using Newtonsoft.Json;
using RecommendService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RecommendService.Repository
{
    public class RecommendRepository : IRecommendRepository
    {
        // Variable for constructor injection
        RecommendContext recommendContext;

        // dynamic variable to store the values from FavouriteService D
        dynamic res = "";

        #region Constructor Injection
        public RecommendRepository(RecommendContext recommendContext)
        {
            this.recommendContext = recommendContext;
        }
        #endregion

        #region public List<Recommend> GetRecommendedBooks()
        public List<Recommend> GetRecommendedBooks()
        {
            try
            {
                var recommendList = recommendContext.Recommends.Find(c=>c.Count>3).Sort(Builders <Recommend>.Sort.Descending("Count")).ToList();
                return recommendList;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region public async Task<Object> GetAllRecommendedBooks()
        public async Task<Object> GetAllRecommendedBooks(string token)
        {
            using (var httpClient = new HttpClient())
            {
                //Attaching token in request header
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                using (var response = await httpClient.GetAsync("http://localhost:8088/api/Favourite/getFavouritesFromFavouriteService/"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject(apiResponse);
                }
                
                IEnumerable<dynamic> sequence = res;
                List<dynamic> favList = sequence.ToList();
                
                recommendContext.Recommends.DeleteMany(books => true);
                foreach (var f in favList)
                {
                    
                    string bookId = f["bookId"];
                    var rec = recommendContext.Recommends.Find(b => b.BookId == bookId).FirstOrDefault();
                    if( rec!= null)
                    {
                        int count = rec.Count + 1;
                        var upadate = Builders<Recommend>.Update.Set("Count", count);
                        recommendContext.Recommends.UpdateOne(r => r.BookId == bookId, upadate);
                    }
                    else
                    {
                        Recommend recommend = new Recommend();
                        recommend.BookId = f["bookId"];
                        recommend.Title = f["title"];
                        recommend.Author = f["author"];
                        recommend.Category = f["category"];
                        recommend.Count = 1;
                        recommendContext.Recommends.InsertOne(recommend);
                    }                    
                }
                List<Recommend> recommends = GetRecommendedBooks();
                return recommends;
            }
        }
        #endregion
    }
}
