using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

// Habla con la API por HTTP; el frontend nunca toca la base de datos. Los métodos que
// escriben devuelven null si todo salió bien o el mensaje de la API si algo falló.
public class AcreditacionCliente
{
    private readonly HttpClient _http;
    public AcreditacionCliente(HttpClient http) => _http = http;

    public async Task<List<Acreditacion>> ListarAsync()
        => await _http.GetFromJsonAsync<List<Acreditacion>>("api/acreditacion") ?? new();

    public async Task<Acreditacion?> ObtenerPorIdAsync(int id)
    {
        var r = await _http.GetAsync($"api/acreditacion/{id}");
        if (!r.IsSuccessStatusCode) return null;
        return await r.Content.ReadFromJsonAsync<Acreditacion>();
    }

    public async Task<string?> CrearAsync(Acreditacion item)
    {
        var r = await _http.PostAsJsonAsync("api/acreditacion", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo crear el registro");
    }

    public async Task<string?> ActualizarAsync(int id, Acreditacion item)
    {
        var r = await _http.PutAsJsonAsync($"api/acreditacion/{id}", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo actualizar el registro");
    }

    public async Task<string?> EliminarAsync(int id)
    {
        var r = await _http.DeleteAsync($"api/acreditacion/{id}");
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo retirar el registro");
    }
}
