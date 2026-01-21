namespace GestaoFinanceiraApi.Entity
{
    public class Usuario : Identificador
    {
        public string Nome { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string SenhaHash { get; private set; } = string.Empty;


        public DateTime CriadoEm { get; set; }


        public bool IsAdmin { get; private set; }

        protected Usuario() { }

        public Usuario(string nome, string email, string senhaHash)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome inválido");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email inválido");

            Nome = nome;
            Email = email;
            SenhaHash = senhaHash;
            IsAdmin = false;
        }

        public void AlterarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome inválido");

            Nome = nome;
        }

        public void AlterarSenha(string novaSenhaHash)
        {
            SenhaHash = novaSenhaHash;
        }

        public void TornarAdmin()
        {
            IsAdmin = true;
        }
    }
}
