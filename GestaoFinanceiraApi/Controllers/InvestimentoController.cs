using GestaoFinanceiraApi.DTOs.Investimento;
using GestaoFinanceiraApi.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestaoFinanceiraApi.Controllers
{





    [ApiController]
    [Route("financas/[controller]")]
    [Authorize]
    public class InvestimentosController : ControllerBase
    {




        private readonly IInvestimentosService _investimentosService;

        public InvestimentosController(IInvestimentosService investimentosService)
        {
            _investimentosService = investimentosService;
        }






        private long ObterUsuarioId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
                throw new UnauthorizedAccessException("Usuário não autenticado");

            return long.Parse(claim.Value);
        }






        [HttpPost("criar")]
        public async Task<IActionResult> Criar([FromBody] CriarInvestimentoDTO dto)
        {
            var usuarioId = ObterUsuarioId();

            await _investimentosService.CriarAsync(dto, usuarioId);

            return Ok(new { message = "Investimento criado com sucesso" });
        }




        [HttpPost("resgatar")]
        public async Task<IActionResult> Resgatar(
        [FromBody] ResgatarInvestimentoDTO dto)
        {
            var usuarioId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); 

            await _investimentosService.ResgatarAsync(dto, usuarioId);

            return Ok(new
            {
                mensagem = "Resgate realizado com sucesso"
            });
        }





        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(long id, [FromBody] AtualizarInvestimentoDTO dto)
        {
            var usuarioId = ObterUsuarioId();

            await _investimentosService.AtualizarAsync(id, dto, usuarioId);

            return Ok(new { message = "Investimento atualizado com sucesso" });
        }

    





        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(long id)
        {
            var usuarioId = ObterUsuarioId();

            await _investimentosService.RemoverAsync(id, usuarioId);

            return Ok(new { message = "Investimento removido com sucesso" });
        }






        [HttpGet("obter")]
        public async Task<IActionResult> ObterPorUsuario()
        {
            var usuarioId = ObterUsuarioId();

            var response = await _investimentosService.ObterPorUsuarioAsync(usuarioId);

            return Ok(response);
        }






        [HttpGet("categoria/{categoria}")]
        public async Task<IActionResult> ObterPorCategoria(string categoria)
        {
            var usuarioId = ObterUsuarioId();

            var response = await _investimentosService
                .ObterPorCategoriaAsync(usuarioId, categoria);

            return Ok(response);
        }






        [HttpGet("obter/{id}")]
        public async Task<IActionResult> ObterPorId(long id)
        {
            var usuarioId = ObterUsuarioId();

            var response = await _investimentosService
                .ObterPorIdAsync(id, usuarioId);

            if (response == null)
                return NotFound();

            return Ok(response);
        }




    }
}
