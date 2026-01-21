using GestaoFinanceiraApi.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GestaoFinanceiraApi.Controllers
{
    [ApiController]
    [Route("financas/resumo")]
    public class ResumoFinanceiroController : ControllerBase

    {

        private readonly IResumoFinanceiroService _service;


        public ResumoFinanceiroController(IResumoFinanceiroService service)
        {
            _service = service;
        }





        [HttpGet("mensal/{usuarioId:long}")]
        public async Task<IActionResult> ObterResumoMensal(
            long usuarioId,
            [FromQuery] int ano,
            [FromQuery] int mes)
        {
            var resumo = await _service.ObterResumoMensalAsync(usuarioId, ano, mes);
            return Ok(resumo);
        }






        [HttpGet("mensal-atual/{usuarioId:long}")]
        public async Task<IActionResult> ObterResumoMensalAtual(long usuarioId)
        {
            var resumo = await _service.ObterResumoMensalAtualAsync(usuarioId);
            return Ok(resumo);
        }





        [HttpGet("total/{usuarioId:long}")]
        public async Task<IActionResult> ObterResumoTotal(long usuarioId)
        {
            var resumo = await _service.ObterResumoTotalAsync(usuarioId);
            return Ok(resumo);
        }




    }
}
