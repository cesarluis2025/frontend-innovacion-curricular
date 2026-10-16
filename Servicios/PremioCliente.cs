using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

// Habla con la API por HTTP; el frontend nunca toca la base de datos. Los métodos que
// escriben devuelven null si todo salió bien o el mensaje de la API si algo falló.
public class PremioCliente
{
    private readonly HttpClient _http;
    public PremioCliente(HttpClient http) => _http = http;

    public async Task<List<Premio>> ListarAsync()
        => await _http.GetFromJsonAsync<List<Premio>>("api/premio") ?? new();

    public async Task<Premio?> ObtenerPorIdAsync(int id)
    {
        var r = await _http.GetAsync($"api/premio/{id}");
        if (!r.IsSuccessStatusCode) return null;
        return await r.Content.ReadFromJsonAsync<Premio>();
    }

    public async Task<string?> CrearAsync(Premio item)
    {
        var r = await _http.PostAsJsonAsync("api/premio", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo crear el registro");
    }

    public async Task<string?> ActualizarAsync(int id, Premio item)
    {
        var r = await _http.PutAsJsonAsync($"api/premio/{id}", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo actualizar el registro");
    }

    public async Task<string?> EliminarAsync(int id)
    {
        var r = await _http.DeleteAsync($"api/premio/{id}");
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo retirar el registro");
    }
}
