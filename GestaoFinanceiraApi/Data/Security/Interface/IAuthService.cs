namespace GestaoFinanceiraApi.Data.Security.Interface
{
    public interface IAuthService
    {
            Task Register(string nome, string email, string senha);
            Task<string> Login(string email, string senha);
        
    }
}
