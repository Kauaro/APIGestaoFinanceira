using GestaoFinanceiraApi.DTOs;
using GestaoFinanceiraApi.DTOs.Extrato;
using GestaoFinanceiraApi.DTOs.ResumoSaldo;

namespace GestaoFinanceiraApi.Services.Interface
{
    public interface IExtratoService
    {
        Task<List<ExtratoDTO>> ObterExtratoMensalAsync(long usuarioId, int ano, int mes);
        Task<List<ExtratoDTO>> ObterExtratoTotalAsync(long usuarioId);
    }
}
