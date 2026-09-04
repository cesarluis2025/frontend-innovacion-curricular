using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

public class EnfoqueCliente
{
    private readonly HttpClient _http;
    public EnfoqueCliente(HttpClient http) => _http = http;

    public async Task<List<Enfoque>> ListarAsync()
        => await _http.GetFromJsonAsync<List<Enfoque>>("api/enfoque") ?? new();

    public async Task<Enfoque?> ObtenerPorIdAsync(int id)
    {
        var r = await _http.GetAsync($"api/enfoque/{id}");
        if (!r.IsSuccessStatusCode) return null;
        return await r.Content.ReadFromJsonAsync<Enfoque>();
    }

    public async Task<string?> CrearAsync(Enfoque item)
    {
        var r = await _http.PostAsJsonAsync("api/enfoque", item);
        if (r.IsSuccessStatusCode) return null;
        var e = await r.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        return e != null && e.TryGetValue("mensaje", out var m) ? m.ToString() : "no se pudo crear el registro";
    }

    public async Task<string?> ActualizarAsync(int id, Enfoque item)
    {
        var r = await _http.PutAsJsonAsync($"api/enfoque/{id}", item);
        if (r.IsSuccessStatusCode) return null;
        var e = await r.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        return e != null && e.TryGetValue("mensaje", out var m) ? m.ToString() : "no se pudo actualizar el registro";
    }

    public async Task EliminarAsync(int id) => await _http.DeleteAsync($"api/enfoque/{id}");
}
