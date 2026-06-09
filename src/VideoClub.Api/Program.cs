using FluentValidation;
using MediatR;
using OpenApiUi;
using Serilog;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Articulos;
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

AppContext.SetSwitch("Npgsql.EnableDateTimeKindConversion", true);

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddDatabaseServices(builder.Configuration);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddAutoMapper(cfg => { }, typeof(Program).Assembly);

builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:7115")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseOpenApiUi(config => config.OpenApiSpecPath = "/openapi/v1.json");
}

app.UseHttpsRedirection();

app.MapGroup("/api/articulos")
    .MapArticuloEndpoints();

app.MapGroup("/api/clientes")
    .MapClienteEndpoints();

app.MapGroup("/api/elenco")
    .MapElencoEndpoints();

app.MapGroup("/api/empleados")
    .MapEmpleadoEndpoints();

app.MapGroup("/api/rentas")
    .MapRentaEndpoints();

app.MapGroup("/api/tipos-articulos")
    .MapTipoArticuloEndpoints();

app.MapGroup("/api/idiomas")
    .MapIdiomaEndpoints();

app.MapGroup("/api/generos")
    .MapGeneroEndpoints();

app.MapGroup("/api/roles-elenco")
    .MapRolElencoEndpoints();

if (app.Environment.IsDevelopment())
    await app.SeedAsync();

app.Run();
