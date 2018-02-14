using System;
using Service.Repositories.Interfaces;
using Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Service.Contexts;
using MongoDB.Driver;
using System.Linq;
using System.Diagnostics.Contracts;

namespace Service.Repositories.Implementation
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UserContext _context;

        public UsersRepository(IOptions<Settings> settings)
        {
            _context = new UserContext(settings);
        }

        public Task<UserModel> GetUser(string userId)
        {
            var filter = Builders<UserModel>.Filter.Eq("id", userId);
            return _context.UserModel
                            .Find(filter)
                            .FirstOrDefaultAsync();
        }

        public async Task<UserModel> GetUserByLogin(string login, string password)
        {
            return await _context.UserModel
                                 .Find(user => user.Profile.Login.Equals(login) && user.Profile.Password.Equals(password))
                                 .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            return await _context.UserModel.Find(item => true)
                           .ToListAsync();
        }

        public async Task<bool> AddUser(string login, string password)
        {
            await _context.UserModel.InsertOneAsync(new UserModel
                                                        { 
                                                            Profile = new ProfileModel{ Login = login, Password = password},
                                                            ProfileType = "client"
                                                        });

            return true;
        }
    }
}
