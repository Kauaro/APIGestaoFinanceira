using GestaoFinanceiraApi.Data.Repositories.Interface;
using GestaoFinanceiraApi.DTOs.ResumoSaldo;
using GestaoFinanceiraApi.Services.Interface;

namespace GestaoFinanceiraApi.Services
{
    public class ResumoFinanceiroService : IResumoFinanceiroService
    {
        private readonly IGastosRepository _gastosRepository;
        private readonly IGanhosRepository _ganhosRepository;
        private readonly IInvestimentoRepository _investimentoRepository;

        public ResumoFinanceiroService(
            IGastosRepository gastosRepository,
            IGanhosRepository ganhosRepository,
            IInvestimentoRepository investimentoRepository)
        {
            _gastosRepository = gastosRepository;
            _ganhosRepository = ganhosRepository;
            _investimentoRepository = investimentoRepository;
        }

       






        public async Task<ResumoFinanceiroMensalDTO> ObterResumoMensalAsync(
            long usuarioId,
            int ano,
            int mes)
        {
            var totalGanhos = await _ganhosRepository
                .ObterTotalDoMesAsync(usuarioId, ano, mes);

            var totalGastos = await _gastosRepository
                .ObterTotalDoMesAsync(usuarioId, ano, mes);

            var totalInvestido = await _investimentoRepository
                .ObterTotalDoMesAsync(usuarioId, ano, mes);

            

            var totalInvestimentos = totalInvestido;

            return new ResumoFinanceiroMensalDTO
            {
                Ano = ano,
                Mes = mes,
                TotalGanhos = totalGanhos,
                TotalGastos = totalGastos,
                TotalInvestimentos = totalInvestimentos
            };
        }






        public async Task<ResumoFinanceiroMensalDTO> ObterResumoMensalAtualAsync(long usuarioId)
        {
            var agora = DateTime.Now;
            return await ObterResumoMensalAsync(usuarioId, agora.Year, agora.Month);
        }

        



        public async Task<ResumoFinanceiroTotalDTO> ObterResumoTotalAsync(long usuarioId)
        {
            var totalGanhos = await _ganhosRepository
                .ObterTotalGeralAsync(usuarioId);

            var totalGastos = await _gastosRepository
                .ObterTotalGeralAsync(usuarioId);

            var totalInvestido = await _investimentoRepository
                .ObterTotalGeralAsync(usuarioId);


            var totalResgatado = await _ganhosRepository
                .ObterTotalResgatesAsync(usuarioId);

            var totalInvestimentos = totalInvestido - totalResgatado;

            return new ResumoFinanceiroTotalDTO
            {
                TotalGanhos = totalGanhos,
                TotalGastos = totalGastos,
                TotalInvestimentos = totalInvestimentos
            };
        }




    }
}
