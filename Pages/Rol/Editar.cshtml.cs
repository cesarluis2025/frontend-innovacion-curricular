using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Autorizacion;
using FrontendInnovacionCurricular.Modelos;
using FrontendInnovacionCurricular.Servicios;

namespace FrontendInnovacionCurricular.Pages.Rol;

[PermisoPagina("acceso", "escribir")]
public class EditarModel : PageModel
{
    private readonly ClienteRol _cliente;
    public EditarModel(ClienteRol cliente) => _cliente = cliente;

    [BindProperty]
    public RolCambio Registro { get; set; } = new();

    public string? MensajeError { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var actual = await _cliente.ObtenerPorIdAsync(id);
        if (actual is null) return NotFound();
        Registro = new RolCambio { Nombre = actual.Nombre, Descripcion = actual.Descripcion };
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        MensajeError = await _cliente.ActualizarAsync(id, Registro);
        if (MensajeError is null)
        {
            TempData["Ok"] = "Cambios guardados.";
            return RedirectToPage("Index");
        }
        return Page();
    }
}
