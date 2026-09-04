using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

public class AliadoCliente
{
    private readonly HttpClient _http;
    public AliadoCliente(HttpClient http) => _http = http;

    public async Task<List<Aliado>> ListarAsync()
        => await _http.GetFromJsonAsync<List<Aliado>>("api/aliado") ?? new();

    public async Task<Aliado?> ObtenerPorNitAsync(int nit)
    {
        var r = await _http.GetAsync($"api/aliado/{nit}");
        if (!r.IsSuccessStatusCode) return null;
        return await r.Content.ReadFromJsonAsync<Aliado>();
    }

    public async Task<string?> CrearAsync(Aliado item)
    {
        var r = await _http.PostAsJsonAsync("api/aliado", item);
        if (r.IsSuccessStatusCode) return null;
        var e = await r.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        return e != null && e.TryGetValue("mensaje", out var m) ? m.ToString() : "no se pudo crear el registro";
    }

    public async Task<string?> ActualizarAsync(int nit, Aliado item)
    {
        var r = await _http.PutAsJsonAsync($"api/aliado/{nit}", item);
        if (r.IsSuccessStatusCode) return null;
        var e = await r.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        return e != null && e.TryGetValue("mensaje", out var m) ? m.ToString() : "no se pudo actualizar el registro";
    }

    public async Task EliminarAsync(int nit) => await _http.DeleteAsync($"api/aliado/{nit}");
}
