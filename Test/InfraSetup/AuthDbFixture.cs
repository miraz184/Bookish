using AuthenticationService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testing.InfraSetup
{
    public class AuthDbFixture : IDisposable
    {
        public AuthDbContext context;
        public AuthDbFixture()
        {
            //var options = new DbContextOptionsBuilder<AuthDbContext>()
            //    .UseInMemoryDatabase(databaseName: "AuthDB")
            //    .Options;

            //Initializing DbContext with InMemory
            //context = new AuthDbContext(options);

            // Insert seed data into the database using one instance of the context
            context.users.Add(new User { UserName = "Mukesh", Password = "admin123", LastName = "Ayyar", FirstName = "Mukesh" });
            context.SaveChanges();
        }
        public void Dispose()
        {
            context = null;
        }
    }
}
