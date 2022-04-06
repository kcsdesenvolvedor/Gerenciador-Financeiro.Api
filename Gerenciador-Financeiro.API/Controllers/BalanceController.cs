using Gerenciador_Financeiro.Domains.Domains.Balance.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gerenciador_Financeiro.API.Controllers
{
    [Route("v1/[controller]")]
    public class BalanceController : ControllerBase
    {
        private IBalanceService _balanceService;
        public BalanceController(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCurrentBalance()
        {
            try
            {
                return Ok(_balanceService.GetCurrentBalance().Result);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
