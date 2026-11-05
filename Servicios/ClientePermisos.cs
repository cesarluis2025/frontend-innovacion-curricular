using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

public class ClientePermisos
{
    private readonly HttpClient _http;
    public ClientePermisos(HttpClient http) => _http = http;

    // null si la API no pudo responder; la sesión vencida sube como excepción desde TokenHandler.
    public async Task<MisPermisos?> MiosAsync()
    {
        var r = await _http.GetAsync("api/permisos/mios");
        return r.IsSuccessStatusCode ? await r.Content.ReadFromJsonAsync<MisPermisos>() : null;
    }
}
