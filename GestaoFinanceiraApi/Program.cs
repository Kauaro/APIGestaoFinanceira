using GestaoFinanceiraApi.Data.Context;
using GestaoFinanceiraApi.Data.Repositories;
using GestaoFinanceiraApi.Data.Repositories.Interface;
using GestaoFinanceiraApi.Data.Security;
using GestaoFinanceiraApi.Data.Security.Interface;
using GestaoFinanceiraApi.Services;
using GestaoFinanceiraApi.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);


builder.Services.AddCors(options =>
{
    options.AddPolicy("WebApp",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5173") // porta do React
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});




builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            )
        };
    });


builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

builder.Services.AddAuthorization();



builder.Services.AddScoped<AuthService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();


builder.Services.AddScoped<IGanhosRepository, GanhosRepository>();
builder.Services.AddScoped<IGanhosService, GanhosService>();


builder.Services.AddScoped<IGastosService, GastosService>();
builder.Services.AddScoped<IGastosRepository, GastosRepository>();


builder.Services.AddScoped<IResumoFinanceiroService, ResumoFinanceiroService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("WebApp");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
