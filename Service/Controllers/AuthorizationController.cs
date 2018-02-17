using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.Login;
using Service.Auth;
using Service.Repositories.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Controllers
{
    [Produces("application/json")]
    [Route("api/v0/[controller]")]
    public class AuthorizationController : Controller
    {
        public IUsersRepository _usersRepository { get; }

        public AuthorizationController(IUsersRepository _usersRepository)
        {
            this._usersRepository = _usersRepository;
        }

        [HttpGet]
        public async void Index()
        {
            var result = await _usersRepository.GetUsers();
        }

        [HttpPost("login")]
        public async Task<JsonResult> Login([FromBody] UserLoginModel model)
        {
            var user = await _usersRepository.GetUserByLogin(model.Name, model.Password);

            if (user != null)
            {
                var now = DateTime.UtcNow;
                // создаем JWT-токен
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                return Json(new { access_token = encodedJwt, profile_type = user.ProfileType });
            }

            return new JsonResult(null){ StatusCode = 404};
        }

        [HttpPost("registration")]
        public async Task<StatusCodeResult> Registration([FromBody] RegistrationUserModel model)
        {
            var user = await _usersRepository.GetUserByLogin(model.Name, model.Password);

            if (user == null)
            {
                var result = await _usersRepository.AddUser(model.Name, model.Password, false);
                if (result)
                    return new OkResult();
                else
                    return new BadRequestResult();
            }

            return new BadRequestResult();
        }
    }
}
