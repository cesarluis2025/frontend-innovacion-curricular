using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.DocenteDepartamento;

namespace FrontendInnovacionCurricular.Pages.DocenteDepartamento;

public class EditarModel : PageModel
{
    private readonly DocenteDepartamentoCliente _cliente;
    public EditarModel(DocenteDepartamentoCliente cliente) => _cliente = cliente;

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
            if (actual != null) { Registro.Docente = a; Registro.Departamento = b; Registro.Activo = actual.Activo; Registro.DepartamentoNombre = actual.DepartamentoNombre; }
            return Page();
        }
        TempData["Ok"] = "Cambios guardados.";
        return RedirectToPage("Index");
    }
}
