namespace FrontendInnovacionCurricular.Modelos;

// Lo que la API devuelve y recibe de asignación. Las propiedades con «Nombre» al final
// llegan solo en las lecturas: son el texto del registro relacionado, para mostrarlo
// en pantalla en lugar de un número.
public class EnfoqueRc
{
    public int Enfoque { get; set; }
    public int RegistroCalificado { get; set; }
    public string? EnfoqueNombre { get; set; }
    public string? RegistroCalificadoNombre { get; set; }
    public bool Activo { get; set; }
}
