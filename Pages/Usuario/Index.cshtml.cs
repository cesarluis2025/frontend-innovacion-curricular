using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Autorizacion;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.Usuario;

namespace FrontendInnovacionCurricular.Pages.Usuario;

[PermisoPagina("acceso", "leer")]
public class IndexModel : PageModel
{
    private readonly ClienteUsuario _cliente;
    public IndexModel(ClienteUsuario cliente) => _cliente = cliente;

    public List<Modelo> Registros { get; set; } = new();
    public string? MensajeError { get; set; }

    public async Task OnGetAsync() => Registros = await _cliente.ListarAsync();

    // Si la API se niega (por ejemplo, es la última persona administradora), se vuelve
    // a mostrar la lista con el motivo en lugar de fallar en silencio.
    public async Task<IActionResult> OnPostEliminarAsync(int id)
    {
        MensajeError = await _cliente.EliminarAsync(id);
        if (MensajeError is null) { TempData["Ok"] = "Persona retirada."; return RedirectToPage(); }
        Registros = await _cliente.ListarAsync();
        return Page();
    }
}
