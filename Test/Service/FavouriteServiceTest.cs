using FavouriteService.Exceptions;
using FavouriteService.Models;
using FavouriteService.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Test.Service
{
    [TestCaseOrderer("Test.PriorityOrderer", "Test")]
    public class FavouriteServiceTest
    {
        #region Positive tests
        [Fact, TestPriority(1)]
        public void CreateFavouriteShouldReturnFavourite()
        {
            var mockRepo = new Mock<IFavouriteRepository>();
            Favourite favourite = new Favourite { BookId = 121, Title = "Indefinites Between Latin and Romance", Author = "Chiara Gianollo", Category = "Romance", CreatedBy = "Mukesh", CreationDate = DateTime.Now };
            mockRepo.Setup(repo => repo.GetAllFavouritesByUserName("Mukesh")).Returns(this.GetFavourites());
            mockRepo.Setup(repo => repo.AddFavourite(favourite)).Returns(favourite);
            var service = new FavouriteService.Service.FavouriteService(mockRepo.Object);

            var actual = service.AddFavourite(favourite);

            Assert.NotNull(actual);
            Assert.IsAssignableFrom<Favourite>(actual);
        }

        [Fact, TestPriority(2)]
        public void GetFavouriteByUserShouldReturnListOfFavourites()
        {
            var mockRepo = new Mock<IFavouriteRepository>();
            var userName = "Mukesh";
            mockRepo.Setup(repo => repo.GetAllFavouritesByUserName(userName)).Returns(this.GetFavourites());
            var service = new FavouriteService.Service.FavouriteService(mockRepo.Object);

            var actual = service.GetAllFavouritesByUserName(userName);

            Assert.IsAssignableFrom<List<Favourite>>(actual);
        }

        [Fact, TestPriority(3)]
        public void DeleteCategoryShouldReturnTrue()
        {
            var mockRepo = new Mock<IFavouriteRepository>();
            var BookId = 222;
            var userName = "Mukesh";
            mockRepo.Setup(repo => repo.DeleteFavourite(BookId,userName)).Returns(true);
            var service = new FavouriteService.Service.FavouriteService(mockRepo.Object);

            var actual = service.DeleteFavourite(BookId,userName);
            Assert.True(actual);
        }

        private List<Favourite> GetFavourites()
        {
            List<Favourite> favourites = new List<Favourite> {
                new Favourite{BookId=111, Title="A Manual of the Art of Fiction", Author="Clayton Meeker Hamilton", Category="Fiction", CreatedBy="Mukesh", CreationDate=new DateTime() },
                 new Favourite{BookId=222, Title="Healing Arts: The History of Art Therapy", Author="Susan Hogan", Category="Arts", CreatedBy="Mukesh", CreationDate=new DateTime() }
            };

            return favourites;
        }

        #endregion Positive tests

        #region Negative tests

        [Fact, TestPriority(6)]
        public void CreateCategoryShouldThrowException()
        {
            var mockRepo = new Mock<IFavouriteRepository>();
            Favourite favourite = new Favourite { BookId = 111, Title = "A Manual of the Art of Fiction", Author = "Clayton Meeker Hamilton", Category = "Fiction", CreatedBy = "Mukesh", CreationDate = DateTime.Now };
            List<Favourite> favourites = new List<Favourite>();
            mockRepo.Setup(repo => repo.GetAllFavouritesByUserName("Mukesh")).Returns(this.GetFavourites());
            var service = new FavouriteService.Service.FavouriteService(mockRepo.Object);

            var actual = Assert.Throws<FavouriteBookNotAddedException>(() => service.AddFavourite(favourite));
            Assert.Equal("This favourite book already exists", actual.Message);
        }


        [Fact, TestPriority(7)]
        public void GetCategoryByUserShouldReturnEmptyList()
        {
            var mockRepo = new Mock<IFavouriteRepository>();
            var userName = "Nitin";
            mockRepo.Setup(repo => repo.GetAllFavouritesByUserName(userName)).Returns(new List<Favourite>());
            var service = new FavouriteService.Service.FavouriteService(mockRepo.Object);

            var actual = service.GetAllFavouritesByUserName(userName);

            Assert.IsAssignableFrom<List<Favourite>>(actual);
            Assert.Empty(actual);
        }

        [Fact, TestPriority(8)]
        public void DeleteCategoryShouldThrowException()
        {
            var mockRepo = new Mock<IFavouriteRepository>();
            var BookId = 105;
            var userName = "Raj";
            mockRepo.Setup(repo => repo.DeleteFavourite(BookId, userName)).Returns(false);
            var service = new FavouriteService.Service.FavouriteService(mockRepo.Object);


            var actual = Assert.Throws<FavouriteBookNotFoundException>(() => service.DeleteFavourite(BookId,userName));

            Assert.Equal("This favourite book not found for this userName", actual.Message);
        }
        
        #endregion Negative tests
    }
}
