using System;
using Newtonsoft.Json;

namespace Models.Login
{
    public class BaseLoginModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
