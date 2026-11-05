using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Autorizacion;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.Rol;

namespace FrontendInnovacionCurricular.Pages.Rol;

[PermisoPagina("acceso", "leer")]
public class IndexModel : PageModel
{
    private readonly ClienteRol _cliente;
    public IndexModel(ClienteRol cliente) => _cliente = cliente;

    public List<Modelo> Registros { get; set; } = new();
    public string? MensajeError { get; set; }

    public async Task OnGetAsync() => Registros = await _cliente.ListarAsync();

    // La API se niega a retirar un rol base o uno con personas asignadas: el motivo se muestra aquí.
    public async Task<IActionResult> OnPostEliminarAsync(int id)
    {
        MensajeError = await _cliente.EliminarAsync(id);
        if (MensajeError is null) { TempData["Ok"] = "Rol retirado."; return RedirectToPage(); }
        Registros = await _cliente.ListarAsync();
        return Page();
    }
}
