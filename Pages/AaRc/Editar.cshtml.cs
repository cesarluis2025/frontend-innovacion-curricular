using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.AaRc;

namespace FrontendInnovacionCurricular.Pages.AaRc;

public class EditarModel : PageModel
{
    private readonly AaRcCliente _cliente;
    public EditarModel(AaRcCliente cliente) => _cliente = cliente;

    [BindProperty]
    public Modelo Registro { get; set; } = new();

    public string? MensajeError { get; set; }

    public async Task<IActionResult> OnGetAsync(int a, int b)
    {
        var registro = await _cliente.ObtenerAsync(a, b);
        if (registro is null) return RedirectToPage("Index");
        Registro = registro;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int a, int b)
    {
        var error = await _cliente.ActualizarAsync(a, b, Registro);
        if (error != null)
        {
            MensajeError = error;
            var actual = await _cliente.ObtenerAsync(a, b);
            if (actual != null) { Registro.ActivAcademicasIdcurso = a; Registro.RegistroCalificadoCodigo = b; Registro.ActivAcademicaNombre = actual.ActivAcademicaNombre; Registro.RegistroCalificadoNombre = actual.RegistroCalificadoNombre; }
            return Page();
        }
        return RedirectToPage("Index");
    }
}
