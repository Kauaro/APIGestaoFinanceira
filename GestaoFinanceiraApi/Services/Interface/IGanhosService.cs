using GestaoFinanceiraApi.DTOs.Ganho;
using GestaoFinanceiraApi.Entity;

namespace GestaoFinanceiraApi.Services.Interface
{
    public interface IGanhosService
    {
        Task<Ganhos> CriarAsync(
        string descricao,
        decimal valor,
        string categoria,
        string pagamento,
        DateTime dataGanho,
        long usuarioId
    );

        Task AtualizarAsync(long id, string descricao, decimal valor, string categoria, string pagamento, DateTime dataGanho);
        Task RemoverAsync(long id);

        Task<Ganhos?> ObterPorIdAsync(long id);
        Task<IEnumerable<Ganhos>> ObterPorUsuarioAsync(long usuarioId);
        Task<IEnumerable<Ganhos>> ObterUltimosAsync(long usuarioId, int quantidade);

        Task<IEnumerable<Ganhos>> ObterPorPeriodoAsync(long usuarioId, DateTime inicio, DateTime fim);

        Task<decimal> ObterTotalDoMesAsync(long usuarioId, int ano, int mes);
        Task<decimal> ObterTotalDoMesAtualAsync(long usuarioId);
        Task<decimal> ObterTotalPorCategoriaAsync(long usuarioId, string categoria, DateTime inicio, DateTime fim);

    }

}
