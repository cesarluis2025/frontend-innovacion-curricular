using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

public class PracticaEstrategiaCliente
{
    private readonly HttpClient _http;
    public PracticaEstrategiaCliente(HttpClient http) => _http = http;

    public async Task<List<PracticaEstrategia>> ListarAsync()
        => await _http.GetFromJsonAsync<List<PracticaEstrategia>>("api/practica_estrategia") ?? new();

    public async Task<PracticaEstrategia?> ObtenerPorIdAsync(int id)
    {
        var r = await _http.GetAsync($"api/practica_estrategia/{id}");
        if (!r.IsSuccessStatusCode) return null;
        return await r.Content.ReadFromJsonAsync<PracticaEstrategia>();
    }

    public async Task<string?> CrearAsync(PracticaEstrategia item)
    {
        var r = await _http.PostAsJsonAsync("api/practica_estrategia", item);
        if (r.IsSuccessStatusCode) return null;
        var e = await r.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        return e != null && e.TryGetValue("mensaje", out var m) ? m.ToString() : "no se pudo crear el registro";
    }

    public async Task<string?> ActualizarAsync(int id, PracticaEstrategia item)
    {
        var r = await _http.PutAsJsonAsync($"api/practica_estrategia/{id}", item);
        if (r.IsSuccessStatusCode) return null;
        var e = await r.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        return e != null && e.TryGetValue("mensaje", out var m) ? m.ToString() : "no se pudo actualizar el registro";
    }

    public async Task EliminarAsync(int id) => await _http.DeleteAsync($"api/practica_estrategia/{id}");
}
