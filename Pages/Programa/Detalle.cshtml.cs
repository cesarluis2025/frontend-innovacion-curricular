using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Modelos;
using FrontendInnovacionCurricular.Servicios;

namespace FrontendInnovacionCurricular.Pages.Programa;

public class DetalleModel : PageModel
{
    private readonly ProgramaCliente _cliente;
    private readonly ProgramaDetalleCliente _detalle;
    public DetalleModel(ProgramaCliente cliente, ProgramaDetalleCliente detalle)
    {
        _cliente = cliente;
        _detalle = detalle;
    }

    public FrontendInnovacionCurricular.Modelos.Programa Maestro { get; set; } = new();
    public string? MensajeError { get; set; }
    public string? SeccionConError { get; set; }
    public List<Modelos.Acreditacion> Acreditaciones { get; set; } = new();
    [BindProperty] public List<Modelos.Acreditacion> NuevasAcreditaciones { get; set; } = new();
    public List<Modelos.RegistroCalificado> RegistrosCalificados { get; set; } = new();
    [BindProperty] public List<Modelos.RegistroCalificado> NuevasRegistrosCalificados { get; set; } = new();
    public List<Modelos.ActivAcademica> ActividadesAcademicas { get; set; } = new();
    [BindProperty] public List<Modelos.ActivAcademica> NuevasActividadesAcademicas { get; set; } = new();
    public List<Modelos.Pasantia> Pasantias { get; set; } = new();
    [BindProperty] public List<Modelos.Pasantia> NuevasPasantias { get; set; } = new();
    public List<Modelos.Premio> Premios { get; set; } = new();
    [BindProperty] public List<Modelos.Premio> NuevasPremios { get; set; } = new();

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
        Acreditaciones = await _detalle.ListarAcreditacionesAsync(id);
        if (NuevasAcreditaciones.Count == 0) for (var i = 0; i < 3; i++) NuevasAcreditaciones.Add(new Modelos.Acreditacion());
        RegistrosCalificados = await _detalle.ListarRegistrosCalificadosAsync(id);
        if (NuevasRegistrosCalificados.Count == 0) for (var i = 0; i < 3; i++) NuevasRegistrosCalificados.Add(new Modelos.RegistroCalificado());
        ActividadesAcademicas = await _detalle.ListarActividadesAcademicasAsync(id);
        if (NuevasActividadesAcademicas.Count == 0) for (var i = 0; i < 3; i++) NuevasActividadesAcademicas.Add(new Modelos.ActivAcademica());
        Pasantias = await _detalle.ListarPasantiasAsync(id);
        if (NuevasPasantias.Count == 0) for (var i = 0; i < 3; i++) NuevasPasantias.Add(new Modelos.Pasantia());
        Premios = await _detalle.ListarPremiosAsync(id);
        if (NuevasPremios.Count == 0) for (var i = 0; i < 3; i++) NuevasPremios.Add(new Modelos.Premio());
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

    public async Task<IActionResult> OnPostAcreditacionesAsync(int id)
    {
        // Una fila sin código ni nombre se considera vacía y se ignora.
        var filas = NuevasAcreditaciones.Where(x => !(x.Resolucion == 0 && string.IsNullOrWhiteSpace(x.Tipo))).ToList();
        if (filas.Count == 0) return await Fallo(id, "Acreditaciones", "Escriba al menos una fila.");
        var error = await _detalle.CrearAcreditacionesLoteAsync(id, filas);
        if (error != null) return await Fallo(id, "Acreditaciones", error);
        TempData["Ok"] = $"Se guardaron {filas.Count} fila(s) de una sola vez.";
        return RedirectToPage(new { id });
    }
    public async Task<IActionResult> OnPostRegistrosCalificadosAsync(int id)
    {
        // Una fila sin código ni nombre se considera vacía y se ignora.
        var filas = NuevasRegistrosCalificados.Where(x => !(x.Codigo == 0 && string.IsNullOrWhiteSpace(x.CantCreditos))).ToList();
        if (filas.Count == 0) return await Fallo(id, "RegistrosCalificados", "Escriba al menos una fila.");
        var error = await _detalle.CrearRegistrosCalificadosLoteAsync(id, filas);
        if (error != null) return await Fallo(id, "RegistrosCalificados", error);
        TempData["Ok"] = $"Se guardaron {filas.Count} fila(s) de una sola vez.";
        return RedirectToPage(new { id });
    }
    public async Task<IActionResult> OnPostActividadesAcademicasAsync(int id)
    {
        // Una fila sin código ni nombre se considera vacía y se ignora.
        var filas = NuevasActividadesAcademicas.Where(x => !(x.Id == 0 && string.IsNullOrWhiteSpace(x.Nombre))).ToList();
        if (filas.Count == 0) return await Fallo(id, "ActividadesAcademicas", "Escriba al menos una fila.");
        var error = await _detalle.CrearActividadesAcademicasLoteAsync(id, filas);
        if (error != null) return await Fallo(id, "ActividadesAcademicas", error);
        TempData["Ok"] = $"Se guardaron {filas.Count} fila(s) de una sola vez.";
        return RedirectToPage(new { id });
    }
    public async Task<IActionResult> OnPostPasantiasAsync(int id)
    {
        // Una fila sin código ni nombre se considera vacía y se ignora.
        var filas = NuevasPasantias.Where(x => !(x.Id == 0 && string.IsNullOrWhiteSpace(x.Nombre))).ToList();
        if (filas.Count == 0) return await Fallo(id, "Pasantias", "Escriba al menos una fila.");
        var error = await _detalle.CrearPasantiasLoteAsync(id, filas);
        if (error != null) return await Fallo(id, "Pasantias", error);
        TempData["Ok"] = $"Se guardaron {filas.Count} fila(s) de una sola vez.";
        return RedirectToPage(new { id });
    }
    public async Task<IActionResult> OnPostPremiosAsync(int id)
    {
        // Una fila sin código ni nombre se considera vacía y se ignora.
        var filas = NuevasPremios.Where(x => !(x.Id == 0 && string.IsNullOrWhiteSpace(x.Nombre))).ToList();
        if (filas.Count == 0) return await Fallo(id, "Premios", "Escriba al menos una fila.");
        var error = await _detalle.CrearPremiosLoteAsync(id, filas);
        if (error != null) return await Fallo(id, "Premios", error);
        TempData["Ok"] = $"Se guardaron {filas.Count} fila(s) de una sola vez.";
        return RedirectToPage(new { id });
    }
}
