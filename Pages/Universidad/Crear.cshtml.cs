using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.Universidad;

namespace FrontendInnovacionCurricular.Pages.Universidad;

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
        return RedirectToPage("Index");
    }
}
