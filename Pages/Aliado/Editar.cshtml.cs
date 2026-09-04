using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.Aliado;

namespace FrontendInnovacionCurricular.Pages.Aliado;

public class EditarModel : PageModel
{
    private readonly AliadoCliente _cliente;
    public EditarModel(AliadoCliente cliente) => _cliente = cliente;

    [BindProperty]
    public Modelo Registro { get; set; } = new();

    public string? MensajeError { get; set; }

    public async Task<IActionResult> OnGetAsync(int nit)
    {
        var registro = await _cliente.ObtenerPorNitAsync(nit);
        if (registro is null) return RedirectToPage("Index");
        Registro = registro;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int nit)
    {
        var error = await _cliente.ActualizarAsync(nit, Registro);
        if (error != null) { MensajeError = error; Registro.Nit = nit; return Page(); }
        return RedirectToPage("Index");
    }
}
