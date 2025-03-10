using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UserJWT.DTOs;
using UserJWT.Services.AuthService;

namespace UserJWT.Controller;

public static class UserController
{
    
    public static void AddUserController(this WebApplication app)
    {
        
        app.MapPost("register", async ([FromServices] IAuthInterface authInterface, RegisterDTO userRegister) =>
        {
            var response = await authInterface.Register(userRegister);

            return Results.Ok(response);
        });
    }
}