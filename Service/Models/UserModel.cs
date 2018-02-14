using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Service.Models
{
    public class UserModel
    {
        [BsonRepresentation(BsonType.ObjectId)] 
        public string Id { get; set; }

        public string ProfileType { get; set; } = string.Empty;

        public ProfileModel Profile { get; set; } = new ProfileModel();

    }
}
