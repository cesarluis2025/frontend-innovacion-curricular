using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Servicios;
using ModeloArea = FrontendInnovacionCurricular.Modelos.AreaConocimiento;

namespace FrontendInnovacionCurricular.Pages.AreaConocimiento;

public class CrearModel : PageModel
{
    private readonly AreaConocimientoCliente _cliente;
    public CrearModel(AreaConocimientoCliente cliente) => _cliente = cliente;

    [BindProperty]
    public ModeloArea Registro { get; set; } = new();

    public string? MensajeError { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        var error = await _cliente.CrearAsync(Registro);
        if (error != null)
        {
            // La API rechazó el registro (id repetido, campo muy largo, etc.)
            // Mostramos su mensaje tal cual, sin inventar uno propio.
            MensajeError = error;
            return Page();
        }
        return RedirectToPage("Index");
    }
}
