using GestaoFinanceiraApi.Entity;

namespace GestaoFinanceiraApi.Services.Interface
{
    public interface IGastosService
    {
        Task<Gastos> CriarAsync(
            string descricao,
            decimal valor,
            string categoria,
            string pagamento,
            DateTime dataGasto,
            long usuarioId
        );

        Task AtualizarAsync(long id, string descricao, decimal valor, string categoria, string pagamento, DateTime dataGasto);
        Task RemoverAsync(long id);

        Task<Gastos?> ObterPorIdAsync(long id);
        Task<IEnumerable<Gastos>> ObterPorUsuarioAsync(long usuarioId);
        Task<IEnumerable<Gastos>> ObterUltimosAsync(long usuarioId, int quantidade);

        Task<IEnumerable<Gastos>> ObterPorPeriodoAsync(long usuarioId, DateTime inicio, DateTime fim);

        Task<decimal> ObterTotalDoMesAsync(long usuarioId, int ano, int mes);
        Task<decimal> ObterTotalDoMesAtualAsync(long usuarioId);
        Task<decimal> ObterTotalPorCategoriaAsync(long usuarioId, string categoria, DateTime inicio, DateTime fim);
    }

}
