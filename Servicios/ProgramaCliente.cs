using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

// Habla con la API por HTTP; el frontend nunca toca la base de datos. Los métodos que
// escriben devuelven null si todo salió bien o el mensaje de la API si algo falló.
public class ProgramaCliente
{
    private readonly HttpClient _http;
    public ProgramaCliente(HttpClient http) => _http = http;

    public async Task<List<Programa>> ListarAsync()
        => await _http.GetFromJsonAsync<List<Programa>>("api/programa") ?? new();

    public async Task<Programa?> ObtenerPorIdAsync(int id)
    {
        var r = await _http.GetAsync($"api/programa/{id}");
        if (!r.IsSuccessStatusCode) return null;
        return await r.Content.ReadFromJsonAsync<Programa>();
    }

    public async Task<string?> CrearAsync(Programa item)
    {
        var r = await _http.PostAsJsonAsync("api/programa", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo crear el registro");
    }

    public async Task<string?> ActualizarAsync(int id, Programa item)
    {
        var r = await _http.PutAsJsonAsync($"api/programa/{id}", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo actualizar el registro");
    }

    public async Task<string?> EliminarAsync(int id)
    {
        var r = await _http.DeleteAsync($"api/programa/{id}");
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo retirar el registro");
    }
}
