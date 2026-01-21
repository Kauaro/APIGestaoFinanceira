using GestaoFinanceiraApi.Data.Repositories.Interface;
using GestaoFinanceiraApi.DTOs.ResumoSaldo;
using GestaoFinanceiraApi.Services.Interface;

namespace GestaoFinanceiraApi.Services
{
    public class ResumoFinanceiroService : IResumoFinanceiroService
    {
        private readonly IGastosRepository _gastosRepository;
        private readonly IGanhosRepository _ganhosRepository;

        public ResumoFinanceiroService(
            IGastosRepository gastosRepository,
            IGanhosRepository ganhosRepository)
        {
            _gastosRepository = gastosRepository;
            _ganhosRepository = ganhosRepository;
        }


        public async Task<ResumoFinanceiroMensalDTO> ObterResumoMensalAsync(long usuarioId, int ano, int mes)
        {
            var totalGanhos = await _ganhosRepository.ObterTotalDoMesAsync(usuarioId, ano, mes);
            var totalGastos = await _gastosRepository.ObterTotalDoMesAsync(usuarioId, ano, mes);

            return new ResumoFinanceiroMensalDTO
            {
                Ano = ano,
                Mes = mes,
                TotalGanhos = totalGanhos,
                TotalGastos = totalGastos
            };
        }


        public async Task<ResumoFinanceiroMensalDTO> ObterResumoMensalAtualAsync(long usuarioId)
        {
            var agora = DateTime.Now;
            return await ObterResumoMensalAsync(usuarioId, agora.Year, agora.Month);
        }


        public async Task<ResumoFinanceiroTotalDTO> ObterResumoTotalAsync(long usuarioId)
        {
            var totalGanhos = await _ganhosRepository.ObterTotalGeralAsync(usuarioId);
            var totalGastos = await _gastosRepository.ObterTotalGeralAsync(usuarioId);

            return new ResumoFinanceiroTotalDTO
            {
                TotalGanhos = totalGanhos,
                TotalGastos = totalGastos
            };
        }


    }
}
