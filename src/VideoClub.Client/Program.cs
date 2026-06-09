using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using VideoClub.Client;
using VideoClub.Client.Services.Http;
using VideoClub.Client.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiUrl = builder.Configuration["ApiUrl"] ?? builder.HostEnvironment.BaseAddress;

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) });

builder.Services.AddMudServices();

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IArticuloService, ArticuloService>();
builder.Services.AddScoped<IRentaService, RentaService>();
builder.Services.AddScoped<ITipoArticuloService, TipoArticuloService>();
builder.Services.AddScoped<IIdiomaService, IdiomaService>();
builder.Services.AddScoped<IElencoService, ElencoService>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();

await builder.Build().RunAsync();
