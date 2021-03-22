using AuthenticationService.Models;
using AuthenticationService.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Testing.InfraSetup;
using Xunit;

namespace Testing.Repository
{
    class AuthRepositoryTest: IClassFixture<AuthDbFixture>
    {
        private IAuthRepository repository;
        public AuthRepositoryTest(AuthDbFixture fixture)
        {
            repository = new AuthRepository(fixture.context);
        }
        #region positivetests
       // [Fact, TestPriority(1)]
        public void CreateUserShouldSuccess()
        {
            User user = new User { UserName = "Sachin", Password = "admin123", LastName = "Tendulker", FirstName = "Sachin" };

            var actual = repository.CreateUser(user);
            Assert.True(actual);

            var isExists = repository.IsUserExists(user.UserName);
            Assert.True(isExists);
        }

       // [Fact, TestPriority(2)]
        public void LoginUserShouldSuccess()
        {
            User user = new User {  UserName = "Sachin", Password = "admin123" ,LastName = "Tendulker",FirstName="Sachin"};

            var actual = repository.LoginUser(user);
            Assert.True(actual);
        }
        #endregion positivetests


        #region negativetests
       // [Fact, TestPriority(3)]
        public void LoginUserShouldFail()
        {
            User user = new User { UserName = "John", Password = "admin123", };

            var actual = repository.LoginUser(user);
            Assert.False(actual);
        }
        #endregion negativetests
    }
}
