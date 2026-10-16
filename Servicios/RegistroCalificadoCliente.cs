using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

// Habla con la API por HTTP; el frontend nunca toca la base de datos. Los métodos que
// escriben devuelven null si todo salió bien o el mensaje de la API si algo falló.
public class RegistroCalificadoCliente
{
    private readonly HttpClient _http;
    public RegistroCalificadoCliente(HttpClient http) => _http = http;

    public async Task<List<RegistroCalificado>> ListarAsync()
        => await _http.GetFromJsonAsync<List<RegistroCalificado>>("api/registro_calificado") ?? new();

    public async Task<RegistroCalificado?> ObtenerPorIdAsync(int id)
    {
        var r = await _http.GetAsync($"api/registro_calificado/{id}");
        if (!r.IsSuccessStatusCode) return null;
        return await r.Content.ReadFromJsonAsync<RegistroCalificado>();
    }

    public async Task<string?> CrearAsync(RegistroCalificado item)
    {
        var r = await _http.PostAsJsonAsync("api/registro_calificado", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo crear el registro");
    }

    public async Task<string?> ActualizarAsync(int id, RegistroCalificado item)
    {
        var r = await _http.PutAsJsonAsync($"api/registro_calificado/{id}", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo actualizar el registro");
    }

    public async Task<string?> EliminarAsync(int id)
    {
        var r = await _http.DeleteAsync($"api/registro_calificado/{id}");
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo retirar el registro");
    }
}
