using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.AspectoNormativo;

namespace FrontendInnovacionCurricular.Pages.AspectoNormativo;

public class IndexModel : PageModel
{
    private readonly AspectoNormativoCliente _cliente;
    public IndexModel(AspectoNormativoCliente cliente) => _cliente = cliente;

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
