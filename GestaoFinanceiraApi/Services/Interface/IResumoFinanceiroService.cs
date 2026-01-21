using GestaoFinanceiraApi.DTOs.ResumoSaldo;

namespace GestaoFinanceiraApi.Services.Interface
{
    public interface IResumoFinanceiroService
    {

        Task<ResumoFinanceiroMensalDTO> ObterResumoMensalAsync(long usuarioId, int ano, int mes);
        Task<ResumoFinanceiroMensalDTO> ObterResumoMensalAtualAsync(long usuarioId);
        Task<ResumoFinanceiroTotalDTO> ObterResumoTotalAsync(long usuarioId);

    }
}
