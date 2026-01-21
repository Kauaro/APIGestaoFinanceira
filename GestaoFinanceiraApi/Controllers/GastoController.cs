using GestaoFinanceiraApi.DTOs.Gasto;
using GestaoFinanceiraApi.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestaoFinanceiraApi.Controllers
{
    [ApiController]
    [Route("financas/[controller]")]
    [Authorize] 
    public class GastoController : ControllerBase
    {
        private readonly IGastosService _gastosService;

        public GastoController(IGastosService gastosService)
        {
            _gastosService = gastosService;
        }

        
        
        

        [HttpPost("criar")]
        public async Task<IActionResult> CriarGasto([FromBody] CriarGastoDTO dto)
        {
            try
            {
                var usuarioId = long.Parse(
                    User.FindFirst(ClaimTypes.NameIdentifier)!.Value
                );

                var gasto = await _gastosService.CriarAsync(
                    dto.Descricao,
                    dto.Valor,
                    dto.Categoria,
                    dto.Pagamento,
                    dto.DataGasto,
                    usuarioId
                );

                return CreatedAtAction(nameof(ObterPorId), new { id = gasto.Id }, gasto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        



        [HttpPut("atualizar/{id:long}")]
        public async Task<IActionResult> AtualizarGasto(long id, [FromBody] AtualizarGastoDTO dto)
        {
            try
            {
                var usuarioId = long.Parse(
                    User.FindFirst(ClaimTypes.NameIdentifier)!.Value
                );

                await _gastosService.AtualizarAsync(
                    id,
                    dto.Descricao,
                    dto.Valor,
                    dto.Categoria,
                    dto.Pagamento,
                    dto.DataGasto
                    
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
        public async Task<IActionResult> DeletarGasto(long id)
        {
            try
            {
                

                await _gastosService.RemoverAsync(id);
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
           

            var gasto = await _gastosService.ObterPorIdAsync(id);

            if (gasto == null)
                return NotFound("Gasto não encontrado");

            return Ok(gasto);
        }

        




        [HttpGet("visualizar/todos")]
        public async Task<IActionResult> ObterTodos()
        {
            var usuarioId = long.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            var gastos = await _gastosService.ObterPorUsuarioAsync(usuarioId);
            return Ok(gastos);
        }

        




        [HttpGet("visualizar/ultimos")]
        public async Task<IActionResult> ObterUltimos([FromQuery] int quantidade = 5)
        {
            var usuarioId = long.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            var gastos = await _gastosService.ObterUltimosAsync(usuarioId, quantidade);
            return Ok(gastos);
        }

        




        [HttpGet("visualizar/periodo")]
        public async Task<IActionResult> ObterPorPeriodo(
            [FromQuery] DateTime dataInicio,
            [FromQuery] DateTime dataFim)
        {
            var usuarioId = long.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            var gastos = await _gastosService.ObterPorPeriodoAsync(usuarioId, dataInicio, dataFim);
            return Ok(gastos);
        }

        




        [HttpGet("visualizar/total-mes-atual")]
        public async Task<IActionResult> ObterTotalDoMesAtual()
        {
            var usuarioId = long.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            var total = await _gastosService.ObterTotalDoMesAtualAsync(usuarioId);
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

            var total = await _gastosService.ObterTotalPorCategoriaAsync(
                usuarioId,
                categoria,
                dataInicio,
                dataFim
            );

            return Ok(total);
        }
    }
}
