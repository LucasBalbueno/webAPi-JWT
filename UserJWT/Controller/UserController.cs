using Microsoft.AspNetCore.Http.HttpResults;
using UserJWT.DTOs;

namespace UserJWT.Controller;

public static class UserController
{
    public static void AddUserController(this WebApplication app)
    {
        app.MapPost("register", (RegisterDTO userRegister) =>
        {
            return Results.Ok();
        });
    }
}