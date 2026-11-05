using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

public class ClienteRol
{
    private readonly HttpClient _http;
    public ClienteRol(HttpClient http) => _http = http;

    public async Task<List<Rol>> ListarAsync()
        => await _http.GetFromJsonAsync<List<Rol>>("api/rol") ?? new();

    public async Task<Rol?> ObtenerPorIdAsync(int id)
    {
        var r = await _http.GetAsync($"api/rol/{id}");
        return r.IsSuccessStatusCode ? await r.Content.ReadFromJsonAsync<Rol>() : null;
    }

    public async Task<string?> CrearAsync(RolCambio item)
    {
        var r = await _http.PostAsJsonAsync("api/rol", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo crear el rol");
    }

    public async Task<string?> ActualizarAsync(int id, RolCambio item)
    {
        var r = await _http.PutAsJsonAsync($"api/rol/{id}", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo actualizar el rol");
    }

    public async Task<string?> EliminarAsync(int id)
    {
        var r = await _http.DeleteAsync($"api/rol/{id}");
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo retirar el rol");
    }
}
