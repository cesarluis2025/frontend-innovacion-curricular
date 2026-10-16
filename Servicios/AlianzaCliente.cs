using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

// Tabla puente: el registro se identifica por la pareja (a, b). Asignar es crear;
// retirar quita la pareja de la lista. Los métodos que escriben devuelven null si todo
// salió bien o el mensaje de la API si algo falló.
public class AlianzaCliente
{
    private readonly HttpClient _http;
    public AlianzaCliente(HttpClient http) => _http = http;

    public async Task<List<Alianza>> ListarAsync()
        => await _http.GetFromJsonAsync<List<Alianza>>("api/alianza") ?? new();

    public async Task<string?> AsignarAsync(Alianza item)
    {
        var r = await _http.PostAsJsonAsync("api/alianza", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo guardar la asignación");
    }

    public async Task<Alianza?> ObtenerAsync(int a, int b)
    {
        var r = await _http.GetAsync($"api/alianza/{a}/{b}");
        if (!r.IsSuccessStatusCode) return null;
        return await r.Content.ReadFromJsonAsync<Alianza>();
    }

    public async Task<string?> ActualizarAsync(int a, int b, Alianza item)
    {
        var r = await _http.PutAsJsonAsync($"api/alianza/{a}/{b}", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo actualizar la asignación");
    }

    public async Task<string?> RetirarAsync(int a, int b)
    {
        var r = await _http.DeleteAsync($"api/alianza/{a}/{b}");
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo retirar la asignación");
    }
}
