namespace UserJWT.Services.PasswordService;

public interface IPasswordInterface
{
    void CreatePasswordHash(string password, out byte[] passwordHash);
}