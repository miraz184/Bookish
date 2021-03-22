using AuthenticationService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testing
{
    
    
        public class AuthWebApplicationFactory<TStartup> : WebApplicationFactory<AuthenticationService.Startup>
        {
            protected override void ConfigureWebHost(IWebHostBuilder builder)
            {
                builder.ConfigureServices(services =>
                {
                    // Create a new service provider.
                    //var serviceProvider = new ServiceCollection()
                    //    .AddEntityFrameworkInMemoryDatabase()
                    //    .BuildServiceProvider();

                    // Add a database context (KeepNoteContext) using an in-memory database for testing.
                    services.AddDbContext<AuthDbContext>(options =>
                    {
                        //options.UseInMemoryDatabase("InMemoryAuthDB");
                        //options.UseInternalServiceProvider(serviceProvider);
                    });

                    // Build the service provider.
                    var sp = services.BuildServiceProvider();

                    // Create a scope to obtain a reference to the database contexts
                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var authDb = scopedServices.GetRequiredService<AuthDbContext>();

                        var logger = scopedServices.GetRequiredService<ILogger<AuthWebApplicationFactory<TStartup>>>();

                        // Ensure the database is created.
                        authDb.Database.EnsureCreated();

                        try
                        {
                            // Seed the database with some specific test data.
                            authDb.users.Add(new AuthenticationService.Models.User { UserName = "Mukesh", Password = "admin123", LastName = "Ayyar", FirstName = "Mukesh" });
                            authDb.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, "An error occurred seeding the " +
                                                "database with test messages. Error: {ex.Message}");
                        }
                    }
                });
            }
        }
    }

