using Gerenciador_Financeiro.Domains.Domains.User;
using Gerenciador_Financeiro.Domains.Domains.User.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Gerenciador_Financeiro.API.Controllers
{
    //[AllowAnonymous]
    [Route("v1/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Save([FromBody] User user)
        {
            try
            {
                if(user == null)
                return BadRequest();
                _userService.Save(user);
                return Ok(new { message = "Usuário cadastrado com sucesso!", status = "success" });
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                return Ok(_userService.GetUsers().Result);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateUsers([FromBody] User user)
        {
            try
            {
                _userService.Update(user);
                return Ok(new { message = "Usuário alterado com sucesso!"});
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                _userService.Delete(id);
                return Ok(new { message = "Usuário deletado com sucesso!", status = "success" });
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
