using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

public class UniversidadCliente
{
    private readonly HttpClient _http;
    public UniversidadCliente(HttpClient http) => _http = http;

    public async Task<List<Universidad>> ListarAsync()
        => await _http.GetFromJsonAsync<List<Universidad>>("api/universidad") ?? new();

    public async Task<Universidad?> ObtenerPorIdAsync(int id)
    {
        var r = await _http.GetAsync($"api/universidad/{id}");
        if (!r.IsSuccessStatusCode) return null;
        return await r.Content.ReadFromJsonAsync<Universidad>();
    }

    public async Task<string?> CrearAsync(Universidad item)
    {
        var r = await _http.PostAsJsonAsync("api/universidad", item);
        if (r.IsSuccessStatusCode) return null;
        var e = await r.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        return e != null && e.TryGetValue("mensaje", out var m) ? m.ToString() : "no se pudo crear el registro";
    }

    public async Task<string?> ActualizarAsync(int id, Universidad item)
    {
        var r = await _http.PutAsJsonAsync($"api/universidad/{id}", item);
        if (r.IsSuccessStatusCode) return null;
        var e = await r.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        return e != null && e.TryGetValue("mensaje", out var m) ? m.ToString() : "no se pudo actualizar el registro";
    }

    public async Task EliminarAsync(int id) => await _http.DeleteAsync($"api/universidad/{id}");
}
