using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Servicios;
using ModeloArea = FrontendInnovacionCurricular.Modelos.AreaConocimiento;

namespace FrontendInnovacionCurricular.Pages.AreaConocimiento;

public class EditarModel : PageModel
{
    private readonly AreaConocimientoCliente _cliente;
    public EditarModel(AreaConocimientoCliente cliente) => _cliente = cliente;

    [BindProperty]
    public ModeloArea Registro { get; set; } = new();

    public string? MensajeError { get; set; }

    // El {id} de la url llega aquí como parámetro, gracias a @page "{id:int}"
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
        if (error != null)
        {
            MensajeError = error;
            Registro.Id = id;
            return Page();
        }
        return RedirectToPage("Index");
    }
}
