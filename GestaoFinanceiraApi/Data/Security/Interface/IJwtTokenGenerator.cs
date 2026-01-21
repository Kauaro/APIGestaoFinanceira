using GestaoFinanceiraApi.Entity;

namespace GestaoFinanceiraApi.Data.Security.Interface
{
    public interface IJwtTokenGenerator
    {
        string Generate(Usuario usuario);
    }
}
