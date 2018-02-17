using System;
using Newtonsoft.Json;

namespace AlzaMobile.Models
{
    public class LoginModel
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }

        [JsonProperty("profile_type")]
        public string ProfileType { get; set; }
    }
}
