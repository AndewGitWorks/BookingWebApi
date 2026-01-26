using Application.Interfaces;
using Application.Services;
using Infrastructure.Auth;
using Infrastructure.CrudRepository;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IAuthInterface, AuthRepository>();
builder.Services.AddScoped<IJwtInterface, JwtService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductInterface, ProductRepository>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<JwtService>();
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddAuthentication();
builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection(nameof(AuthSettings)));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
