using GestaoFinanceiraApi.DTOs.Auth;
using GestaoFinanceiraApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace GestaoFinanceiraApi.Controllers
{


    [ApiController]
    [Route("autenticar")]
    public class AuthController : ControllerBase
    {

        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("cadastro")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            try
            {
                await _authService.Register(dto.Nome, dto.Email, dto.Senha);
                return Ok(new { message = "Usuário registrado com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            try
            {
                var token = await _authService.Login(dto.Email, dto.Senha);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }


    }
}
