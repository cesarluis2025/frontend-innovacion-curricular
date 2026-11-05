using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using FrontendInnovacionCurricular.Autorizacion;
using FrontendInnovacionCurricular.Servicios;

var builder = WebApplication.CreateBuilder(args);

// v3: el filtro de permisos es GLOBAL: corre en toda página, así ninguna pantalla nueva
// puede quedar sin revisar (falla cerrado).
builder.Services.AddRazorPages().AddMvcOptions(o => o.Filters.Add<FiltroPaginaPermiso>());

// La url de la API sale de una variable de entorno (definida en
// docker-compose.yml). Así el frontend nunca toca la base de datos:
// todo pasa por aquí, por HTTP.
var urlApi = Environment.GetEnvironmentVariable("API_URL") ?? "http://localhost:8080/";

builder.Services.AddHttpClient<AreaConocimientoCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<UniversidadCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<AspectoNormativoCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<PracticaEstrategiaCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<EnfoqueCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<CarInnovacionCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<AliadoCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();

// v2: los 15 clientes de las tablas nuevas.
builder.Services.AddHttpClient<FacultadCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<ProgramaCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<AcreditacionCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<RegistroCalificadoCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<ActivAcademicaCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<PasantiaCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<PremioCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<ProgramaAcCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<ProgramaPeCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<ProgramaCiCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<AnProgramaCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<EnfoqueRcCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<AaRcCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<DocenteDepartamentoCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<AlianzaCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();

// v2: maestro-detalle (varias filas en una sola petición).
builder.Services.AddHttpClient<UniversidadDetalleCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<FacultadDetalleCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<ProgramaDetalleCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();

// ---------------------------------------------------------------------------
// v3: control de acceso del frontend.
// ---------------------------------------------------------------------------

// Cookie de SESIÓN, HttpOnly y cifrada por el servidor. La credencial que da la API va
// DENTRO de esa cookie (no en localStorage): ningún script de la página puede leerla.
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.Cookie.Name = "innovacion.sesion";
        o.Cookie.HttpOnly = true;
        o.Cookie.SameSite = SameSiteMode.Lax;
        o.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;   // en desarrollo se sirve por http
        o.SlidingExpiration = false;   // la cookie vence cuando vence la credencial, no se renueva sola
        o.LoginPath = "/Login";
        o.AccessDeniedPath = "/AccesoDenegado";
        // Una cookie que existía pero ya no sirve (venció) lleva a la entrada con un aviso.
        o.Events.OnRedirectToLogin = contexto =>
        {
            var destino = contexto.Request.Cookies.ContainsKey("innovacion.sesion")
                ? "/Login?aviso=vencida"
                : "/Login?ReturnUrl=" + Uri.EscapeDataString(contexto.Request.Path + contexto.Request.QueryString);
            contexto.Response.Redirect(destino);
            return Task.CompletedTask;
        };
    });

// Política por defecto: TODA página exige estar identificado, salvo las marcadas
// [AllowAnonymous] (hoy solo la pantalla de entrada).
builder.Services.AddAuthorization(o =>
    o.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());

// El TokenHandler lee la credencial del HttpContext de la petición en curso.
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<TokenHandler>();

// La entrada es el único cliente SIN TokenHandler: se usa antes de tener credencial.
builder.Services.AddHttpClient<ClienteSesion>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
});
builder.Services.AddHttpClient<ClientePermisos>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<ClienteUsuario>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<ClienteRol>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<ClienteRolUsuario>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi);
})
    .AddHttpMessageHandler<TokenHandler>();

// Los permisos de la persona, UNA consulta por carga de página (Scoped = por petición).
builder.Services.AddScoped<PermisosActuales>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

// Si la API responde «no sé quién eres» (la sesión venció o la cuenta dejó de estar
// activa), TokenHandler lanza esta excepción: se cierra la sesión y se lleva a la
// entrada con un aviso. Va ANTES de la autenticación para envolver todo lo demás.
app.Use(async (contexto, siguiente) =>
{
    try
    {
        await siguiente();
    }
    catch (SesionVencidaExcepcion) when (!contexto.Response.HasStarted)
    {
        await contexto.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        contexto.Response.Redirect("/Login?aviso=vencida");
    }
});

// El ORDEN importa: UseAuthentication ANTES de UseAuthorization.
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
