using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UserJWT.DTOs;
using UserJWT.Services.AuthService;

namespace UserJWT.Controller;

public static class UserController
{
    
    public static void AddUserController(this WebApplication app)
    {
        app.MapPost("login", async ([FromServices] IAuthInterface authInterface, LoginDto userLogin) =>
        {
            var response = await authInterface.Login(userLogin);

            return Results.Ok(response);
        });
        
        app.MapPost("register", async ([FromServices] IAuthInterface authInterface, RegisterDto userRegister) =>
        {
            var response = await authInterface.Register(userRegister);

            return Results.Ok(response);
        });
    }
}