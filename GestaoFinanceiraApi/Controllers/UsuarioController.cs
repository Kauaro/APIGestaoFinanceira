using GestaoFinanceiraApi.Data.Context;
using GestaoFinanceiraApi.DTOs;
using GestaoFinanceiraApi.Entity;
using GestaoFinanceiraApi.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestaoFinanceiraApi.Controllers
{
    [ApiController]
    [Route("usuario")]
    [Authorize] 
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IExtratoService _extratoService;

        public UsuarioController(IUsuarioService usuarioService, IExtratoService extratoService)
        {
            _usuarioService = usuarioService;
            _extratoService = extratoService;
        }

        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var usuarioId = long.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            var usuario = await _usuarioService.ObterUsuarioLogadoAsync(usuarioId);

            return Ok(usuario);
        }




        [HttpGet("extrato-total")]
        public async Task<IActionResult> ObterExtratoTotal()
        {
            var usuarioId = long.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );
            var extrato = await _extratoService.ObterExtratoTotalAsync(usuarioId);
            return Ok(extrato);
        }



        [HttpGet("extrato-mensal")]
        public async Task<IActionResult> ObterExtratoMensal([FromQuery] int ano, [FromQuery] int mes)
        {
            var usuarioId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var extrato = await _extratoService.ObterExtratoMensalAsync(usuarioId, ano, mes);

            return Ok(extrato);
        }
    }

}
