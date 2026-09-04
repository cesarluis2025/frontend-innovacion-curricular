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

    public async Task OnGetAsync()
    {
        Registros = await _cliente.ListarAsync();
    }

    public async Task<IActionResult> OnPostEliminarAsync(int id)
    {
        await _cliente.EliminarAsync(id);
        return RedirectToPage();
    }
}
