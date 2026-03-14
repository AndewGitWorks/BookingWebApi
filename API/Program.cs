using API;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Auth;
using Infrastructure.CrudRepository;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.ServicesRegistration();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<JwtService>();
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddAuthentication();
builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection(nameof(AuthSettings)));

//Serilog
builder.Host.UseSerilog((context, configuration) =>
configuration
.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
