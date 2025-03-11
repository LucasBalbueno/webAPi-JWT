using UserJWT.Models;

namespace UserJWT.Services.PasswordService;

public interface IPasswordInterface
{
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

    bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    
    string CreateTokenJwt(UserModel user);
}