using System.ComponentModel.DataAnnotations;

namespace UserJWT.DTOs;

public class LoginDto
{
    [EmailAddress(ErrorMessage = "Email inválido!")]
    public string Email { get; set; }
    
    public string Password { get; set; }
}