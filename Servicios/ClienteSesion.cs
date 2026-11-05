using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

// Habla con POST /api/login. Es el ÚNICO cliente que NO lleva TokenHandler: se usa
// justo antes de tener una credencial, y un 401 aquí significa «usuario o contraseña
// incorrectos», no «sesión vencida».
public class ClienteSesion
{
    private readonly HttpClient _http;
    public ClienteSesion(HttpClient http) => _http = http;

    // Devuelve la sesión, o el mensaje de la API (incorrectos, cuenta inactiva, sin rol).
    public async Task<(SesionRespuesta? Sesion, string? Error)> IniciarAsync(string username, string password)
    {
        var r = await _http.PostAsJsonAsync("api/login", new { username, password });
        if (!r.IsSuccessStatusCode)
            return (null, await RespuestaApi.LeerErrorAsync(r, "no se pudo iniciar la sesión"));
        return (await r.Content.ReadFromJsonAsync<SesionRespuesta>(), null);
    }
}
