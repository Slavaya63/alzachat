using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Models;

namespace Service.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        Task<UserModel> GetUser(string userId);

        Task<IEnumerable<UserModel>> GetUsers();

        Task<UserModel> GetUserByLogin(string login, string password);

        Task<bool> AddUser(string login, string password, bool isConsultant);

        Task<bool> RemoveAll();
    }
}
