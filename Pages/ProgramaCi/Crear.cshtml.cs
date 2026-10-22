using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.ProgramaCi;

namespace FrontendInnovacionCurricular.Pages.ProgramaCi;

public class CrearModel : PageModel
{
    private readonly ProgramaCiCliente _cliente;
    private readonly ProgramaCliente _programa;
    private readonly CarInnovacionCliente _carInnovacion;
    public CrearModel(ProgramaCiCliente cliente, ProgramaCliente programa, CarInnovacionCliente carInnovacion)
    {
        _cliente = cliente;
        _programa = programa;
        _carInnovacion = carInnovacion;
    }

    [BindProperty]
    public Modelo Registro { get; set; } = new();

    public string? MensajeError { get; set; }
    public List<SelectListItem> ListaPrograma { get; set; } = new();
    public List<SelectListItem> ListaCarInnovacion { get; set; } = new();

    private async Task CargarListasAsync()
    {
        ListaPrograma = (await _programa.ListarAsync())
            .Select(x => new SelectListItem(x.Nombre, x.Id.ToString())).ToList();
        ListaCarInnovacion = (await _carInnovacion.ListarAsync())
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
        TempData["Ok"] = "Registro guardado.";
        return RedirectToPage("Index");
    }
}
