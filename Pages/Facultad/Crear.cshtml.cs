using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.Facultad;

namespace FrontendInnovacionCurricular.Pages.Facultad;

public class CrearModel : PageModel
{
    private readonly FacultadCliente _cliente;
    private readonly UniversidadCliente _universidad;
    public CrearModel(FacultadCliente cliente, UniversidadCliente universidad)
    {
        _cliente = cliente;
        _universidad = universidad;
    }

    [BindProperty]
    public Modelo Registro { get; set; } = new();

    public string? MensajeError { get; set; }
    public List<SelectListItem> ListaUniversidad { get; set; } = new();

    private async Task CargarListasAsync()
    {
        ListaUniversidad = (await _universidad.ListarAsync())
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
