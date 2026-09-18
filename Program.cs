using FrontendInnovacionCurricular.Servicios;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// La url de la API sale de una variable de entorno (definida en
// docker-compose.yml). Así el frontend nunca toca la base de datos:
// todo pasa por aquí, por HTTP.
var urlApi = Environment.GetEnvironmentVariable("API_URL") ?? "http://localhost:8080/";

builder.Services.AddHttpClient<AreaConocimientoCliente>(cliente => { cliente.BaseAddress = new Uri(urlApi); });
builder.Services.AddHttpClient<UniversidadCliente>(cliente => { cliente.BaseAddress = new Uri(urlApi); });
builder.Services.AddHttpClient<AspectoNormativoCliente>(cliente => { cliente.BaseAddress = new Uri(urlApi); });
builder.Services.AddHttpClient<PracticaEstrategiaCliente>(cliente => { cliente.BaseAddress = new Uri(urlApi); });
builder.Services.AddHttpClient<EnfoqueCliente>(cliente => { cliente.BaseAddress = new Uri(urlApi); });
builder.Services.AddHttpClient<CarInnovacionCliente>(cliente => { cliente.BaseAddress = new Uri(urlApi); });
builder.Services.AddHttpClient<AliadoCliente>(cliente => { cliente.BaseAddress = new Uri(urlApi); });

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();
