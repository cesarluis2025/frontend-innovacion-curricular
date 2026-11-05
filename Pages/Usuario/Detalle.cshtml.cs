using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FrontendInnovacionCurricular.Autorizacion;
using FrontendInnovacionCurricular.Modelos;
using FrontendInnovacionCurricular.Servicios;
using ModeloUsuario = FrontendInnovacionCurricular.Modelos.Usuario;

namespace FrontendInnovacionCurricular.Pages.Usuario;

// Detalle de una persona: sus roles, con asignar y retirar. Los roles NO se cambian
// desde «Editar»: se asignan y retiran aquí, uno por uno.
[PermisoPagina("acceso", "leer")]
public class DetalleModel : PageModel
{
    private readonly ClienteUsuario _cliente;
    private readonly ClienteRol _roles;
    private readonly ClienteRolUsuario _asignaciones;

    public DetalleModel(ClienteUsuario cliente, ClienteRol roles, ClienteRolUsuario asignaciones)
    {
        _cliente = cliente;
        _roles = roles;
        _asignaciones = asignaciones;
    }

    public ModeloUsuario Maestro { get; set; } = new();
    public List<RolDeUsuario> RolesActuales { get; set; } = new();
    public List<SelectListItem> RolesDisponibles { get; set; } = new();
    public string? MensajeError { get; set; }

    [BindProperty]
    public int RolAsignar { get; set; }

    private async Task<bool> CargarAsync(int id)
    {
        var actual = await _cliente.ObtenerPorIdAsync(id);
        if (actual is null) return false;
        Maestro = actual;
        RolesActuales = await _cliente.ListarRolesAsync(id);
        var tiene = RolesActuales.Select(r => r.RolId).ToHashSet();
        RolesDisponibles = (await _roles.ListarAsync())
            .Where(r => !tiene.Contains(r.Id))
            .Select(r => new SelectListItem(r.Nombre, r.Id.ToString())).ToList();
        return true;
    }

    public async Task<IActionResult> OnGetAsync(int id)
        => await CargarAsync(id) ? Page() : NotFound();

    public async Task<IActionResult> OnPostAsignarAsync(int id)
    {
        MensajeError = await _asignaciones.AsignarAsync(id, RolAsignar);
        if (MensajeError is null) { TempData["Ok"] = "Rol asignado."; return RedirectToPage(new { id }); }
        await CargarAsync(id);
        return Page();
    }

    // Si es el último administrador, la API se niega y el motivo se muestra aquí.
    public async Task<IActionResult> OnPostRetirarAsync(int id, int rolId)
    {
        MensajeError = await _asignaciones.RetirarAsync(id, rolId);
        if (MensajeError is null) { TempData["Ok"] = "Rol retirado."; return RedirectToPage(new { id }); }
        await CargarAsync(id);
        return Page();
    }
}
