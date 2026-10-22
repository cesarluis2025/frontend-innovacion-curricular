using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Modelos;
using FrontendInnovacionCurricular.Servicios;

namespace FrontendInnovacionCurricular.Pages.Universidad;

public class DetalleModel : PageModel
{
    private readonly UniversidadCliente _cliente;
    private readonly UniversidadDetalleCliente _detalle;
    public DetalleModel(UniversidadCliente cliente, UniversidadDetalleCliente detalle)
    {
        _cliente = cliente;
        _detalle = detalle;
    }

    public FrontendInnovacionCurricular.Modelos.Universidad Maestro { get; set; } = new();
    public string? MensajeError { get; set; }
    public string? SeccionConError { get; set; }
    public List<Modelos.Facultad> Facultades { get; set; } = new();
    [BindProperty] public List<Modelos.Facultad> NuevasFacultades { get; set; } = new();

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
        Facultades = await _detalle.ListarFacultadesAsync(id);
        if (NuevasFacultades.Count == 0) for (var i = 0; i < 3; i++) NuevasFacultades.Add(new Modelos.Facultad());
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

    public async Task<IActionResult> OnPostFacultadesAsync(int id)
    {
        // Una fila sin código ni nombre se considera vacía y se ignora.
        var filas = NuevasFacultades.Where(x => !(x.Id == 0 && string.IsNullOrWhiteSpace(x.Nombre))).ToList();
        if (filas.Count == 0) return await Fallo(id, "Facultades", "Escriba al menos una fila.");
        var error = await _detalle.CrearFacultadesLoteAsync(id, filas);
        if (error != null) return await Fallo(id, "Facultades", error);
        TempData["Ok"] = $"Se guardaron {filas.Count} fila(s) de una sola vez.";
        return RedirectToPage(new { id });
    }
}
