using GestaoFinanceiraApi.Data.Security.Interface;
using GestaoFinanceiraApi.Data.Security;
using GestaoFinanceiraApi.Entity;
using GestaoFinanceiraApi.Data.Repositories.Interface;


namespace GestaoFinanceiraApi.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(
            IUsuarioRepository usuarioRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _usuarioRepository = usuarioRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }



        public async Task Register(string nome, string email, string senha)
        {

            var usuarioExistente = await _usuarioRepository.GetByEmail(email);

            if (usuarioExistente != null)
            {
                throw new Exception("Usuário com este email já existe.");
            }

            var senhaHash = _passwordHasher.Hash(senha);


            var usuario = new Usuario(nome, email, senhaHash);


            await _usuarioRepository.Add(usuario);
        }



        public async Task<string> Login(string email, string senha)
        {
            var usuario = await _usuarioRepository.GetByEmail(email);
            if (usuario == null)
            {
                throw new Exception("Credenciais inválidas");
            }

            var senhaValidar = _passwordHasher.Verify(senha, usuario.SenhaHash);
            if (!senhaValidar)
            {
                throw new Exception("Credenciais inválidas");
            }

            return _jwtTokenGenerator.Generate(usuario);


        }

    }
}
