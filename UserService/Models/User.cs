using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserService.Models
{
    public class User
    {
        #region Properties


        [BsonId]
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string College { get; set; }

        public  string DOB { get; set; }
        public string Contact { get; set; }
        public DateTime AddedDate { get; set; }
        #endregion


    }
}
