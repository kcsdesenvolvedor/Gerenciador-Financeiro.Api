using Gerenciador_Financeiro.Domains.Domains.Demostrative.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gerenciador_Financeiro.API.Controllers
{
    [Route("v1/[controller]")]
    public class DemonstrativeController : ControllerBase
    {
        private IDemonstrativeService _demonstrativeService;
        public DemonstrativeController(IDemonstrativeService demonstrativeService)
        {
            _demonstrativeService = demonstrativeService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllsDemonstrative()
        {
            try
            {
                return Ok(_demonstrativeService.GetAllsDemonstrative().Result);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{interval1}/{interval2}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDemonstrativeByInterval(string interval1, string interval2)
        {
            try
            {
                return Ok(_demonstrativeService.GetDemonstrativeByInterval(interval1, interval2).Result);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
