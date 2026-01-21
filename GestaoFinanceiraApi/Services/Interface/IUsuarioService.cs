using GestaoFinanceiraApi.DTOs.Usuario;

namespace GestaoFinanceiraApi.Services.Interface
{
    public interface IUsuarioService
    {
        Task<UsuarioMeDTO> ObterUsuarioLogadoAsync(long usuarioId);
    }
}
