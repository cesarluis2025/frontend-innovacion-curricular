using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

public class ClienteUsuario
{
    private readonly HttpClient _http;
    public ClienteUsuario(HttpClient http) => _http = http;

    public async Task<List<Usuario>> ListarAsync()
        => await _http.GetFromJsonAsync<List<Usuario>>("api/usuario") ?? new();

    public async Task<Usuario?> ObtenerPorIdAsync(int id)
    {
        var r = await _http.GetAsync($"api/usuario/{id}");
        return r.IsSuccessStatusCode ? await r.Content.ReadFromJsonAsync<Usuario>() : null;
    }

    public async Task<List<RolDeUsuario>> ListarRolesAsync(int id)
    {
        var r = await _http.GetAsync($"api/usuario/{id}/roles");
        return r.IsSuccessStatusCode ? await r.Content.ReadFromJsonAsync<List<RolDeUsuario>>() ?? new() : new();
    }

    public async Task<string?> CrearAsync(UsuarioNuevo item)
    {
        var r = await _http.PostAsJsonAsync("api/usuario", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo crear la persona");
    }

    public async Task<string?> ActualizarAsync(int id, UsuarioCambio item)
    {
        var r = await _http.PutAsJsonAsync($"api/usuario/{id}", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo actualizar la persona");
    }

    public async Task<string?> EliminarAsync(int id)
    {
        var r = await _http.DeleteAsync($"api/usuario/{id}");
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo retirar la persona");
    }
}
