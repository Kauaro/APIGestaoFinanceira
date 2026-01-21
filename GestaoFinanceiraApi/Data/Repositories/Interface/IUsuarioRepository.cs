using GestaoFinanceiraApi.Entity;

namespace GestaoFinanceiraApi.Data.Repositories.Interface
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByEmail(string email);
        Task<Usuario?> GetById(long id);

        Task Add(Usuario usuario);
    }
}
