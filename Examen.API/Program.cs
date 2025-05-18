using Examen.API.AutoMapper;
using Examen.API.Business.Interfaces;
using Examen.API.Business.Services;
using Examen.API.Business.Settings;
using Examen.API.Data.Interfaces;
using Examen.API.Data.UnitOfWork;
using Examen.API.Utilidades.Interfaces;
using Examen.API.Utilidades.Settings;
using Examen.API.Utilidades.Utilidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Examen.API.Validators;
using Examen.API.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<ExamenContext>(options => options.UseSqlServer(builder.Configuration.
//GetConnectionString("defaultConnection")));

var connectionString = builder.Configuration.GetConnectionString("ExamenSQLite");

builder.Services.AddDbContext<ExamenContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

// Add services to the container.
builder.Services.AddAutoMapper(typeof(MappingProfile));  // Registra el perfil de AutoMapper

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Servicios
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IPersonaService, Persona_Service>();
builder.Services.AddScoped<IFacturaService, FacturaService>();

// Servicios de validacion de FluentValidation
builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PersonaViewModelValidator>());

// Servicio Log
builder.Services.Configure<LogSettings>(builder.Configuration.GetSection("LogSettings"));
builder.Services.AddSingleton<ILogService, LogService>();

// Configuracion de JWT
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<IJwtService, JwtService>();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
