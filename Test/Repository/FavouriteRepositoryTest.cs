using FavouriteService.Models;
using FavouriteService.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Test.InfraSetUp;
using Xunit;

namespace Test.Repository
{
    [TestCaseOrderer("Test.PriorityOrderer", "Test")]
    public class FavouriteRepositoryTest : IClassFixture<FavouriteDbFixture>
    {
        private IFavouriteRepository repository;

        public FavouriteRepositoryTest(FavouriteDbFixture _fixture)
        {
            repository = new FavouriteRepository(_fixture.context);
        }
        [Fact, TestPriority(1)]
        public void CreateFavouriteShouldReturnFavourite()
        {
            Favourite favourite = new Favourite { BookId = 333, Title = "Othello", Author = "William Shakespeare", Category = "Tragedy", CreatedBy = "Mukesh", CreationDate = DateTime.Now };

            var actual = repository.AddFavourite(favourite);
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<Favourite>(actual);
            Assert.Equal(333, actual.BookId);
        }

        [Fact, TestPriority(2)]
        public void GetFavouriteByUserShouldReturnListOfFavourites()
        {
            var actual = repository.GetAllFavouritesByUserName("Mukesh");
            Assert.IsAssignableFrom<List<Favourite>>(actual);
            Assert.Contains(actual, c => c.Title == "A Manual of the Art of Fiction");
        }

        [Fact, TestPriority(3)]
        public void DeleteFavouriteShouldReturnTrue()
        {
            var actual = repository.DeleteFavourite(333, "Mukesh");
            Assert.True(actual);
        }
    }
}
