using GestaoFinanceiraApi.Entity;

namespace GestaoFinanceiraApi.Data.Repositories.Interface
{
    public interface IGanhosRepository
    {

        // CRUD
        Task AdicionarAsync(Ganhos ganho);
        Task AtualizarAsync(Ganhos ganho);
        Task RemoverAsync(long id);
        Task<Ganhos?> ObterPorIdAsync(long id);

        // Listagens
        Task<IEnumerable<Ganhos>> ObterPorUsuarioAsync(long usuarioId);
        Task<IEnumerable<Ganhos>> ObterUltimosAsync(long usuarioId, int quantidade);

        // Filtros por período
        Task<IEnumerable<Ganhos>> ObterPorPeriodoAsync(
            long usuarioId,
            DateTime dataInicio,
            DateTime dataFim
        );

        // Relatórios
        Task<decimal> ObterTotalDoMesAsync(long usuarioId, int ano, int mes);
        Task<decimal> ObterTotalGeralAsync(long usuarioId);
        Task<decimal> ObterTotalDoMesAtualAsync(long usuarioId);
        Task<decimal> ObterTotalPorCategoriaAsync(
            long usuarioId,
            string categoria,
            DateTime dataInicio,
            DateTime dataFim
        );

    }
}
