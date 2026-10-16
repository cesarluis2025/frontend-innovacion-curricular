using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.Programa;

namespace FrontendInnovacionCurricular.Pages.Programa;

public class CrearModel : PageModel
{
    private readonly ProgramaCliente _cliente;
    private readonly FacultadCliente _facultad;
    public CrearModel(ProgramaCliente cliente, FacultadCliente facultad)
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

    public async Task OnGetAsync()
    {
        await CargarListasAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var error = await _cliente.CrearAsync(Registro);
        if (error != null)
        {
            MensajeError = error;
            await CargarListasAsync();
            return Page();
        }
        return RedirectToPage("Index");
    }
}
