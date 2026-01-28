using GestaoFinanceiraApi.DTOs.Investimento;

namespace GestaoFinanceiraApi.Services.Interface
{
    public interface IInvestimentosService
    {
        Task CriarAsync(CriarInvestimentoDTO dto, long usuarioId);
        Task ResgatarAsync(ResgatarInvestimentoDTO dto, long usuarioId);

        Task AtualizarAsync(long investimentoId, AtualizarInvestimentoDTO dto, long usuarioId);

        Task RemoverAsync(long investimentoId, long usuarioId);

        Task<ResponseInvestimentoDTO?> ObterPorIdAsync(long investimentoId, long usuarioId);

        Task<List<ResponseInvestimentoDTO>> ObterPorUsuarioAsync(long usuarioId);

        Task<List<ResponseInvestimentoDTO>> ObterPorCategoriaAsync(long usuarioId, string categoria);
    }
}
