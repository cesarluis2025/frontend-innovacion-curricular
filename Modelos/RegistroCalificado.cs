namespace FrontendInnovacionCurricular.Modelos;

// Lo que la API devuelve y recibe de registro calificado. Las propiedades con «Nombre» al final
// llegan solo en las lecturas: son el texto del registro relacionado, para mostrarlo
// en pantalla en lugar de un número.
public class RegistroCalificado
{
    public int Codigo { get; set; }
    public string CantCreditos { get; set; } = string.Empty;
    public string HoraAcom { get; set; } = string.Empty;
    public string HoraInd { get; set; } = string.Empty;
    public string Metodologia { get; set; } = string.Empty;
    public DateTime FechaInicio { get; set; } = DateTime.Today;
    public DateTime FechaFin { get; set; } = DateTime.Today;
    public string DuracionAnios { get; set; } = string.Empty;
    public string DuracionSemestres { get; set; } = string.Empty;
    public string TipoTitulacion { get; set; } = string.Empty;
    public int Programa { get; set; }
    public string? ProgramaNombre { get; set; }
    public bool Activo { get; set; }
}
