namespace UserJWT.Controller;

public static class UserController
{
    public static void AddUserController(this WebApplication app)
    {
        app.MapGet("teste", () => "Hello World!");
    }
}