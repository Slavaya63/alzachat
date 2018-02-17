using System;
using System.Net.Http;
using System.Threading.Tasks;
using AlzaMobile.Core.Repositories.Interfaces;
using Flurl.Http;

namespace AlzaMobile.Core.Repositories.Implementation
{
    public class LoginRepository : ILoginRepository
    {
        private static string Url = Settings.BaseUrl + "/api/v0/authorization/login";

        public async Task<HttpResponseMessage> Login(string login, string password)
        {
            try
            {
                return await Url.AllowAnyHttpStatus()
                                .PostJsonAsync(new { login = login, password = password });
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }
    }
}
