using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

// Habla con la API por HTTP; el frontend nunca toca la base de datos. Los métodos que
// escriben devuelven null si todo salió bien o el mensaje de la API si algo falló.
public class FacultadCliente
{
    private readonly HttpClient _http;
    public FacultadCliente(HttpClient http) => _http = http;

    public async Task<List<Facultad>> ListarAsync()
        => await _http.GetFromJsonAsync<List<Facultad>>("api/facultad") ?? new();

    public async Task<Facultad?> ObtenerPorIdAsync(int id)
    {
        var r = await _http.GetAsync($"api/facultad/{id}");
        if (!r.IsSuccessStatusCode) return null;
        return await r.Content.ReadFromJsonAsync<Facultad>();
    }

    public async Task<string?> CrearAsync(Facultad item)
    {
        var r = await _http.PostAsJsonAsync("api/facultad", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo crear el registro");
    }

    public async Task<string?> ActualizarAsync(int id, Facultad item)
    {
        var r = await _http.PutAsJsonAsync($"api/facultad/{id}", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo actualizar el registro");
    }

    public async Task<string?> EliminarAsync(int id)
    {
        var r = await _http.DeleteAsync($"api/facultad/{id}");
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo retirar el registro");
    }
}
