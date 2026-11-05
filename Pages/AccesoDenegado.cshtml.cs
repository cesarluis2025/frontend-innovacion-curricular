using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Autorizacion;

namespace FrontendInnovacionCurricular.Pages;

// La pantalla «sin permiso». La persona SÍ está identificada (si no, iría a la entrada);
// lo que no tiene es permiso para esta sección. Responde con código 403 para que se
// pueda comprobar con las herramientas del navegador, aunque la pantalla no lo diga.
[SoloIdentificadoPagina]
public class AccesoDenegadoModel : PageModel
{
    public void OnGet() => Response.StatusCode = StatusCodes.Status403Forbidden;
}
