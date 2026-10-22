using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

// Tabla puente: el registro se identifica por la pareja (a, b). Asignar es crear;
// retirar quita la pareja de la lista. Los métodos que escriben devuelven null si todo
// salió bien o el mensaje de la API si algo falló.
public class AaRcCliente
{
    private readonly HttpClient _http;
    public AaRcCliente(HttpClient http) => _http = http;

    public async Task<List<AaRc>> ListarAsync()
        => await _http.GetFromJsonAsync<List<AaRc>>("api/aa_rc") ?? new();

    public async Task<string?> AsignarAsync(AaRc item)
    {
        var r = await _http.PostAsJsonAsync("api/aa_rc", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo guardar la asignación");
    }

    public async Task<AaRc?> ObtenerAsync(int a, int b)
    {
        var r = await _http.GetAsync($"api/aa_rc/{a}/{b}");
        if (!r.IsSuccessStatusCode) return null;
        return await r.Content.ReadFromJsonAsync<AaRc>();
    }

    public async Task<string?> ActualizarAsync(int a, int b, AaRc item)
    {
        var r = await _http.PutAsJsonAsync($"api/aa_rc/{a}/{b}", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo actualizar la asignación");
    }

    public async Task<string?> RetirarAsync(int a, int b)
    {
        var r = await _http.DeleteAsync($"api/aa_rc/{a}/{b}");
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo retirar la asignación");
    }
}
