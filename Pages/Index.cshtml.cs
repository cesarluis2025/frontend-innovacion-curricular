using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Servicios;

namespace FrontendInnovacionCurricular.Pages;

public class IndexModel : PageModel
{
    private readonly UniversidadCliente _universidades;
    private readonly FacultadCliente _facultades;
    private readonly ProgramaCliente _programas;

    public IndexModel(UniversidadCliente universidades, FacultadCliente facultades, ProgramaCliente programas)
    {
        _universidades = universidades;
        _facultades = facultades;
        _programas = programas;
    }

    // -1 significa que la API no respondió: la portada se muestra igual, con una raya en lugar del número.
    public int Universidades { get; set; } = -1;
    public int Facultades { get; set; } = -1;
    public int Programas { get; set; } = -1;

    public async Task OnGetAsync()
    {
        try
        {
            Universidades = (await _universidades.ListarAsync()).Count;
            Facultades = (await _facultades.ListarAsync()).Count;
            Programas = (await _programas.ListarAsync()).Count;
        }
        catch (HttpRequestException) { }
    }
}
