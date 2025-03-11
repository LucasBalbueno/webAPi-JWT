using System.IdentityModel.Tokens.Jwt;
using UserJWT.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace UserJWT.Services.PasswordService;

public class PasswordService : IPasswordInterface
{
    private readonly IConfiguration _config;

    public PasswordService(IConfiguration config)
    {
        _config = config;
    }
    
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }

    public string CreateTokenJwt(UserModel user)
    {
        
        // Informações incluidas no tokenJWT
        List<Claim> claims = new List<Claim>()
        {
            new Claim("Role", user.Role.ToString()),
            new Claim("Email", user.Email),
            new Claim("Username", user.UserName)
        };

        // Criando uma key baseado em Token fixo do AppSettings
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

        // Criando uma credencial baseado na key e definindo o método de cripitografia
        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        // Estruturando o token, passando as claims, definindo data de expiração e a credencial criada
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credential
        );

        // Transformando o token estruturado em um token formato JWT
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        
        return jwt;
    }
}