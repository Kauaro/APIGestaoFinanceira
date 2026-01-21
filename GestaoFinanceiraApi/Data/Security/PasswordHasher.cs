using BCrypt.Net;
using GestaoFinanceiraApi.Data.Security.Interface;


namespace GestaoFinanceiraApi.Data.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public bool Verify(string senha, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(senha, hash);
        }
    }
}
