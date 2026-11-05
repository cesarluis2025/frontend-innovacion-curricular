namespace FrontendInnovacionCurricular.Servicios;

// La API respondió que la persona ya no está identificada (la sesión venció o la
// cuenta dejó de estar activa). La lanza TokenHandler y la atrapa el middleware de
// Program.cs, que cierra la sesión y lleva a la pantalla de entrada con un aviso.
public class SesionVencidaExcepcion : Exception
{
    public SesionVencidaExcepcion() : base("la sesión venció") { }
}
