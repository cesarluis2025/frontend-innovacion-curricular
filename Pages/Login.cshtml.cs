using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Servicios;

namespace FrontendInnovacionCurricular.Pages;

// La pantalla de entrada. Abierta a propósito ([AllowAnonymous]): es lo único que se
// puede ver sin identificarse (con la salida, que es un POST).
[AllowAnonymous]
public class LoginModel : PageModel
{
    private readonly ClienteSesion _sesion;
    public LoginModel(ClienteSesion sesion) => _sesion = sesion;

    [BindProperty]
    public string Usuario { get; set; } = string.Empty;

    [BindProperty]
    public string Clave { get; set; } = string.Empty;

    public string? MensajeError { get; set; }
    public string? Aviso { get; set; }

    public IActionResult OnGet(string? aviso)
    {
        // Quien ya está dentro no necesita la pantalla de entrada.
        if (User.Identity?.IsAuthenticated == true && aviso is null) return LocalRedirect("/");
        Aviso = TextoDelAviso(aviso);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl)
    {
        var (sesion, error) = await _sesion.IniciarAsync(Usuario ?? "", Clave ?? "");
        if (sesion is null)
        {
            // El mensaje es el de la API: el mismo para «no existe» y «clave equivocada»,
            // y solo con la clave correcta dice si la cuenta está inactiva o sin rol.
            MensajeError = error;
            Clave = string.Empty;
            return Page();
        }

        var identidad = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        identidad.AddClaim(new Claim(ClaimTypes.NameIdentifier, sesion.Usuario.Id.ToString()));
        identidad.AddClaim(new Claim(ClaimTypes.Name, sesion.Usuario.Username));
        foreach (var rol in sesion.Usuario.Roles) identidad.AddClaim(new Claim(ClaimTypes.Role, rol));

        // Cookie de SESIÓN (IsPersistent = false: muere al cerrar el navegador). La
        // credencial de la API viaja DENTRO de la cookie, que el servidor cifra; no se
        // guarda en localStorage ni en ninguna cookie legible por la página. La cookie
        // deja de servir cuando vence la credencial.
        var propiedades = new AuthenticationProperties { IsPersistent = false, ExpiresUtc = sesion.ExpiraEn };
        propiedades.StoreTokens(new[] { new AuthenticationToken { Name = "access_token", Value = sesion.Token } });
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identidad), propiedades);

        return LocalRedirect(Url.IsLocalUrl(returnUrl) ? returnUrl! : "/");
    }

    private static string? TextoDelAviso(string? aviso) => aviso switch
    {
        "vencida" => "Su sesión venció. Vuelva a identificarse.",
        "salio" => "Cerró la sesión.",
        _ => null,
    };
}
