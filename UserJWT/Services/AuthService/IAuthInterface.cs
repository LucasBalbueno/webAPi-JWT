using UserJWT.DTOs;
using UserJWT.Models;

namespace UserJWT.Services.AuthService;

public interface IAuthInterface
{
    Task<Response<RegisterDto>> Register(RegisterDto userRegister);
    Task<Response<string>> Login(LoginDto userlogin);
}