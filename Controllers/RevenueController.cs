using Gerenciador_Financeiro.Domains.Domains.Revenue;
using Gerenciador_Financeiro.Domains.Domains.Revenue.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gerenciador_Financeiro.API.Controllers
{
    [Route("v1/[controller]")]
    public class RevenueController : ControllerBase
    {
        private IRevenueService _revenueService;
        public RevenueController(IRevenueService revenueService)
        {
            _revenueService = revenueService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SaveRevenue([FromBody] Revenue revenue)
        {
            try
            {
                _revenueService.Save(revenue);
                return Ok(new { message = "Saldo adicionado com sucesso!", status = "success"});
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateRevenue([FromBody] Revenue revenue)
        {
            try
            {
                _revenueService.Update(revenue);
                return Ok(new { message = "Saldo atualizado com sucesso!", status = "success"});
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteRevenue(string id)
        {
            try
            {
                _revenueService.Delete(id);
                return Ok(new { message = "Receita deletada com sucesso!", status = "success" });
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllsRevenue()
        {
            try
            {
                return Ok(_revenueService.GetAllsRevenue().Result);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRevenueById(string id)
        {
            try
            {
                return Ok(_revenueService.GetRevenueById(id).Result);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
