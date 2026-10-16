using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.Facultad;

namespace FrontendInnovacionCurricular.Pages.Facultad;

public class IndexModel : PageModel
{
    private readonly FacultadCliente _cliente;
    public IndexModel(FacultadCliente cliente) => _cliente = cliente;

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
        if (MensajeError is null) { TempData["Ok"] = "Registro retirado."; return RedirectToPage(); }
        Registros = await _cliente.ListarAsync();
        return Page();
    }
}
