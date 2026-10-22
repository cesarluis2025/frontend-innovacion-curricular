using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.Pasantia;

namespace FrontendInnovacionCurricular.Pages.Pasantia;

public class CrearModel : PageModel
{
    private readonly PasantiaCliente _cliente;
    private readonly ProgramaCliente _programa;
    public CrearModel(PasantiaCliente cliente, ProgramaCliente programa)
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
        TempData["Ok"] = "Registro guardado.";
        return RedirectToPage("Index");
    }
}
