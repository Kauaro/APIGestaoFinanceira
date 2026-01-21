using GestaoFinanceiraApi.Data.Repositories.Interface;
using GestaoFinanceiraApi.Entity;
using GestaoFinanceiraApi.Services.Interface;

namespace GestaoFinanceiraApi.Services
{
    public class GastosService : IGastosService
    {

        private readonly IGastosRepository _repository;

        public GastosService(IGastosRepository repository)
        {
            _repository = repository;
        }

        public async Task<Gastos> CriarAsync(
           string descricao,
           decimal valor,
           string categoria,
           string pagamento,
           DateTime dataGasto,
           long usuarioId)

        {
            var gasto = new Gastos(
            
                descricao,
                valor,
                categoria,
                pagamento,
                dataGasto,
                usuarioId
            );


            await _repository.AdicionarAsync(gasto);
            return gasto;

        }



        public async Task AtualizarAsync(
       long id,
       string descricao,
       decimal valor,
       string categoria,
       string pagamento,
       DateTime dataGasto)
        {
            var gasto = await _repository.ObterPorIdAsync(id);

            if (gasto == null)
                throw new Exception("Gasto não encontrado");

            gasto.AlterarDescricao(descricao);
            gasto.AlterarValor(valor);
            gasto.AlterarCategoria(categoria);
            gasto.AlterarPagamento(pagamento);
            gasto.AlterarDataGasto(dataGasto);

            await _repository.AtualizarAsync(gasto);
        }


        public async Task RemoverAsync(long id)
        {
            var gasto = await _repository.ObterPorIdAsync(id);
            if (gasto == null)
                throw new Exception("Gasto não encontrado");

            await _repository.RemoverAsync(id);
        }


        


        public Task<Gastos?> ObterPorIdAsync(long id)
            => _repository.ObterPorIdAsync(id);
       

        public Task<IEnumerable<Gastos>> ObterPorUsuarioAsync(long usuarioId)
            => _repository.ObterPorUsuarioAsync(usuarioId);

        public Task<IEnumerable<Gastos>> ObterUltimosAsync(long usuarioId, int quantidade)
            => _repository.ObterUltimosAsync(usuarioId, quantidade);

        public Task<IEnumerable<Gastos>> ObterPorPeriodoAsync(long usuarioId, DateTime inicio, DateTime fim)
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
