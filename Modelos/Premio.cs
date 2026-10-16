namespace FrontendInnovacionCurricular.Modelos;

// Lo que la API devuelve y recibe de premio. Las propiedades con «Nombre» al final
// llegan solo en las lecturas: son el texto del registro relacionado, para mostrarlo
// en pantalla en lugar de un número.
public class Premio
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime Fecha { get; set; } = DateTime.Today;
    public string EntidadOtorgante { get; set; } = string.Empty;
    public string Pais { get; set; } = string.Empty;
    public int Programa { get; set; }
    public string? ProgramaNombre { get; set; }
    public bool Activo { get; set; }
}
