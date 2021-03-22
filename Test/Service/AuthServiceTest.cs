using AuthenticationService.Exceptions;
using AuthenticationService.Models;
using AuthenticationService.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Testing.Service
{
   public class AuthServiceTest
    {
        public void RegisterUserShouldSuccess()
        {
            var mockRepo = new Mock<IAuthRepository>();
            User user = new User { UserName = "Sachin", Password = "admin123" };
            mockRepo.Setup(repo => repo.IsUserExists(user.UserName)).Returns(false);
            mockRepo.Setup(repo => repo.CreateUser(user)).Returns(true);
            var service = new AuthenticationService.Services.AuthService(mockRepo.Object);

            var actual = service.RegisterUser(user);
            Assert.True(actual);
        }

       // [Fact, TestPriority(2)]
        public void LoginUserShouldSuccess()
        {
            var mockRepo = new Mock<IAuthRepository>();
            User user = new User { UserName = "Sachin", Password = "admin123" ,LastName = "Tendulker",FirstName="Sachin"};
            mockRepo.Setup(repo => repo.LoginUser(user)).Returns(true);
            var service = new AuthenticationService.Services.AuthService(mockRepo.Object);

            var actual = service.LoginUser(user);
            Assert.True(actual);
        }

        //[Fact, TestPriority(3)]
        public void RegisterUserShouldFail()
        {
            var mockRepo = new Mock<IAuthRepository>();
            User user = new User { UserName = "Mukesh", Password = "admin123" ,LastName="Ayyar",FirstName="Mukesh"};
            mockRepo.Setup(repo => repo.IsUserExists(user.UserName)).Returns(true);
            var service = new AuthenticationService.Services.AuthService(mockRepo.Object);

            var actual = Assert.Throws<UserAlreadyExistsException>(() => service.RegisterUser(user));
            Assert.Equal($"This userId {user.UserName} already in use", actual.Message);
        }
    }
}
