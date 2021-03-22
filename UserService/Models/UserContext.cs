using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace UserService.Models
{
    public class UserContext
    {
        #region Mongo Variable
        //declare variable to connect to MongoDB database
        MongoClient mongoClient;
        IMongoDatabase database;
        #endregion
        public UserContext(IConfiguration configuration)
        {
            #region Environment
            //mongoClient = new MongoClient(configuration.GetSection("MongoDB:ConnectionString").Value);
            mongoClient = new MongoClient(Environment.GetEnvironmentVariable("MONGO_USTCONNECTION"));
            database = mongoClient.GetDatabase(configuration.GetSection("MongoDB:UserDatabase").Value);
            #endregion
        }
        #region Mongo Collection
        //Define a MongoCollection to represent the Users collection of MongoDB
        public IMongoCollection<User> Users => database.GetCollection<User>("users");
        #endregion
    }
}
