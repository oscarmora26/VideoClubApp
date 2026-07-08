using System.Text;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using OpenApiUi;
using Serilog;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Articulos;
using VideoClub.Api.Features.Auth;
using VideoClub.Api.Features.Clientes;
using VideoClub.Api.Features.Elenco;
using VideoClub.Api.Features.Empleados;
using VideoClub.Api.Features.Generos;
using VideoClub.Api.Features.RolesElenco;
using VideoClub.Api.Features.Idiomas;
using VideoClub.Api.Features.Rentas;
using VideoClub.Api.Features.TiposArticulos;
using VideoClub.Api.Middleware;
using VideoClub.Api.PipelineBehaviors;
using VideoClub.Api.Services;

AppContext.SetSwitch("Npgsql.EnableDateTimeKindConversion", true);

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});
builder.Services.AddDatabaseServices(builder.Configuration);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddAutoMapper(cfg => { }, typeof(Program).Assembly);

builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>));

builder.Services.AddSingleton<TokenService>();
builder.Services.AddSingleton<BearerSecuritySchemeTransformer>();

var jwtKey = builder.Configuration["Jwt:Key"]!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Administrador"));
});

var allowedOrigins = builder.Configuration["AllowedOrigins"]?.Split(';', StringSplitOptions.RemoveEmptyEntries);
if (allowedOrigins?.Length > 0)
{
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            policy.WithOrigins(allowedOrigins)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseOpenApiUi(config => config.OpenApiSpecPath = "/openapi/v1.json");
}

app.MapGroup("/api/auth")
    .MapAuthEndpoints();

app.MapGroup("/api/articulos")
    .MapArticuloEndpoints()
    .RequireAuthorization();

app.MapGroup("/api/clientes")
    .MapClienteEndpoints()
    .RequireAuthorization();

app.MapGroup("/api/elenco")
    .MapElencoEndpoints()
    .RequireAuthorization("AdminOnly");

app.MapGroup("/api/empleados")
    .MapEmpleadoEndpoints()
    .RequireAuthorization("AdminOnly");

app.MapGroup("/api/rentas")
    .MapRentaEndpoints()
    .RequireAuthorization();

app.MapGroup("/api/tipos-articulos")
    .MapTipoArticuloEndpoints()
    .RequireAuthorization();

app.MapGroup("/api/idiomas")
    .MapIdiomaEndpoints()
    .RequireAuthorization();

app.MapGroup("/api/generos")
    .MapGeneroEndpoints()
    .RequireAuthorization();

app.MapGroup("/api/roles-elenco")
    .MapRolElencoEndpoints()
    .RequireAuthorization("AdminOnly");

if (app.Environment.IsDevelopment())
    await app.SeedAsync();

app.Run();
