using UserJWT.DataAccess;
using UserJWT.DTOs;
using UserJWT.Models;
using UserJWT.Services.PasswordService;

namespace UserJWT.Services.AuthService;

public class AuthService : IAuthInterface
{
    private readonly AppDbContext _context;
    private readonly IPasswordInterface _passwordInteface;

    public AuthService(AppDbContext context, IPasswordInterface passwordInterface)
    {
        _context = context;
        _passwordInteface = passwordInterface;
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
            
            _passwordInteface.CreatePasswordHash(userRegister.Password, out byte[] passwordHash);

            UserModel user = new UserModel()
            {
                Name = userRegister.Name,
                UserName = userRegister.UserName,
                Email = userRegister.Email,
                Role = userRegister.Role,
                PasswordHash = passwordHash
            };

            _context.Add(user);
            await _context.SaveChangesAsync();

            responseService.Message = "Usuário criado com sucesso!";
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