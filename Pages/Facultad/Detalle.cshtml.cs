using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Modelos;
using FrontendInnovacionCurricular.Servicios;

namespace FrontendInnovacionCurricular.Pages.Facultad;

public class DetalleModel : PageModel
{
    private readonly FacultadCliente _cliente;
    private readonly FacultadDetalleCliente _detalle;
    public DetalleModel(FacultadCliente cliente, FacultadDetalleCliente detalle)
    {
        _cliente = cliente;
        _detalle = detalle;
    }

    public FrontendInnovacionCurricular.Modelos.Facultad Maestro { get; set; } = new();
    public string? MensajeError { get; set; }
    public string? SeccionConError { get; set; }
    public List<Modelos.Programa> Programas { get; set; } = new();
    [BindProperty] public List<Modelos.Programa> NuevasProgramas { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var maestro = await _cliente.ObtenerPorIdAsync(id);
        if (maestro is null) return RedirectToPage("Index");
        Maestro = maestro;
        await CargarAsync(id);
        return Page();
    }

    private async Task CargarAsync(int id)
    {
        Programas = await _detalle.ListarProgramasAsync(id);
        if (NuevasProgramas.Count == 0) for (var i = 0; i < 3; i++) NuevasProgramas.Add(new Modelos.Programa());
    }

    // Si la API rechaza el lote no se guardó ninguna fila; se muestra el motivo y
    // las filas escritas se conservan para corregirlas sin volver a teclearlas.
    private async Task<IActionResult> Fallo(int id, string seccion, string mensaje)
    {
        var maestro = await _cliente.ObtenerPorIdAsync(id);
        if (maestro is null) return RedirectToPage("Index");
        Maestro = maestro;
        MensajeError = mensaje;
        SeccionConError = seccion;
        await CargarAsync(id);
        return Page();
    }

    public async Task<IActionResult> OnPostProgramasAsync(int id)
    {
        // Una fila sin código ni nombre se considera vacía y se ignora.
        var filas = NuevasProgramas.Where(x => !(x.Id == 0 && string.IsNullOrWhiteSpace(x.Nombre))).ToList();
        if (filas.Count == 0) return await Fallo(id, "Programas", "Escriba al menos una fila.");
        var error = await _detalle.CrearProgramasLoteAsync(id, filas);
        if (error != null) return await Fallo(id, "Programas", error);
        TempData["Ok"] = $"Se guardaron {filas.Count} fila(s) de una sola vez.";
        return RedirectToPage(new { id });
    }
}
