namespace FrontendInnovacionCurricular.Modelos;

// Lo que la API devuelve y recibe del control de acceso. Ninguna clase tiene
// «Password» salvo las peticiones de crear y cambiar: la API nunca la devuelve.

public class Usuario
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? NombreCompleto { get; set; }
    public bool Activo { get; set; }
}

public class Rol
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public bool Activo { get; set; }
}

public class RolDeUsuario
{
    public int RolId { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

// Lo que se manda a POST /api/usuario.
public class UsuarioNuevo
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? NombreCompleto { get; set; }
    public List<int> Roles { get; set; } = new();
}

// Lo que se manda a PUT /api/usuario/{id}. Password vacío = no cambia.
public class UsuarioCambio
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? NombreCompleto { get; set; }
    public string? Password { get; set; }
}

public class RolCambio
{
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
}

// Respuesta de POST /api/login.
public class SesionRespuesta
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiraEn { get; set; }
    public UsuarioSesion Usuario { get; set; } = new();
}

public class UsuarioSesion
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? NombreCompleto { get; set; }
    public List<string> Roles { get; set; } = new();
}

// Respuesta de GET /api/permisos/mios.
public class MisPermisos
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? NombreCompleto { get; set; }
    public List<string> Roles { get; set; } = new();
    public Dictionary<string, List<string>> Permisos { get; set; } = new();
}
