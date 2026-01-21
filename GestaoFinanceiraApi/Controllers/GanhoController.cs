using GestaoFinanceiraApi.DTOs.Ganho;
using GestaoFinanceiraApi.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestaoFinanceiraApi.Controllers
{
    [ApiController]
    [Route("financas/[controller]")]
    [Authorize] 


    public class GanhoController : ControllerBase
    {
        private readonly IGanhosService _ganhosService;

        public GanhoController(IGanhosService ganhosService)
        {
            _ganhosService = ganhosService;
        }




        [HttpPost("criar")]
        public async Task<IActionResult> CriarGanho([FromBody] CriarGanhoDTO dto)
        {
            try
            {
                var usuarioId = long.Parse(
                    User.FindFirst(ClaimTypes.NameIdentifier)!.Value
                );

                var ganho = await _ganhosService.CriarAsync(
                    dto.Descricao,
                    dto.Valor,
                    dto.Categoria,
                    dto.Pagamento,
                    dto.DataGanho,
                    usuarioId
                );

                return CreatedAtAction(nameof(ObterPorId), new { id = ganho.Id }, ganho);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPut("atualizar/{id:long}")]
        public async Task<IActionResult> AtualizarGanho(long id, [FromBody] AtualizarGanhoDTO dto)
        {
            try
            {
                var usuarioId = long.Parse(
                    User.FindFirst(ClaimTypes.NameIdentifier)!.Value
                );

                await _ganhosService.AtualizarAsync(
                    id,
                    dto.Descricao,
                    dto.Valor,
                    dto.Categoria,
                    dto.Pagamento,
                    dto.DataGanho
                );

                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }





        [HttpDelete("deletar/{id:long}")]
        public async Task<IActionResult> DeletarGanho(long id)
        {
            try
            {
                

                await _ganhosService.RemoverAsync(id);
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }





        [HttpGet("visualizar/{id:long}")]
        public async Task<IActionResult> ObterPorId(long id)
        {
            

            var ganho = await _ganhosService.ObterPorIdAsync(id);

            if (ganho == null)
                return NotFound("Ganho não encontrado");

            return Ok(ganho);
        }





        [HttpGet("visualizar/todos")]
        public async Task<IActionResult> ObterTodos()
        {
            var usuarioId = long.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            var ganhos = await _ganhosService.ObterPorUsuarioAsync(usuarioId);
            return Ok(ganhos);
        }





        [HttpGet("visualizar/ultimos")]
        public async Task<IActionResult> ObterUltimos([FromQuery] int quantidade = 5)
        {
            var usuarioId = long.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            var ganhos = await _ganhosService.ObterUltimosAsync(usuarioId, quantidade);
            return Ok(ganhos);
        }





        [HttpGet("visualizar/periodo")]
        public async Task<IActionResult> ObterPorPeriodo(
            [FromQuery] DateTime dataInicio,
            [FromQuery] DateTime dataFim)
        {
            var usuarioId = long.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            var ganhos = await _ganhosService.ObterPorPeriodoAsync(usuarioId, dataInicio, dataFim);
            return Ok(ganhos);
        }





        [HttpGet("visualizar/total-mes-atual")]
        public async Task<IActionResult> ObterTotalDoMesAtual()
        {
            var usuarioId = long.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            var total = await _ganhosService.ObterTotalDoMesAtualAsync(usuarioId);
            return Ok(total);
        }





        [HttpGet("visualizar/total-categoria")]
        public async Task<IActionResult> ObterTotalPorCategoria(
            [FromQuery] string categoria,
            [FromQuery] DateTime dataInicio,
            [FromQuery] DateTime dataFim)
        {
            var usuarioId = long.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            var total = await _ganhosService.ObterTotalPorCategoriaAsync(
                usuarioId,
                categoria,
                dataInicio,
                dataFim
            );

            return Ok(total);
        }
    }
}
