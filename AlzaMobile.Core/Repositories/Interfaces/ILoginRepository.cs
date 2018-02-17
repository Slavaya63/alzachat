using System;
using System.Threading.Tasks;

namespace AlzaMobile.Core.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        Task<System.Net.Http.HttpResponseMessage> Login(string login, string password);
    }
}
