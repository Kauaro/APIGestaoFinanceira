using GestaoFinanceiraApi.Data.Repositories.Interface;
using GestaoFinanceiraApi.Entity;
using GestaoFinanceiraApi.Services.Interface;

namespace GestaoFinanceiraApi.Services
{
    public class GanhosService : IGanhosService
    {
        private readonly IGanhosRepository _repository;

        public GanhosService(IGanhosRepository repository)
        {
            _repository = repository;
        }

        public async Task<Ganhos> CriarAsync(
           string descricao,
           decimal valor,
           string categoria,
           string pagamento,
           DateTime dataGanho,
           long usuarioId)

        {
            var ganho = new Ganhos(

                descricao,
                valor,
                categoria,
                pagamento,
                dataGanho,
                usuarioId
            );


            await _repository.AdicionarAsync(ganho);
            return ganho;

        }



        public async Task AtualizarAsync(
       long id,
       string descricao,
       decimal valor,
       string categoria,
       string pagamento,
       DateTime dataGanho)
        {
            var ganho = await _repository.ObterPorIdAsync(id);

            if (ganho == null)
                throw new Exception("Ganho não encontrado");

            ganho.AlterarDescricao(descricao);
            ganho.AlterarValor(valor);
            ganho.AlterarCategoria(categoria);
            ganho.AlterarPagamento(pagamento);
            ganho.AlterarDataGanho(dataGanho);

            await _repository.AtualizarAsync(ganho);
        }


        public async Task RemoverAsync(long id)
        {
            var ganho = await _repository.ObterPorIdAsync(id);
            if (ganho == null)
                throw new Exception("Ganho não encontrado");

            await _repository.RemoverAsync(id);
        }


        //Localizar


        public Task<Ganhos?> ObterPorIdAsync(long id)
            => _repository.ObterPorIdAsync(id);


        public Task<IEnumerable<Ganhos>> ObterPorUsuarioAsync(long usuarioId)
            => _repository.ObterPorUsuarioAsync(usuarioId);

        public Task<IEnumerable<Ganhos>> ObterUltimosAsync(long usuarioId, int quantidade)
            => _repository.ObterUltimosAsync(usuarioId, quantidade);

        public Task<IEnumerable<Ganhos>> ObterPorPeriodoAsync(long usuarioId, DateTime inicio, DateTime fim)
            => _repository.ObterPorPeriodoAsync(usuarioId, inicio, fim);

        public Task<decimal> ObterTotalDoMesAsync(long usuarioId, int ano, int mes)
            => _repository.ObterTotalDoMesAsync(usuarioId, ano, mes);

        public Task<decimal> ObterTotalDoMesAtualAsync(long usuarioId)
            => _repository.ObterTotalDoMesAtualAsync(usuarioId);


        public Task<decimal> ObterTotalPorCategoriaAsync(
        long usuarioId,
        string categoria,
        DateTime inicio,
        DateTime fim)
        => _repository.ObterTotalPorCategoriaAsync(usuarioId, categoria, inicio, fim);

    }
}