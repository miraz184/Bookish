using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FavouriteService.Models
{
    public class Favourite
    {
        #region Properties
        [BsonId]       
        public ObjectId Id { get; set; }
        public string BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string CreatedBy { get; set; }
        #endregion
    }
}
