using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.Enfoque;

namespace FrontendInnovacionCurricular.Pages.Enfoque;

public class IndexModel : PageModel
{
    private readonly EnfoqueCliente _cliente;
    public IndexModel(EnfoqueCliente cliente) => _cliente = cliente;

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
