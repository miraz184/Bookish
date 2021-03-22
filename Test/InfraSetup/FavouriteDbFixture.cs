using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using FavouriteService.Models;
using MongoDB.Driver;

namespace Test.InfraSetUp
{
    public class FavouriteDbFixture : IDisposable
    {
        private IConfigurationRoot configuration;
        public FavouriteContext context;
        public FavouriteDbFixture()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

            configuration = builder.Build();
            context = new FavouriteContext(configuration);
            context.Favourites.DeleteMany(Builders<Favourite>.Filter.Empty);
            context.Favourites.InsertMany(new List<Favourite>
            {
                new Favourite{ BookId=111, Title="A Manual of the Art of Fiction", Author="Clayton Meeker Hamilton", Category="Fiction", CreatedBy="Mukesh", CreationDate=DateTime.Now },
                 new Favourite{ BookId=222, Title="Healing Arts: The History of Art Therapy", Author="Susan Hogan", Category="Arts", CreatedBy="Mukesh", CreationDate=DateTime.Now }
            });
        }
        public void Dispose()
        {
            context = null;
        }
    }
}
