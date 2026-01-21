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

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
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
    }

}
