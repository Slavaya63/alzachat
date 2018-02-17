using System;
using System.Threading.Tasks;
using AlzaMobile.Models;

namespace AlzaMobile.Core.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        Task<LoginModel> Login(string login, string password);
    }
}
