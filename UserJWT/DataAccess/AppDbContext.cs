using Microsoft.EntityFrameworkCore;
using UserJWT.Models;

namespace UserJWT.DataAccess;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<UserModel> Users { get; set; }
}