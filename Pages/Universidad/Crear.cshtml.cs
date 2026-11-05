using Microsoft.AspNetCore.Mvc;
using FrontendInnovacionCurricular.Autorizacion;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.Universidad;

namespace FrontendInnovacionCurricular.Pages.Universidad;

[PermisoPagina("modulo", "escribir")]
public class CrearModel : PageModel
{
    private readonly UniversidadCliente _cliente;
    public CrearModel(UniversidadCliente cliente) => _cliente = cliente;

    [BindProperty]
    public Modelo Registro { get; set; } = new();

    public string? MensajeError { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        var error = await _cliente.CrearAsync(Registro);
        if (error != null) { MensajeError = error; return Page(); }
        TempData["Ok"] = "Registro guardado.";
        return RedirectToPage("Index");
    }
}
