using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Service.Models
{
    public class ProfileModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
