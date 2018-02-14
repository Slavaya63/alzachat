using System;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Service.Models;

namespace Service.Contexts
{
    public class UserContext
    {
        private readonly IMongoDatabase _database = null;

        public UserContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<UserModel> UserModel => _database.GetCollection<UserModel>("users");
    }
}
