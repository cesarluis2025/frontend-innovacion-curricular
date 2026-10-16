using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.ProgramaAc;

namespace FrontendInnovacionCurricular.Pages.ProgramaAc;

public class CrearModel : PageModel
{
    private readonly ProgramaAcCliente _cliente;
    private readonly ProgramaCliente _programa;
    private readonly AreaConocimientoCliente _areaConocimiento;
    public CrearModel(ProgramaAcCliente cliente, ProgramaCliente programa, AreaConocimientoCliente areaConocimiento)
    {
        _cliente = cliente;
        _programa = programa;
        _areaConocimiento = areaConocimiento;
    }

    [BindProperty]
    public Modelo Registro { get; set; } = new();

    public string? MensajeError { get; set; }
    public List<SelectListItem> ListaPrograma { get; set; } = new();
    public List<SelectListItem> ListaAreaConocimiento { get; set; } = new();

    private async Task CargarListasAsync()
    {
        ListaPrograma = (await _programa.ListarAsync())
            .Select(x => new SelectListItem(x.Nombre, x.Id.ToString())).ToList();
        ListaAreaConocimiento = (await _areaConocimiento.ListarAsync())
            .Select(x => new SelectListItem(x.Disciplina + " (" + x.Area + ")", x.Id.ToString())).ToList();
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
