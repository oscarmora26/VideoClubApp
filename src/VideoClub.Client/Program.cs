using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using VideoClub.Client;
using VideoClub.Client.Services;
using VideoClub.Client.Services.Http;
using VideoClub.Client.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiUrl = builder.Configuration["ApiUrl"] ?? builder.HostEnvironment.BaseAddress;

builder.Services.AddTransient<JwtDelegatingHandler>();
builder.Services.AddScoped(sp =>
{
    JwtDelegatingHandler? handler = sp.GetRequiredService<JwtDelegatingHandler>();
    handler.InnerHandler = new HttpClientHandler();
    return new HttpClient(handler) { BaseAddress = new Uri(apiUrl) };
});

builder.Services.AddMudServices();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IArticuloService, ArticuloService>();
builder.Services.AddScoped<IRentaService, RentaService>();
builder.Services.AddScoped<ITipoArticuloService, TipoArticuloService>();
builder.Services.AddScoped<IIdiomaService, IdiomaService>();
builder.Services.AddScoped<IElencoService, ElencoService>();
builder.Services.AddScoped<IRolElencoService, RolElencoService>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();

await builder.Build().RunAsync();
