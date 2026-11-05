using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Autorizacion;
using FrontendInnovacionCurricular.Servicios;
using FrontendInnovacionCurricular.Modelos;
using ModeloRol = FrontendInnovacionCurricular.Modelos.Rol;

namespace FrontendInnovacionCurricular.Pages.Usuario;

[PermisoPagina("acceso", "escribir")]
public class CrearModel : PageModel
{
    private readonly ClienteUsuario _cliente;
    private readonly ClienteRol _roles;
    public CrearModel(ClienteUsuario cliente, ClienteRol roles)
    {
        _cliente = cliente;
        _roles = roles;
    }

    [BindProperty]
    public UsuarioNuevo Registro { get; set; } = new();

    public List<ModeloRol> ListaRoles { get; set; } = new();
    public string? MensajeError { get; set; }

    public async Task OnGetAsync() => ListaRoles = await _roles.ListarAsync();

    public async Task<IActionResult> OnPostAsync()
    {
        // Una persona sin rol no podría entrar a nada: se pide al menos uno antes de enviar.
        if (Registro.Roles.Count == 0)
            MensajeError = "Marque al menos un rol.";
        else
            MensajeError = await _cliente.CrearAsync(Registro);

        if (MensajeError is null)
        {
            TempData["Ok"] = "Persona guardada.";
            return RedirectToPage("Index");
        }
        // Por seguridad la contraseña nunca se vuelve a pintar en el formulario.
        Registro.Password = string.Empty;
        ListaRoles = await _roles.ListarAsync();
        return Page();
    }
}
