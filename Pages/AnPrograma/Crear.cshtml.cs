using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.AnPrograma;

namespace FrontendInnovacionCurricular.Pages.AnPrograma;

public class CrearModel : PageModel
{
    private readonly AnProgramaCliente _cliente;
    private readonly AspectoNormativoCliente _aspectoNormativo;
    private readonly ProgramaCliente _programa;
    public CrearModel(AnProgramaCliente cliente, AspectoNormativoCliente aspectoNormativo, ProgramaCliente programa)
    {
        _cliente = cliente;
        _aspectoNormativo = aspectoNormativo;
        _programa = programa;
    }

    [BindProperty]
    public Modelo Registro { get; set; } = new();

    public string? MensajeError { get; set; }
    public List<SelectListItem> ListaAspectoNormativo { get; set; } = new();
    public List<SelectListItem> ListaPrograma { get; set; } = new();

    private async Task CargarListasAsync()
    {
        ListaAspectoNormativo = (await _aspectoNormativo.ListarAsync())
            .Select(x => new SelectListItem(x.Tipo + ": " + x.Descripcion, x.Id.ToString())).ToList();
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
        TempData["Ok"] = "Registro guardado.";
        return RedirectToPage("Index");
    }
}
