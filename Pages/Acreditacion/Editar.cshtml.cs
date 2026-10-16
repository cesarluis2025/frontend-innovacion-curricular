using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.Acreditacion;

namespace FrontendInnovacionCurricular.Pages.Acreditacion;

public class EditarModel : PageModel
{
    private readonly AcreditacionCliente _cliente;
    private readonly ProgramaCliente _programa;
    public EditarModel(AcreditacionCliente cliente, ProgramaCliente programa)
    {
        _cliente = cliente;
        _programa = programa;
    }

    [BindProperty]
    public Modelo Registro { get; set; } = new();

    public string? MensajeError { get; set; }
    public List<SelectListItem> ListaPrograma { get; set; } = new();

    private async Task CargarListasAsync()
    {
        ListaPrograma = (await _programa.ListarAsync())
            .Select(x => new SelectListItem(x.Nombre, x.Id.ToString())).ToList();
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var registro = await _cliente.ObtenerPorIdAsync(id);
        if (registro is null) return RedirectToPage("Index");
        Registro = registro;
        await CargarListasAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var error = await _cliente.ActualizarAsync(id, Registro);
        if (error != null)
        {
            MensajeError = error;
            Registro.Resolucion = id;
            await CargarListasAsync();
            return Page();
        }
        return RedirectToPage("Index");
    }
}
