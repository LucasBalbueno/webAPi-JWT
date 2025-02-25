using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API.DataAccess;

public class ConnectionContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=localhost;Port=3306;Database=TestAPiWithJWT;User=root;Password=root1234;";
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}