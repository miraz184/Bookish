using AuthenticationService.Models;
using FavouriteService.Models;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Xunit;

namespace Test.Controller
{
    [Collection("Auth API")]
    [TestCaseOrderer("Test.PriorityOrderer", "Test")]
    public class FavouriteControllerTest : IClassFixture<FavouriteWebApplicationFactory<FavouriteService.Startup>>
    {
        private readonly HttpClient _client, _authclient;
        /*public FavouriteControllerTest(FavouriteWebApplicationFactory<FavouriteService.Startup> factory, AuthWebApplicationFactory<AuthenticationService.Startup> authFactory)
        {
            //calling Auth API to get JWT
           /* User user = new User { UserName = "Mukesh", Password = "admin123" };
            _authclient = authFactory.CreateClient();
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = _authclient.PostAsync<User>("/api/auth/login", user, formatter);
            httpResponse.Wait();
            // Deserialize and examine results.
            var stringResponse = httpResponse.Result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<TokenModel>(stringResponse.Result);

            _client = factory.CreateClient();
            //Attaching token in request header
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {response.Token}");
        }*/

        #region positivetests
        [Fact, TestPriority(1)]
        public async Task GetByUserNameShouldSuccess()
        {
            // The endpoint or route of the controller action.
            string userName = "Mukesh";
            var httpResponse = await _client.GetAsync($"/api/favourite/{userName}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var favourites = JsonConvert.DeserializeObject<IEnumerable<Favourite>>(stringResponse);
            Assert.Contains(favourites, f => f.BookId == 111);
        }

        [Fact, TestPriority(2)]
        public async Task CreateFavouriteShouldSuccess()
        {
            Favourite favourite = new Favourite { BookId = 444, Title = "Macbeth", Author = "William Shakespeare", Category = "Tragedy", CreatedBy = "Mukesh" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PostAsync<Favourite>("/api/favourite", favourite, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Favourite>(stringResponse);
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
            Assert.IsAssignableFrom<Favourite>(response);
        }

        [Fact, TestPriority(3)]
        public async Task DeleteFavouriteShouldSuccess()
        {
            int BookId = 444;
            var userName = "Mukesh";
            // The endpoint or route of the controller action.
            var httpResponse = await _client.DeleteAsync($"/api/favourite/{BookId}/{userName}");

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.True(Convert.ToBoolean(stringResponse));
        }
        #endregion positivetests

        #region negativetests       

        [Fact, TestPriority(4)]
        public async Task DeleteFavouriteShouldFail()
        {
            int bookId = 104;
            var userName = "Raj";
            // The endpoint or route of the controller action.
            var httpResponse = await _client.DeleteAsync($"/api/favourite/{bookId}/{userName}");

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.Equal($"This favourite book not found for this userName", stringResponse);
        }

        #endregion negativetests
    }
}