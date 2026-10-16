using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Servicios;
using ModeloArea = FrontendInnovacionCurricular.Modelos.AreaConocimiento;

namespace FrontendInnovacionCurricular.Pages.AreaConocimiento;

public class IndexModel : PageModel
{
    private readonly AreaConocimientoCliente _cliente;
    public IndexModel(AreaConocimientoCliente cliente) => _cliente = cliente;

    public List<ModeloArea> Registros { get; set; } = new();

    // Se ejecuta cada vez que alguien visita /AreaConocimiento (GET)
    public async Task OnGetAsync()
    {
        Registros = await _cliente.ListarAsync();
    }

    // Se ejecuta cuando alguien da clic en "Eliminar" (POST, gracias a asp-page-handler="Eliminar")
    public async Task<IActionResult> OnPostEliminarAsync(int id)
    {
        await _cliente.EliminarAsync(id);
        return RedirectToPage(); // recarga la lista, ya sin ese registro
    }
}
