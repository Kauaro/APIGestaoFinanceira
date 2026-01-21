using GestaoFinanceiraApi.Data.Context;
using GestaoFinanceiraApi.Data.Repositories.Interface;
using GestaoFinanceiraApi.Entity;
using Microsoft.EntityFrameworkCore;

namespace GestaoFinanceiraApi.Data.Repositories
{
    public class GastosRepository : IGastosRepository
    {
        private readonly AppDbContext _context;

        public GastosRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Gastos?> ObterPorIdAsync(long id)
        {
            return await _context.Gastos
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Gastos>> ObterPorUsuarioAsync(long usuarioId)
        {
            return await _context.Gastos
                .Where(g => g.UsuarioId == usuarioId)
                .OrderByDescending(g => g.DataGasto)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Gastos>> ObterUltimosAsync(long usuarioId, int quantidade)
        {
            return await _context.Gastos
                .Where(g => g.UsuarioId == usuarioId)
                .OrderByDescending(g => g.DataGasto)
                .Take(quantidade)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Gastos>> ObterPorPeriodoAsync(
            long usuarioId,
            DateTime dataInicio,
            DateTime dataFim)
        {
            return await _context.Gastos
                .Where(g =>
                    g.UsuarioId == usuarioId &&
                    g.DataGasto >= dataInicio &&
                    g.DataGasto <= dataFim)
                .OrderByDescending(g => g.DataGasto)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<decimal> ObterTotalDoMesAsync(long usuarioId, int ano, int mes)
        {
            return await _context.Gastos
                .Where(g =>
                    g.UsuarioId == usuarioId &&
                    g.DataGasto.Year == ano &&
                    g.DataGasto.Month == mes)
                .SumAsync(g => g.Valor);
        }


        public async Task<decimal> ObterTotalDoMesAtualAsync(long usuarioId)
        {
            var agora = DateTime.Now;
            return await _context.Gastos
                .Where(g =>
                    g.UsuarioId == usuarioId &&
                    g.DataGasto.Year == agora.Year &&
                    g.DataGasto.Month == agora.Month)
                .SumAsync(g => g.Valor);
        }


        public async Task<decimal> ObterTotalPorCategoriaAsync(
            long usuarioId,
            string categoria,
            DateTime dataInicio,
            DateTime dataFim)
        {
            return await _context.Gastos
                .Where(g =>
                    g.UsuarioId == usuarioId &&
                    g.Categoria == categoria &&
                    g.DataGasto >= dataInicio &&
                    g.DataGasto <= dataFim)
                .SumAsync(g => g.Valor);
        }

        public async Task AdicionarAsync(Gastos gasto)
        {
            await _context.Gastos.AddAsync(gasto);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Gastos gasto)
        {
            _context.Gastos.Update(gasto);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(long id)
        {
            var gasto = await _context.Gastos.FindAsync(id);

            if (gasto == null)
                throw new Exception("Gasto não encontrado");

            _context.Gastos.Remove(gasto);
            await _context.SaveChangesAsync();
        }


        public async Task<decimal> ObterTotalGeralAsync(long usuarioId)
        {
            return await _context.Gastos
                .Where(g => g.UsuarioId == usuarioId)
                .SumAsync(g => g.Valor);
        }



    }
}
