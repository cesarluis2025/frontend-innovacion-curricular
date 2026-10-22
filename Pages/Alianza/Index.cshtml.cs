using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.Alianza;

namespace FrontendInnovacionCurricular.Pages.Alianza;

public class IndexModel : PageModel
{
    private readonly AlianzaCliente _cliente;
    public IndexModel(AlianzaCliente cliente) => _cliente = cliente;

    public List<Modelo> Registros { get; set; } = new();
    public string? MensajeError { get; set; }

    public async Task OnGetAsync()
    {
        Registros = await _cliente.ListarAsync();
    }

    public async Task<IActionResult> OnPostRetirarAsync(int a, int b)
    {
        MensajeError = await _cliente.RetirarAsync(a, b);
        if (MensajeError is null) { TempData["Ok"] = "Registro retirado."; return RedirectToPage(); }
        Registros = await _cliente.ListarAsync();
        return Page();
    }
}
