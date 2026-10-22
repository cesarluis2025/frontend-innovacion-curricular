namespace FrontendInnovacionCurricular.Modelos;

// Mismo formato que la API: el frontend nunca toca la base de datos,
// solo llena esta clase con lo que le responde la API en JSON.
public class AreaConocimiento
{
    public int Id { get; set; }
    public string GranArea { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string Disciplina { get; set; } = string.Empty;
    public bool Activo { get; set; }
}
