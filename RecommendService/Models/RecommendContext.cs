using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace RecommendService.Models
{
    public class RecommendContext
    {
        //declare variables to connect to MongoDB database
        MongoClient mongoClient;
        IMongoDatabase database;

        #region public RecommendContext(IConfiguration configuration)
        public RecommendContext(IConfiguration configuration)
        {
            //Initialize MongoClient and Database using connection string and database name from configuration
            //mongoClient = new MongoClient(configuration.GetSection("MongoDB:ConnectionString").Value);
            mongoClient = new MongoClient(Environment.GetEnvironmentVariable("MONGO_USTCONNECTION"));
            database = mongoClient.GetDatabase(configuration.GetSection("MongoDB:RecommendDatabase").Value);
        }
        #endregion

        //Define a MongoCollection to represent the Recommends collection of MongoDB
        public IMongoCollection<Recommend> Recommends => database.GetCollection<Recommend>("recommends");
    }
}
