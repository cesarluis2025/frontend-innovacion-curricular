namespace FrontendInnovacionCurricular.Autorizacion;

// Marca una página con el permiso que exige: el GRUPO («modulo» o «acceso») y la
// operación mínima. Las pantallas de crear y editar piden «escribir» aunque solo se
// ABRAN (si la persona no puede escribir, ni el formulario debe verse); cualquier envío
// del formulario (POST) pide «escribir» siempre.
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class PermisoPaginaAttribute : Attribute
{
    public string Grupo { get; }
    public string Operacion { get; }
    public PermisoPaginaAttribute(string grupo, string operacion = "leer")
    {
        Grupo = grupo;
        Operacion = operacion;
    }
}

// Página que solo pide estar identificado (la portada y la pantalla «sin permiso»).
// Es una marca explícita: una página sin ninguna marca se NIEGA (se falla cerrado).
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class SoloIdentificadoPaginaAttribute : Attribute
{
}
