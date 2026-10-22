namespace FrontendInnovacionCurricular.Modelos;

// Lo que la API devuelve y recibe de facultad. Las propiedades con «Nombre» al final
// llegan solo en las lecturas: son el texto del registro relacionado, para mostrarlo
// en pantalla en lugar de un número.
public class Facultad
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public DateTime FechaFun { get; set; } = DateTime.Today;
    public int Universidad { get; set; }
    public string? UniversidadNombre { get; set; }
    public int TotalProgramas { get; set; }
    public bool Activo { get; set; }
}
