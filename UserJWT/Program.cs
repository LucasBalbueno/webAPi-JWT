using Microsoft.EntityFrameworkCore;
using UserJWT.Controller;
using UserJWT.DataAccess;
using UserJWT.Services.AuthService;
using UserJWT.Services.PasswordService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAuthInterface, AuthService>();
builder.Services.AddScoped<IPasswordInterface, PasswordService>();

// Injeção de dependências com as configurações do banco de dados
builder.Services.AddDbContext<AppDbContext>(option =>
{
    var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
    option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Configurando as rotas
app.AddUserController();

app.Run();
