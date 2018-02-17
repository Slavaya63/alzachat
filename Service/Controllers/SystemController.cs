using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Repositories.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Controllers
{
    [Route("api/v0/[controller]")]
    public class SystemController : Controller
    {
        public IUsersRepository _usersRepository { get; }

        public SystemController(IUsersRepository _usersRepository)
        {
            this._usersRepository = _usersRepository;
        }


        /// <summary>
        /// Init fake datas
        /// </summary>
        /// <returns>The init.</returns>
        [HttpGet("init")]
        public string Init()
        {
            try
            {
                _usersRepository.RemoveAll();

                _usersRepository.AddUser("79999999999", "123", true);
                _usersRepository.AddUser("78888888888", "123", true);
                _usersRepository.AddUser("77777777777", "123", false);

            }
            catch (Exception ex)
            {
                return "Fail!";
            }

            return "Done!";
        }
    }
}
