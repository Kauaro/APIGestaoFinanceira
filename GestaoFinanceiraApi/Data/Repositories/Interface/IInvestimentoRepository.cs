using GestaoFinanceiraApi.Entity;

namespace GestaoFinanceiraApi.Data.Repositories.Interface
{
    public interface IInvestimentoRepository
    {
        Task CriarAsync(Investimento investimento);

        Task AtualizarAsync(Investimento investimento);

        Task RemoverAsync(Investimento investimento);

        Task<Investimento?> ObterPorIdAsync(long id);

        Task<List<Investimento>> ObterPorUsuarioAsync(long usuarioId);
        Task<List<Investimento>> ObterPorUsuarioECategoriaAsync(long usuarioId, string categoria);
        Task<decimal> ObterTotalDoMesAsync(long usuarioId, int ano, int mes);
        Task<decimal> ObterTotalGeralAsync(long usuarioId);
        Task<decimal> ObterTotalDoMesAtualAsync(long usuarioId);

    }
}
