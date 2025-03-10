using UserJWT.DataAccess;
using UserJWT.DTOs;
using UserJWT.Models;

namespace UserJWT.Services.AuthService;

public class AuthService : IAuthInterface
{
    private readonly AppDbContext _context;

    public AuthService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Response<RegisterDTO>> Register(RegisterDTO userRegister)
    {
        Response<RegisterDTO> responseService = new Response<RegisterDTO>();

        try
        {
            if (!VerificationUserAndEmailisExist(userRegister))
            {
                responseService.Dados = null;
                responseService.Status = false;
                responseService.Message = "Email/Usuário já cadastrados!";
                return responseService;
            }
            
        }
        catch (Exception ex)
        {
            responseService.Dados = null;
            responseService.Message = ex.Message;
            responseService.Status = false;
        }

        return responseService;
    }

    private bool VerificationUserAndEmailisExist(RegisterDTO userRegister)
    {
        var user = _context.Users.FirstOrDefault(userDb =>
            userDb.Email == userRegister.Email || userDb.UserName == userRegister.UserName);

        if (user != null) return false;

        return true;
    }
}