namespace FrontendInnovacionCurricular.Modelos;

// Lo que la API devuelve y recibe de actividad académica. Las propiedades con «Nombre» al final
// llegan solo en las lecturas: son el texto del registro relacionado, para mostrarlo
// en pantalla en lugar de un número.
public class ActivAcademica
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int NumCreditos { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string AreaFormacion { get; set; } = string.Empty;
    public int HAcom { get; set; }
    public int HIndep { get; set; }
    public string Idioma { get; set; } = string.Empty;
    public int Espejo { get; set; }
    public string? EntidadEspejo { get; set; }
    public string? PaisEspejo { get; set; }
    public int? Disenio { get; set; }
    public string? DisenioNombre { get; set; }
    public bool Activo { get; set; }
}
