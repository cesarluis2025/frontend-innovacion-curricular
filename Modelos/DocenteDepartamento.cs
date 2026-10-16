namespace FrontendInnovacionCurricular.Modelos;

// Lo que la API devuelve y recibe de asignación. Las propiedades con «Nombre» al final
// llegan solo en las lecturas: son el texto del registro relacionado, para mostrarlo
// en pantalla en lugar de un número.
public class DocenteDepartamento
{
    public int Docente { get; set; }
    public int Departamento { get; set; }
    public string Dedicacion { get; set; } = string.Empty;
    public string Modalidad { get; set; } = string.Empty;
    public DateTime FechaIngreso { get; set; } = DateTime.Today;
    public DateTime? FechaSalida { get; set; }
    public string? DepartamentoNombre { get; set; }
    public bool Activo { get; set; }
}
