using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.Acreditacion;

namespace FrontendInnovacionCurricular.Pages.Acreditacion;

public class IndexModel : PageModel
{
    private readonly AcreditacionCliente _cliente;
    public IndexModel(AcreditacionCliente cliente) => _cliente = cliente;

    public List<Modelo> Registros { get; set; } = new();
    public string? MensajeError { get; set; }

    public async Task OnGetAsync()
    {
        Registros = await _cliente.ListarAsync();
    }

    // Si la API se niega (por ejemplo, tiene registros activos que dependen de este),
    // se vuelve a mostrar la lista con el motivo en lugar de fallar en silencio.
    public async Task<IActionResult> OnPostEliminarAsync(int id)
    {
        MensajeError = await _cliente.EliminarAsync(id);
        if (MensajeError is null) return RedirectToPage();
        Registros = await _cliente.ListarAsync();
        return Page();
    }
}
