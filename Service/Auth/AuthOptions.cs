using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Service.Auth
{
    public class AuthOptions
    {
        public const string ISSUER = "Service";
        public const string AUDIENCE = "Mobile"; 
        const string KEY = "secrect_word_54321";  
        public const int LIFETIME = 60; 

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
