
using Gerenciador_Financeiro.Domains.Domains.Expense;
using Gerenciador_Financeiro.Domains.Domains.Expense.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Gerenciador_Financeiro.API.Controllers
{
    [Route("v1/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private IExpenseService _expenseService;
        public ExpenseController(IExpenseService service)
        {
            _expenseService = service;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SaveExpense([FromBody] Expense Expense)
        {
            try
            {
                _expenseService.Save(Expense);
                return Ok(new { message = "Nova despesa inserida!", status = "success"});
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateExpense([FromBody] Expense expense)
        {
            try
            {
                _expenseService.Update(expense);
                return Ok(new { message = "Despesa alterada com sucesso!", status = "success" });
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllsExpense()
        {
            try
            {
                return Ok(_expenseService.GetAllsExpense().Result);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteExpense(string id)
        {
            try
            {
                _expenseService.Delete(id);
                return Ok(new { message = "Despesa deletada com sucesso!", status = "success" });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetExpenseById(string id)
        {
            try
            {
                return Ok(_expenseService.GetExpenseById(id).Result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("name/{value}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetExpenseByName(string value)
        {
            try
            {
                return Ok(_expenseService.GetExpenseByName(value).Result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
