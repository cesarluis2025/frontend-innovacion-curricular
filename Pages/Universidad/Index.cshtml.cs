using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.Universidad;

namespace FrontendInnovacionCurricular.Pages.Universidad;

public class IndexModel : PageModel
{
    private readonly UniversidadCliente _cliente;
    public IndexModel(UniversidadCliente cliente) => _cliente = cliente;

    public List<Modelo> Registros { get; set; } = new();
    public string? MensajeError { get; set; }

    public async Task OnGetAsync()
    {
        Registros = await _cliente.ListarAsync();
    }

    public async Task<IActionResult> OnPostEliminarAsync(int id)
    {
        // Una universidad con facultades activas no se retira: se muestra el motivo.
        MensajeError = await _cliente.EliminarAsync(id);
        if (MensajeError is null) return RedirectToPage();
        Registros = await _cliente.ListarAsync();
        return Page();
    }
}
