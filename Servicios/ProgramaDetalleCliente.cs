using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

// Maestro-detalle de Programa: lista lo que depende del registro y guarda varias filas de una vez.
public class ProgramaDetalleCliente
{
    private readonly HttpClient _http;
    public ProgramaDetalleCliente(HttpClient http) => _http = http;

    public async Task<List<Acreditacion>> ListarAcreditacionesAsync(int id)
        => await _http.GetFromJsonAsync<List<Acreditacion>>($"api/programa/{id}/acreditaciones") ?? new();

    // Todas las filas viajan en UNA petición: la API las guarda todas o ninguna.
    public async Task<string?> CrearAcreditacionesLoteAsync(int id, List<Acreditacion> filas)
    {
        var r = await _http.PostAsJsonAsync($"api/programa/{id}/acreditaciones/lote", new { renglones = filas });
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudieron guardar las filas");
    }

    public async Task<List<RegistroCalificado>> ListarRegistrosCalificadosAsync(int id)
        => await _http.GetFromJsonAsync<List<RegistroCalificado>>($"api/programa/{id}/registros_calificados") ?? new();

    // Todas las filas viajan en UNA petición: la API las guarda todas o ninguna.
    public async Task<string?> CrearRegistrosCalificadosLoteAsync(int id, List<RegistroCalificado> filas)
    {
        var r = await _http.PostAsJsonAsync($"api/programa/{id}/registros_calificados/lote", new { renglones = filas });
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudieron guardar las filas");
    }

    public async Task<List<ActivAcademica>> ListarActividadesAcademicasAsync(int id)
        => await _http.GetFromJsonAsync<List<ActivAcademica>>($"api/programa/{id}/actividades_academicas") ?? new();

    // Todas las filas viajan en UNA petición: la API las guarda todas o ninguna.
    public async Task<string?> CrearActividadesAcademicasLoteAsync(int id, List<ActivAcademica> filas)
    {
        var r = await _http.PostAsJsonAsync($"api/programa/{id}/actividades_academicas/lote", new { renglones = filas });
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudieron guardar las filas");
    }

    public async Task<List<Pasantia>> ListarPasantiasAsync(int id)
        => await _http.GetFromJsonAsync<List<Pasantia>>($"api/programa/{id}/pasantias") ?? new();

    // Todas las filas viajan en UNA petición: la API las guarda todas o ninguna.
    public async Task<string?> CrearPasantiasLoteAsync(int id, List<Pasantia> filas)
    {
        var r = await _http.PostAsJsonAsync($"api/programa/{id}/pasantias/lote", new { renglones = filas });
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudieron guardar las filas");
    }

    public async Task<List<Premio>> ListarPremiosAsync(int id)
        => await _http.GetFromJsonAsync<List<Premio>>($"api/programa/{id}/premios") ?? new();

    // Todas las filas viajan en UNA petición: la API las guarda todas o ninguna.
    public async Task<string?> CrearPremiosLoteAsync(int id, List<Premio> filas)
    {
        var r = await _http.PostAsJsonAsync($"api/programa/{id}/premios/lote", new { renglones = filas });
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudieron guardar las filas");
    }
}
