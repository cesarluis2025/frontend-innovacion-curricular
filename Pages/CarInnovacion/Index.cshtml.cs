using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.CarInnovacion;

namespace FrontendInnovacionCurricular.Pages.CarInnovacion;

public class IndexModel : PageModel
{
    private readonly CarInnovacionCliente _cliente;
    public IndexModel(CarInnovacionCliente cliente) => _cliente = cliente;

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
