namespace FrontendInnovacionCurricular.Modelos;

// Lo que la API devuelve y recibe de asignación. Las propiedades con «Nombre» al final
// llegan solo en las lecturas: son el texto del registro relacionado, para mostrarlo
// en pantalla en lugar de un número.
public class AaRc
{
    public int ActivAcademicasIdcurso { get; set; }
    public int RegistroCalificadoCodigo { get; set; }
    public string Componente { get; set; } = string.Empty;
    public string Semestre { get; set; } = string.Empty;
    public string? ActivAcademicaNombre { get; set; }
    public string? RegistroCalificadoNombre { get; set; }
    public bool Activo { get; set; }
}
