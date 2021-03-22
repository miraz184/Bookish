using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecommendService.Models
{
    public class Recommend
    {
        #region Properties
        [BsonId]
        public ObjectId Id { get; set; }
        public string BookId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public int Count { get; set; }
        public string Author { get; set; }
        #endregion
    }
}
