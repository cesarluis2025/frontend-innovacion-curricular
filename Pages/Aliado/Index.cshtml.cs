using Microsoft.AspNetCore.Mvc;
using FrontendInnovacionCurricular.Autorizacion;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.Aliado;

namespace FrontendInnovacionCurricular.Pages.Aliado;

[PermisoPagina("modulo", "leer")]
public class IndexModel : PageModel
{
    private readonly AliadoCliente _cliente;
    public IndexModel(AliadoCliente cliente) => _cliente = cliente;

    public List<Modelo> Registros { get; set; } = new();

    public async Task OnGetAsync()
    {
        Registros = await _cliente.ListarAsync();
    }

    public async Task<IActionResult> OnPostEliminarAsync(int nit)
    {
        await _cliente.EliminarAsync(nit);
        TempData["Ok"] = "Registro retirado.";
        return RedirectToPage();
    }
}
