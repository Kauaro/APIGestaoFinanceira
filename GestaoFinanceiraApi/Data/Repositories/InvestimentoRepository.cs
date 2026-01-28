using GestaoFinanceiraApi.Data.Context;
using GestaoFinanceiraApi.Data.Repositories.Interface;
using GestaoFinanceiraApi.Entity;
using Microsoft.EntityFrameworkCore;

namespace GestaoFinanceiraApi.Data.Repositories
{
    public class InvestimentoRepository : IInvestimentoRepository
    {
        private readonly AppDbContext _context;

        public InvestimentoRepository(AppDbContext context)
        {
            _context = context;
        }






        public async Task CriarAsync(Investimento investimento)
        {

            await _context.Investimentos.AddAsync(investimento);
            await _context.SaveChangesAsync();
        }





        public async Task AtualizarAsync(Investimento investimento)
        {
            _context.Investimentos.Update(investimento);
            await _context.SaveChangesAsync();
        }




        public async Task RemoverAsync(Investimento investimento)
        {
            _context.Investimentos.Remove(investimento);
            await _context.SaveChangesAsync();
        }




        public async Task<Investimento?> ObterPorIdAsync(long id)
        {
            return await _context.Investimentos
                .FirstOrDefaultAsync(i => i.Id == id);
        }





        public async Task<List<Investimento>> ObterPorUsuarioAsync(long usuarioId)
        {
            return await _context.Investimentos
                .Where(i => i.UsuarioId == usuarioId)
                .OrderByDescending(i => i.DataAplicacao)
                .ToListAsync();
        }


        public async Task<List<Investimento>> ObterPorUsuarioECategoriaAsync(long usuarioId, string categoria)
        {
            return await _context.Investimentos
                .Where(i => i.UsuarioId == usuarioId &&
                            i.Categoria.ToLower() == categoria.Trim().ToLower())
                .OrderByDescending(i => i.DataAplicacao)
                .ToListAsync();
        }



        public async Task<decimal> ObterTotalDoMesAsync(long usuarioId, int ano, int mes)
        {
            return await _context.Investimentos
                .Where(g =>
                    g.UsuarioId == usuarioId &&
                    g.DataAplicacao.Year == ano &&
                    g.DataAplicacao.Month == mes)
                .SumAsync(g => g.ValorAplicado);
        }




        public async Task<decimal> ObterTotalDoMesAtualAsync(long usuarioId)
        {
            var agora = DateTime.Now;
            return await _context.Investimentos
                .Where(g =>
                    g.UsuarioId == usuarioId &&
                    g.DataAplicacao.Year == agora.Year &&
                    g.DataAplicacao.Month == agora.Month)
                .SumAsync(g => g.ValorAplicado);
        }




        public async Task<decimal> ObterTotalGeralAsync(long usuarioId)
        {
            return await _context.Investimentos
                .Where(g => g.UsuarioId == usuarioId)
                .SumAsync(g => g.ValorAplicado);
        }





    }
}
