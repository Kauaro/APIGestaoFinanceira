using GestaoFinanceiraApi.Data.Repositories;
using GestaoFinanceiraApi.Data.Repositories.Interface;
using GestaoFinanceiraApi.DTOs.Investimento;
using GestaoFinanceiraApi.Entity;
using GestaoFinanceiraApi.Services.Interface;

namespace GestaoFinanceiraApi.Services
{
    public class InvestimentosService : IInvestimentosService
    {


        private readonly IInvestimentoRepository _investimentosRepository;
        private readonly IGastosRepository _gastosRepository;
        private readonly IResumoFinanceiroService _resumoFinanceiroService;
        private readonly IGanhosRepository _ganhosRepository;

        public InvestimentosService(
            IInvestimentoRepository investimentosRepository,
            IResumoFinanceiroService resumoFinanceiroService,
            IGastosRepository gastosRepository,
            IGanhosRepository ganhosRepository)
        {
            _investimentosRepository = investimentosRepository;
            _resumoFinanceiroService = resumoFinanceiroService;
            _gastosRepository = gastosRepository;
            _ganhosRepository = ganhosRepository;
        }







        public async Task CriarAsync(CriarInvestimentoDTO dto, long usuarioId)
        {
            if (usuarioId <= 0)
                throw new ArgumentException("Usuário inválido");

            var resumo = await _resumoFinanceiroService.ObterResumoTotalAsync(usuarioId);


            if (resumo.SaldoDisponivel < dto.ValorAplicado)
                throw new InvalidOperationException("Saldo insuficiente");


            var gasto = new Gastos(
                descricao: $"Investimento - {dto.Descricao}",
                categoria: "Investimento",
                valor: dto.ValorAplicado,
                pagamento: "Investimento",
                dataGasto: dto.DataAplicacao,
                usuarioId: usuarioId
            );


            await _gastosRepository.AdicionarAsync(gasto);

            var investimento = new Investimento(
                dto.Descricao,
                dto.Categoria,
                dto.ValorAplicado,
                dto.DataAplicacao,
                usuarioId
            );

            await _investimentosRepository.CriarAsync(investimento);
        }



        public async Task ResgatarAsync(ResgatarInvestimentoDTO dto, long usuarioId)
        {


            if (dto.Valor <= 0)
                throw new ArgumentException("Valor inválido");



            var categoria = dto.Categoria.Trim();

            if (categoria != "Investimento" && categoria != "Poupança")
                throw new ArgumentException("Categoria inválida");



            var investimentos = await _investimentosRepository
                .ObterPorUsuarioECategoriaAsync(usuarioId, categoria);

            var totalInvestido = investimentos.Sum(i => i.ValorAplicado);




            var ganhosResgate = await _ganhosRepository
                .ObterPorUsuarioECategoriaAsync(usuarioId, categoria);

            var totalResgatado = ganhosResgate.Sum(g => g.Valor);

            var saldoDisponivel = totalInvestido - totalResgatado;
            

            if (dto.Valor > saldoDisponivel)
                throw new InvalidOperationException("Saldo insuficiente para resgate");



            var ganho = new Ganhos(
                descricao: $"Resgate de {categoria}",
                categoria: categoria,
                valor: dto.Valor,
                pagamento: "Resgate de Investimento",
                dataGanho: dto.DataResgate,
                usuarioId: usuarioId
            );

            await _ganhosRepository.AdicionarAsync(ganho);


            var investimento = new Investimento(
                descricao: "Resgate",
                categoria: categoria,
                valorAplicado: -dto.Valor,
                dataAplicacao: dto.DataResgate,
                usuarioId: usuarioId
            );

            await _investimentosRepository.CriarAsync(investimento);
        }











        public async Task AtualizarAsync(long investimentoId, AtualizarInvestimentoDTO dto, long usuarioId)
        {
            var investimento = await _investimentosRepository.ObterPorIdAsync(investimentoId);

            if (investimento == null)
                throw new ArgumentException("Investimento não encontrado");

            if (investimento.UsuarioId != usuarioId)
                throw new UnauthorizedAccessException("Você não tem permissão para alterar este investimento");

            investimento.AlterarDescricao(dto.Descricao);
            investimento.AlterarCategoria(dto.Categoria);
            investimento.AlterarValorAplicado(dto.ValorAplicado);
            investimento.AlterarDataAplicacao(dto.DataAplicacao);

            await _investimentosRepository.AtualizarAsync(investimento);
        }








        public async Task RemoverAsync(long investimentoId, long usuarioId)
        {
            var investimento = await _investimentosRepository.ObterPorIdAsync(investimentoId);

            if (investimento == null)
                throw new ArgumentException("Investimento não encontrado");

            if (investimento.UsuarioId != usuarioId)
                throw new UnauthorizedAccessException("Você não tem permissão para remover este investimento");

            await _investimentosRepository.RemoverAsync(investimento);
        }







        public async Task<ResponseInvestimentoDTO?> ObterPorIdAsync(long investimentoId, long usuarioId)
        {
            var investimento = await _investimentosRepository.ObterPorIdAsync(investimentoId);

            if (investimento == null)
                return null;

            if (investimento.UsuarioId != usuarioId)
                throw new UnauthorizedAccessException("Você não tem permissão para acessar este investimento");

            return MapearParaResponse(investimento);
        }







        public async Task<List<ResponseInvestimentoDTO>> ObterPorUsuarioAsync(long usuarioId)
        {
            var investimentos = await _investimentosRepository.ObterPorUsuarioAsync(usuarioId);

            return investimentos
                .Select(MapearParaResponse)
                .ToList();
        }

       





        public async Task<List<ResponseInvestimentoDTO>> ObterPorCategoriaAsync(long usuarioId, string categoria)
        {
            var investimentos = await _investimentosRepository
                .ObterPorUsuarioECategoriaAsync(usuarioId, categoria);

            return investimentos
                .Select(MapearParaResponse)
                .ToList();
        }

        




        private static ResponseInvestimentoDTO MapearParaResponse(Investimento investimento)
        {
            return new ResponseInvestimentoDTO
            {
                Id = investimento.Id,
                Descricao = investimento.Descricao,
                Categoria = investimento.Categoria,
                ValorAplicado = investimento.ValorAplicado,
                DataAplicacao = investimento.DataAplicacao

            };
        }
    }
}
