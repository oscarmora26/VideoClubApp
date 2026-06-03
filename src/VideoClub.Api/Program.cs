using FluentValidation;
using MediatR;
using OpenApiUi;
using Serilog;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Articulos;
using VideoClub.Api.Features.Clientes;
using VideoClub.Api.Features.Elenco;
using VideoClub.Api.Features.Empleados;
using VideoClub.Api.Features.Idiomas;
using VideoClub.Api.Features.RentaDevolucion;
using VideoClub.Api.Features.TiposArticulos;
using VideoClub.Api.Middleware;
using VideoClub.Api.PipelineBehaviors;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();

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
    .MapRentaDevolucionEndpoints();

app.MapGroup("/api/tipos-articulos")
    .MapTipoArticuloEndpoints();

app.MapGroup("/api/idiomas")
    .MapIdiomaEndpoints();

if (app.Environment.IsDevelopment())
    await app.SeedAsync();

app.Run();
