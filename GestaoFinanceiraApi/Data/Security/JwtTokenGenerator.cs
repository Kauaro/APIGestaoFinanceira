using GestaoFinanceiraApi.Data.Security.Interface;
using GestaoFinanceiraApi.Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace GestaoFinanceiraApi.Data.Security
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {

        private readonly IConfiguration _config;

        public JwtTokenGenerator(IConfiguration config)
        {
            _config = config;
        }

        public string Generate(Usuario usuario)
        {
            var claims = new[]
            {

             new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),


             new Claim(ClaimTypes.Email, usuario.Email),
             new Claim(ClaimTypes.Name, usuario.Nome),
        

             new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString())

    };

            var keyString = _config["Jwt:Key"]
                ?? throw new Exception("JWT Key não configurada");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(keyString)
            );

            var creds = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );


            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
