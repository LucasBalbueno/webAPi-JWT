using UserJWT.Enum;

namespace UserJWT.Models;

public class UserModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string UserName { get; set; }
    
    public string Email { get; set; }
    
    public byte[] PasswordHash { get; set; }
    
    public RoleEnum Role { get; set; }
    
    public DateTime TokenDataCreation { get; set; } = DateTime.Now;
}