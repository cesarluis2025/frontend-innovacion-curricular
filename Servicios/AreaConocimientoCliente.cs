using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

public class AreaConocimientoCliente
{
    private readonly HttpClient _http;
    public AreaConocimientoCliente(HttpClient http) => _http = http;

    public async Task<List<AreaConocimiento>> ListarAsync()
        => await _http.GetFromJsonAsync<List<AreaConocimiento>>("api/area_conocimiento") ?? new();

    public async Task<AreaConocimiento?> ObtenerPorIdAsync(int id)
    {
        var r = await _http.GetAsync($"api/area_conocimiento/{id}");
        if (!r.IsSuccessStatusCode) return null;
        return await r.Content.ReadFromJsonAsync<AreaConocimiento>();
    }

    public async Task<string?> CrearAsync(AreaConocimiento item)
    {
        var r = await _http.PostAsJsonAsync("api/area_conocimiento", item);
        if (r.IsSuccessStatusCode) return null;
        var e = await r.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        return e != null && e.TryGetValue("mensaje", out var m) ? m.ToString() : "no se pudo crear el registro";
    }

    public async Task<string?> ActualizarAsync(int id, AreaConocimiento item)
    {
        var r = await _http.PutAsJsonAsync($"api/area_conocimiento/{id}", item);
        if (r.IsSuccessStatusCode) return null;
        var e = await r.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        return e != null && e.TryGetValue("mensaje", out var m) ? m.ToString() : "no se pudo actualizar el registro";
    }

    public async Task EliminarAsync(int id) => await _http.DeleteAsync($"api/area_conocimiento/{id}");
}
