using GestaoFinanceiraApi.Entity;

namespace GestaoFinanceiraApi.Data.Repositories.Interface
{
    public interface IGastosRepository
    {
        // CRUD
        Task AdicionarAsync(Gastos gasto);
        Task AtualizarAsync(Gastos gasto);
        Task RemoverAsync(long id);
        Task<Gastos?> ObterPorIdAsync(long id);

        // Listagens
        Task<IEnumerable<Gastos>> ObterPorUsuarioAsync(long usuarioId);
        Task<IEnumerable<Gastos>> ObterUltimosAsync(long usuarioId, int quantidade);

        // Filtros por período
        Task<IEnumerable<Gastos>> ObterPorPeriodoAsync(
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
