using GestaoFinanceiraApi.Entity;

namespace GestaoFinanceiraApi.Data.Repositories.Interface
{
    public interface IGanhosRepository
    {



        Task AdicionarAsync(Ganhos ganho);
        Task AtualizarAsync(Ganhos ganho);
        Task RemoverAsync(long id);
        Task<Ganhos?> ObterPorIdAsync(long id);

        


        Task<IEnumerable<Ganhos>> ObterPorUsuarioAsync(long usuarioId);
        Task<IEnumerable<Ganhos>> ObterUltimosAsync(long usuarioId, int quantidade);



        Task<IEnumerable<Ganhos>> ObterPorPeriodoAsync(
            long usuarioId,
            DateTime dataInicio,
            DateTime dataFim
        );



        Task<decimal> ObterTotalDoMesAsync(long usuarioId, int ano, int mes);
        Task<decimal> ObterTotalGeralAsync(long usuarioId);
        Task<decimal> ObterTotalDoMesAtualAsync(long usuarioId);
        Task<decimal> ObterTotalPorCategoriaAsync(
            long usuarioId,
            string categoria,
            DateTime dataInicio,
            DateTime dataFim
        );

        Task<List<Ganhos>> ObterPorUsuarioECategoriaAsync(long usuarioId, string categoria);
        Task<List<Ganhos>> ObterResgatesPorUsuarioECategoriaAsync(long usuarioId, string categoria);


        Task<decimal> ObterTotalResgatesAsync(long usuarioId);
        Task<decimal> ObterTotalResgatesDoMesAsync(long usuarioId, int ano, int mes);




    }
}
