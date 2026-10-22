using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.Programa;

namespace FrontendInnovacionCurricular.Pages.Programa;

public class EditarModel : PageModel
{
    private readonly ProgramaCliente _cliente;
    private readonly FacultadCliente _facultad;
    public EditarModel(ProgramaCliente cliente, FacultadCliente facultad)
    {
        _cliente = cliente;
        _facultad = facultad;
    }

    [BindProperty]
    public Modelo Registro { get; set; } = new();

    public string? MensajeError { get; set; }
    public List<SelectListItem> ListaFacultad { get; set; } = new();

    private async Task CargarListasAsync()
    {
        ListaFacultad = (await _facultad.ListarAsync())
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
            Registro.Id = id;
            await CargarListasAsync();
            return Page();
        }
        TempData["Ok"] = "Cambios guardados.";
        return RedirectToPage("Index");
    }
}
