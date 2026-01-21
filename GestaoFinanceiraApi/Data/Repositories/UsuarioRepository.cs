using GestaoFinanceiraApi.Data.Context;
using GestaoFinanceiraApi.Data.Repositories.Interface;
using GestaoFinanceiraApi.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace GestaoFinanceiraApi.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }



        public async Task<Usuario?> GetByEmail(string email)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);
        }


        public async Task<Usuario?> GetById(long id)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == id);
        }





        public async Task Add(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }



    }
}
