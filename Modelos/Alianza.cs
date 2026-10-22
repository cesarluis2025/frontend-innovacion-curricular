namespace FrontendInnovacionCurricular.Modelos;

// Lo que la API devuelve y recibe de alianza. Las propiedades con «Nombre» al final
// llegan solo en las lecturas: son el texto del registro relacionado, para mostrarlo
// en pantalla en lugar de un número.
public class Alianza
{
    public int Aliado { get; set; }
    public int Departamento { get; set; }
    public DateTime FechaInicio { get; set; } = DateTime.Today;
    public DateTime? FechaFin { get; set; }
    public int? Docente { get; set; }
    public string? AliadoNombre { get; set; }
    public string? DepartamentoNombre { get; set; }
    public bool Activo { get; set; }
}
