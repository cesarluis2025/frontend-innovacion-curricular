using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.Alianza;

namespace FrontendInnovacionCurricular.Pages.Alianza;

public class CrearModel : PageModel
{
    private readonly AlianzaCliente _cliente;
    private readonly AliadoCliente _aliado;
    private readonly ProgramaCliente _programa;
    public CrearModel(AlianzaCliente cliente, AliadoCliente aliado, ProgramaCliente programa)
    {
        _cliente = cliente;
        _aliado = aliado;
        _programa = programa;
    }

    [BindProperty]
    public Modelo Registro { get; set; } = new();

    public string? MensajeError { get; set; }
    public List<SelectListItem> ListaAliado { get; set; } = new();
    public List<SelectListItem> ListaPrograma { get; set; } = new();

    private async Task CargarListasAsync()
    {
        ListaAliado = (await _aliado.ListarAsync())
            .Select(x => new SelectListItem(x.RazonSocial, x.Nit.ToString())).ToList();
        ListaPrograma = (await _programa.ListarAsync())
            .Select(x => new SelectListItem(x.Nombre, x.Id.ToString())).ToList();
    }

    public async Task OnGetAsync()
    {
        await CargarListasAsync();
    }

    // Asignar una pareja que ya existe da un mensaje claro; si estaba retirada, se reactiva.
    public async Task<IActionResult> OnPostAsync()
    {
        var error = await _cliente.AsignarAsync(Registro);
        if (error != null)
        {
            MensajeError = error;
            await CargarListasAsync();
            return Page();
        }
        return RedirectToPage("Index");
    }
}
