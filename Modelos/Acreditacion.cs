namespace FrontendInnovacionCurricular.Modelos;

// Lo que la API devuelve y recibe de acreditación. Las propiedades con «Nombre» al final
// llegan solo en las lecturas: son el texto del registro relacionado, para mostrarlo
// en pantalla en lugar de un número.
public class Acreditacion
{
    public int Resolucion { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Calificacion { get; set; } = string.Empty;
    public string FechaInicio { get; set; } = string.Empty;
    public string FechaFin { get; set; } = string.Empty;
    public int Programa { get; set; }
    public string? ProgramaNombre { get; set; }
    public bool Activo { get; set; }
}
