using GestaoFinanceiraApi.DTOs.Usuario;
using GestaoFinanceiraApi.Data.Repositories.Interface;
using GestaoFinanceiraApi.Services.Interface;

namespace GestaoFinanceiraApi.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UsuarioMeDTO> ObterUsuarioLogadoAsync(long usuarioId)
        {
            var usuario = await _usuarioRepository.GetById(usuarioId);

            if (usuario == null)
                throw new Exception("Usuário não encontrado");

            return new UsuarioMeDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email
            };
        }
    }
}
