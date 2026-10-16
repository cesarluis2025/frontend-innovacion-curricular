using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FrontendInnovacionCurricular.Servicios;
using Modelo = FrontendInnovacionCurricular.Modelos.EnfoqueRc;

namespace FrontendInnovacionCurricular.Pages.EnfoqueRc;

public class CrearModel : PageModel
{
    private readonly EnfoqueRcCliente _cliente;
    private readonly EnfoqueCliente _enfoque;
    private readonly RegistroCalificadoCliente _registroCalificado;
    public CrearModel(EnfoqueRcCliente cliente, EnfoqueCliente enfoque, RegistroCalificadoCliente registroCalificado)
    {
        _cliente = cliente;
        _enfoque = enfoque;
        _registroCalificado = registroCalificado;
    }

    [BindProperty]
    public Modelo Registro { get; set; } = new();

    public string? MensajeError { get; set; }
    public List<SelectListItem> ListaEnfoque { get; set; } = new();
    public List<SelectListItem> ListaRegistroCalificado { get; set; } = new();

    private async Task CargarListasAsync()
    {
        ListaEnfoque = (await _enfoque.ListarAsync())
            .Select(x => new SelectListItem(x.Nombre, x.Id.ToString())).ToList();
        ListaRegistroCalificado = (await _registroCalificado.ListarAsync())
            .Select(x => new SelectListItem("Registro " + x.Codigo + " (" + x.ProgramaNombre + ")", x.Codigo.ToString())).ToList();
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
