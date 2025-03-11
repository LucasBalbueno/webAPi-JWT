using Microsoft.EntityFrameworkCore;
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

    public async Task<Response<string>> Login(LoginDto userlogin)
    {
        Response<string> responseService = new Response<string>();

        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(userDb => userDb.Email == userlogin.Email);

            if (user == null)
            {
                responseService.Message = "Credenciais inválidas!";
                responseService.Status = false;
                return responseService;
            }

            if (!_passwordInteface.VerifyPasswordHash(userlogin.Password, user.PasswordHash, user.PasswordSalt))
            {
                responseService.Message = "Credenciais inválidas!";
                responseService.Status = false;
                return responseService;
            }

            var token = _passwordInteface.CreateTokenJwt(user);

            responseService.Message = "Usuário logado com sucesso";
            responseService.Dados = token;

        }
        catch (Exception ex)
        {
            responseService.Dados = null;
            responseService.Message = ex.Message;
            responseService.Status = false;
        }
        
        return responseService;
    }
    
    public async Task<Response<RegisterDto>> Register(RegisterDto userRegister)
    {
        Response<RegisterDto> responseService = new Response<RegisterDto>();

        try
        {
            if (!VerificationUserAndEmailisExist(userRegister))
            {
                responseService.Dados = null;
                responseService.Status = false;
                responseService.Message = "Email/Usuário já cadastrados!";
                return responseService;
            }
            
            _passwordInteface.CreatePasswordHash(userRegister.Password, out byte[] passwordHash, out byte[] passwordSalt);

            UserModel user = new UserModel()
            {
                Name = userRegister.Name,
                UserName = userRegister.UserName,
                Email = userRegister.Email,
                Role = userRegister.Role,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
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

    private bool VerificationUserAndEmailisExist(RegisterDto userRegister)
    {
        var user = _context.Users.FirstOrDefault(userDb =>
            userDb.Email == userRegister.Email || userDb.UserName == userRegister.UserName);

        if (user != null) return false;

        return true;
    }
}