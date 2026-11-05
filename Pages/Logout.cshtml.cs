using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Autorizacion;

namespace FrontendInnovacionCurricular.Pages;

// Cerrar sesión borra la cookie del navegador. Es solo del lado de la pantalla: la
// credencial que ya emitió la API sigue valiendo hasta que venza (límite aceptado:
// no hay lista de revocación). Es un POST para que ningún enlace pueda cerrarla.
[SoloIdentificadoPagina]
public class LogoutModel : PageModel
{
    public IActionResult OnGet() => RedirectToPage("/Index");

    public async Task<IActionResult> OnPostAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return LocalRedirect("/Login?aviso=salio");
    }
}
