using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Autorizacion;
using FrontendInnovacionCurricular.Modelos;
using FrontendInnovacionCurricular.Servicios;

namespace FrontendInnovacionCurricular.Pages.Rol;

[PermisoPagina("acceso", "escribir")]
public class CrearModel : PageModel
{
    private readonly ClienteRol _cliente;
    public CrearModel(ClienteRol cliente) => _cliente = cliente;

    [BindProperty]
    public RolCambio Registro { get; set; } = new();

    public string? MensajeError { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        MensajeError = await _cliente.CrearAsync(Registro);
        if (MensajeError is null)
        {
            TempData["Ok"] = "Rol guardado.";
            return RedirectToPage("Index");
        }
        return Page();
    }
}
