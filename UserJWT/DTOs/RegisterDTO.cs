using System.ComponentModel.DataAnnotations;

namespace UserJWT.DTOs;

public class RegisterDTO
{
    public string Name { get; set; }
    
    [Required(ErrorMessage = "O campo UserName é obrigatório")]
    public string UserName { get; set; }
    
    [Required(ErrorMessage = "O campo Email é obrigatório"), EmailAddress(ErrorMessage = "Email inválido!")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "O campo Password é obrigatório")]
    public string Password { get; set; }
    
    [Compare("Password", ErrorMessage = "Senhas não coincidem!")]
    public string PasswordConfirmation { get; set; }
    
    [Required(ErrorMessage = "O campo Role é obrigatório")]
    public string Role { get; set; }
}