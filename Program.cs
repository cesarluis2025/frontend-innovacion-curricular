using FrontendInnovacionCurricular.Servicios;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// La url de la API sale de una variable de entorno (definida en
// docker-compose.yml). Así el frontend nunca toca la base de datos:
// todo pasa por aquí, por HTTP.
var urlApi = Environment.GetEnvironmentVariable("API_URL") ?? "http://localhost:8080/";

builder.Services.AddHttpClient<AreaConocimientoCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
});
builder.Services.AddHttpClient<UniversidadCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
});
builder.Services.AddHttpClient<AspectoNormativoCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
});
builder.Services.AddHttpClient<PracticaEstrategiaCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
});
builder.Services.AddHttpClient<EnfoqueCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
});
builder.Services.AddHttpClient<CarInnovacionCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
});
builder.Services.AddHttpClient<AliadoCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
});

// v2: los 15 clientes de las tablas nuevas.
builder.Services.AddHttpClient<FacultadCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
});
builder.Services.AddHttpClient<ProgramaCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
});
builder.Services.AddHttpClient<AcreditacionCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
});
builder.Services.AddHttpClient<RegistroCalificadoCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
});
builder.Services.AddHttpClient<ActivAcademicaCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
});
builder.Services.AddHttpClient<PasantiaCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
});
builder.Services.AddHttpClient<PremioCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
});
builder.Services.AddHttpClient<ProgramaAcCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
});
builder.Services.AddHttpClient<ProgramaPeCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
});
builder.Services.AddHttpClient<ProgramaCiCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
});
builder.Services.AddHttpClient<AnProgramaCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
});
builder.Services.AddHttpClient<EnfoqueRcCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
});
builder.Services.AddHttpClient<AaRcCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
});
builder.Services.AddHttpClient<DocenteDepartamentoCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
});
builder.Services.AddHttpClient<AlianzaCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();
