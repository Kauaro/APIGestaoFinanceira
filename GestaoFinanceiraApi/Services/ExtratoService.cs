using GestaoFinanceiraApi.Data.Repositories;
using GestaoFinanceiraApi.Data.Repositories.Interface;
using GestaoFinanceiraApi.DTOs.Extrato;
using GestaoFinanceiraApi.DTOs.ResumoSaldo;
using GestaoFinanceiraApi.Services.Interface;

namespace GestaoFinanceiraApi.Services
{



    public class ExtratoService : IExtratoService
    {
        private readonly IInvestimentoRepository _investimentoRepo;
        private readonly IGanhosRepository _ganhoRepo;
        private readonly IGastosRepository _gastoRepo;

        public ExtratoService(IInvestimentoRepository investimentoRepo,
                              IGanhosRepository ganhoRepo,
                              IGastosRepository gastoRepo)
        {
            _investimentoRepo = investimentoRepo;
            _ganhoRepo = ganhoRepo;
            _gastoRepo = gastoRepo;
        }

        public async Task<List<ExtratoDTO>> ObterExtratoTotalAsync(long usuarioId)
        {
            var investimentos = await _investimentoRepo.ObterPorUsuarioAsync(usuarioId);
            var ganhos = await _ganhoRepo.ObterPorUsuarioAsync(usuarioId);
            var gastos = await _gastoRepo.ObterPorUsuarioAsync(usuarioId);

            

            var listaInvestimentos = investimentos.Select(i => new ExtratoDTO
            {
                Tipo = "Investimento",
                Descricao = i.Categoria,
                Valor = i.ValorAplicado,
                Categoria = i.Categoria,
                Data = i.DataAplicacao
            });





            var listaGanhos = ganhos.Select(g => new ExtratoDTO
            {
                Tipo = "Ganho",
                Descricao = g.Descricao,
                Valor = g.Valor,
                Categoria = g.Categoria,
                Data = g.DataGanho
            });






            var listaGastos = gastos.Select(g => new ExtratoDTO
            {
                Tipo = "Gasto",
                Descricao = g.Descricao,
                Valor = g.Valor,
                Categoria = g.Categoria,
                Data = g.DataGasto
            });






            var extratoCompleto = listaInvestimentos
                .Concat(listaGanhos)
                .Concat(listaGastos)
                .OrderByDescending(x => x.Data)
                .ToList();

            return extratoCompleto;
        }




        public async Task<List<ExtratoDTO>> ObterExtratoMensalAsync(long usuarioId, int ano, int mes)
        {


            var investimentos = await _investimentoRepo.ObterPorUsuarioAsync(usuarioId);
            var ganhos = await _ganhoRepo.ObterPorUsuarioAsync(usuarioId);
            var gastos = await _gastoRepo.ObterPorUsuarioAsync(usuarioId);


            bool EstaNoMes(DateTime data) => data.Year == ano && data.Month == mes;



            var listaInvestimentos = investimentos
                .Where(i => EstaNoMes(i.DataAplicacao))
                .Select(i => new ExtratoDTO
                {
                    Tipo = "Investimento",
                    Descricao = i.Categoria,
                    Valor = i.ValorAplicado,
                    Categoria = i.Categoria,
                    Data = i.DataAplicacao
                });







            var listaGanhos = ganhos
                .Where(g => EstaNoMes(g.DataGanho))
                .Select(g => new ExtratoDTO
                {
                    Tipo = "Ganho",
                    Descricao = g.Descricao,
                    Valor = g.Valor,
                    Categoria = g.Categoria,
                    Data = g.DataGanho
                });







            var listaGastos = gastos
                .Where(g => EstaNoMes(g.DataGasto))
                .Select(g => new ExtratoDTO
                {
                    Tipo = "Gasto",
                    Descricao = g.Descricao,
                    Valor = g.Valor,
                    Categoria = g.Categoria,
                    Data = g.DataGasto
                });







            var extratoMes = listaInvestimentos
                .Concat(listaGanhos)
                .Concat(listaGastos)
                .OrderByDescending(x => x.Data)
                .ToList();

            return extratoMes;
        }



    }
}
