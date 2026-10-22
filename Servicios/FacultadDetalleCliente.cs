using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

// Maestro-detalle de Facultad: lista lo que depende del registro y guarda varias filas de una vez.
public class FacultadDetalleCliente
{
    private readonly HttpClient _http;
    public FacultadDetalleCliente(HttpClient http) => _http = http;

    public async Task<List<Programa>> ListarProgramasAsync(int id)
        => await _http.GetFromJsonAsync<List<Programa>>($"api/facultad/{id}/programas") ?? new();

    // Todas las filas viajan en UNA petición: la API las guarda todas o ninguna.
    public async Task<string?> CrearProgramasLoteAsync(int id, List<Programa> filas)
    {
        var r = await _http.PostAsJsonAsync($"api/facultad/{id}/programas/lote", new { renglones = filas });
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudieron guardar las filas");
    }
}
