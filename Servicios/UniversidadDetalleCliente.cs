using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

// Maestro-detalle de Universidad: lista lo que depende del registro y guarda varias filas de una vez.
public class UniversidadDetalleCliente
{
    private readonly HttpClient _http;
    public UniversidadDetalleCliente(HttpClient http) => _http = http;

    public async Task<List<Facultad>> ListarFacultadesAsync(int id)
        => await _http.GetFromJsonAsync<List<Facultad>>($"api/universidad/{id}/facultades") ?? new();

    // Todas las filas viajan en UNA petición: la API las guarda todas o ninguna.
    public async Task<string?> CrearFacultadesLoteAsync(int id, List<Facultad> filas)
    {
        var r = await _http.PostAsJsonAsync($"api/universidad/{id}/facultades/lote", new { renglones = filas });
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudieron guardar las filas");
    }
}
