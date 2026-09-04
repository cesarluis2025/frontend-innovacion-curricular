using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.AspectoNormativo;

namespace FrontendInnovacionCurricular.Pages.AspectoNormativo;

public class EditarModel : PageModel
{
    private readonly AspectoNormativoCliente _cliente;
    public EditarModel(AspectoNormativoCliente cliente) => _cliente = cliente;

    [BindProperty]
    public Modelo Registro { get; set; } = new();

    public string? MensajeError { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var registro = await _cliente.ObtenerPorIdAsync(id);
        if (registro is null) return RedirectToPage("Index");
        Registro = registro;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var error = await _cliente.ActualizarAsync(id, Registro);
        if (error != null) { MensajeError = error; Registro.Id = id; return Page(); }
        return RedirectToPage("Index");
    }
}
