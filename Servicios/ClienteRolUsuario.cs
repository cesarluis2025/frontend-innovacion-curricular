using System.Net.Http.Json;

namespace FrontendInnovacionCurricular.Servicios;

// Asigna y retira roles de una persona (puente rol_usuario): se asigna y se retira con
// las dos llaves; no hay actualizar.
public class ClienteRolUsuario
{
    private readonly HttpClient _http;
    public ClienteRolUsuario(HttpClient http) => _http = http;

    public async Task<string?> AsignarAsync(int usuarioId, int rolId)
    {
        var r = await _http.PostAsJsonAsync("api/rol_usuario", new { usuarioId, rolId });
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo asignar el rol");
    }

    public async Task<string?> RetirarAsync(int usuarioId, int rolId)
    {
        var r = await _http.DeleteAsync($"api/rol_usuario/{usuarioId}/{rolId}");
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo retirar el rol");
    }
}
