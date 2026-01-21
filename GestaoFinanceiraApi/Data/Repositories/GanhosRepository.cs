using GestaoFinanceiraApi.Data.Context;
using GestaoFinanceiraApi.Data.Repositories.Interface;
using GestaoFinanceiraApi.Entity;
using Microsoft.EntityFrameworkCore;

namespace GestaoFinanceiraApi.Data.Repositories
{
    public class GanhosRepository : IGanhosRepository
    {
        private readonly AppDbContext _context;

        public GanhosRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Ganhos?> ObterPorIdAsync(long id)
        {
            return await _context.Ganhos
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Ganhos>> ObterPorUsuarioAsync(long usuarioId)
        {
            return await _context.Ganhos
                .Where(g => g.UsuarioId == usuarioId)
                .OrderByDescending(g => g.DataGanho)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Ganhos>> ObterUltimosAsync(long usuarioId, int quantidade)
        {
            return await _context.Ganhos
                .Where(g => g.UsuarioId == usuarioId)
                .OrderByDescending(g => g.DataGanho)
                .Take(quantidade)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Ganhos>> ObterPorPeriodoAsync(
            long usuarioId,
            DateTime dataInicio,
            DateTime dataFim)
        {
            return await _context.Ganhos
                .Where(g =>
                    g.UsuarioId == usuarioId &&
                    g.DataGanho >= dataInicio &&
                    g.DataGanho <= dataFim)
                .OrderByDescending(g => g.DataGanho)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<decimal> ObterTotalDoMesAsync(long usuarioId, int ano, int mes)
        {
            return await _context.Ganhos
                .Where(g =>
                    g.UsuarioId == usuarioId &&
                    g.DataGanho.Year == ano &&
                    g.DataGanho.Month == mes)
                .SumAsync(g => g.Valor);
        }


        public async Task<decimal> ObterTotalDoMesAtualAsync(long usuarioId)
        {
            var agora = DateTime.Now;
            return await ObterTotalDoMesAsync(usuarioId, agora.Year, agora.Month);
        }



        public async Task<decimal> ObterTotalPorCategoriaAsync(
            long usuarioId,
            string categoria,
            DateTime dataInicio,
            DateTime dataFim)
        {
            return await _context.Ganhos
                .Where(g =>
                    g.UsuarioId == usuarioId &&
                    g.Categoria == categoria &&
                    g.DataGanho >= dataInicio &&
                    g.DataGanho <= dataFim)
                .SumAsync(g => g.Valor);
        }

        public async Task AdicionarAsync(Ganhos ganho)
        {
            await _context.Ganhos.AddAsync(ganho);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Ganhos ganho)
        {
            _context.Ganhos.Update(ganho);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(long id)
        {
            var ganho = await _context.Ganhos.FindAsync(id);

            if (ganho == null)
                throw new Exception("Gasto não encontrado");

            _context.Ganhos.Remove(ganho);
            await _context.SaveChangesAsync();
        }


        public async Task<decimal> ObterTotalGeralAsync(long usuarioId)
        {
            return await _context.Ganhos
                .Where(g => g.UsuarioId == usuarioId)
                .SumAsync(g => g.Valor);
        }



    }
}
