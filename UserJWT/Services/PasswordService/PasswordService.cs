using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

namespace UserJWT.Services.PasswordService;

public class PasswordService : IPasswordInterface
{
    public void CreatePasswordHash(string password, out byte[] passwordHash)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}