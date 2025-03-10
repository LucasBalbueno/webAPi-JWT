using UserJWT.DTOs;
using UserJWT.Models;

namespace UserJWT.Services.AuthService;

public interface IAuthInterface
{
    Task<Response<RegisterDTO>> Register(RegisterDTO userRegister);
}