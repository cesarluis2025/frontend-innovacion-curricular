namespace FrontendInnovacionCurricular.Modelos;

// Lo que la API devuelve y recibe de asignación. Las propiedades con «Nombre» al final
// llegan solo en las lecturas: son el texto del registro relacionado, para mostrarlo
// en pantalla en lugar de un número.
public class ProgramaCi
{
    public int Programa { get; set; }
    public int CarInnovacion { get; set; }
    public string? ProgramaNombre { get; set; }
    public string? CarInnovacionNombre { get; set; }
    public bool Activo { get; set; }
}
