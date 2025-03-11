using System.ComponentModel.DataAnnotations;
using UserJWT.Enum;

namespace UserJWT.DTOs;

public class RegisterDto
{
    public string Name { get; set; }
    
    public string UserName { get; set; }
    
    [EmailAddress(ErrorMessage = "Email inválido!")]
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public string PasswordConfirmation { get; set; }
    
    public RoleEnum Role { get; set; }
}