using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

// Habla con la API por HTTP; el frontend nunca toca la base de datos. Los métodos que
// escriben devuelven null si todo salió bien o el mensaje de la API si algo falló.
public class PasantiaCliente
{
    private readonly HttpClient _http;
    public PasantiaCliente(HttpClient http) => _http = http;

    public async Task<List<Pasantia>> ListarAsync()
        => await _http.GetFromJsonAsync<List<Pasantia>>("api/pasantia") ?? new();

    public async Task<Pasantia?> ObtenerPorIdAsync(int id)
    {
        var r = await _http.GetAsync($"api/pasantia/{id}");
        if (!r.IsSuccessStatusCode) return null;
        return await r.Content.ReadFromJsonAsync<Pasantia>();
    }

    public async Task<string?> CrearAsync(Pasantia item)
    {
        var r = await _http.PostAsJsonAsync("api/pasantia", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo crear el registro");
    }

    public async Task<string?> ActualizarAsync(int id, Pasantia item)
    {
        var r = await _http.PutAsJsonAsync($"api/pasantia/{id}", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo actualizar el registro");
    }

    public async Task<string?> EliminarAsync(int id)
    {
        var r = await _http.DeleteAsync($"api/pasantia/{id}");
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo retirar el registro");
    }
}
