using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

// Los permisos de la persona de ESTA petición. Se registra por petición (Scoped): se
// pregunta a la API UNA vez por carga de página, y el filtro de páginas, el menú y los
// botones leen la misma respuesta. Nunca se guardan en la cookie: así, si a alguien le
// quitan un rol, la siguiente pantalla que abra ya lo refleja, sin volver a entrar.
//
// OJO: esto solo decide qué se MUESTRA. Esconder una opción no protege nada: quien
// escriba la dirección a mano, o llame a la API directamente, se topa con la negativa
// de la API. Esa es la protección.
public class PermisosActuales
{
    private readonly ClientePermisos _cliente;
    private MisPermisos? _datos;
    private bool _cargado;

    public PermisosActuales(ClientePermisos cliente) => _cliente = cliente;

    public async Task<MisPermisos?> ObtenerAsync()
    {
        if (!_cargado)
        {
            _datos = await _cliente.MiosAsync();
            _cargado = true;
        }
        return _datos;
    }

    // Lo ya consultado en esta petición (null en las pantallas sin sesión, como la de entrada).
    public MisPermisos? Actual => _datos;

    public bool Puede(string grupo, string operacion)
        => _datos is not null && _datos.Permisos.TryGetValue(grupo, out var ops) && ops.Contains(operacion);

    public bool PuedeLeerModulo => Puede("modulo", "leer");
    public bool PuedeEscribirModulo => Puede("modulo", "escribir");
    public bool PuedeLeerAcceso => Puede("acceso", "leer");
}
