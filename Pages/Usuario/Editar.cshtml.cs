using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Autorizacion;
using FrontendInnovacionCurricular.Servicios;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Pages.Usuario;

[PermisoPagina("acceso", "escribir")]
public class EditarModel : PageModel
{
    private readonly ClienteUsuario _cliente;
    public EditarModel(ClienteUsuario cliente) => _cliente = cliente;

    [BindProperty]
    public UsuarioCambio Registro { get; set; } = new();

    public string? MensajeError { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var actual = await _cliente.ObtenerPorIdAsync(id);
        if (actual is null) return NotFound();
        // La contraseña NO se carga: la API nunca la devuelve y el formulario la deja vacía.
        Registro = new UsuarioCambio { Username = actual.Username, Email = actual.Email, NombreCompleto = actual.NombreCompleto };
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        // Vacía = no cambia (el contrato de la API); se manda null para dejarlo claro.
        if (string.IsNullOrEmpty(Registro.Password)) Registro.Password = null;
        MensajeError = await _cliente.ActualizarAsync(id, Registro);
        if (MensajeError is null)
        {
            TempData["Ok"] = "Cambios guardados.";
            return RedirectToPage("Index");
        }
        Registro.Password = null;
        return Page();
    }
}
