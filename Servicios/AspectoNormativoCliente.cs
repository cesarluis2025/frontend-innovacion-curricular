using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

public class AspectoNormativoCliente
{
    private readonly HttpClient _http;
    public AspectoNormativoCliente(HttpClient http) => _http = http;

    public async Task<List<AspectoNormativo>> ListarAsync()
        => await _http.GetFromJsonAsync<List<AspectoNormativo>>("api/aspecto_normativo") ?? new();

    public async Task<AspectoNormativo?> ObtenerPorIdAsync(int id)
    {
        var r = await _http.GetAsync($"api/aspecto_normativo/{id}");
        if (!r.IsSuccessStatusCode) return null;
        return await r.Content.ReadFromJsonAsync<AspectoNormativo>();
    }

    public async Task<string?> CrearAsync(AspectoNormativo item)
    {
        var r = await _http.PostAsJsonAsync("api/aspecto_normativo", item);
        if (r.IsSuccessStatusCode) return null;
        var e = await r.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        return e != null && e.TryGetValue("mensaje", out var m) ? m.ToString() : "no se pudo crear el registro";
    }

    public async Task<string?> ActualizarAsync(int id, AspectoNormativo item)
    {
        var r = await _http.PutAsJsonAsync($"api/aspecto_normativo/{id}", item);
        if (r.IsSuccessStatusCode) return null;
        var e = await r.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        return e != null && e.TryGetValue("mensaje", out var m) ? m.ToString() : "no se pudo actualizar el registro";
    }

    public async Task EliminarAsync(int id) => await _http.DeleteAsync($"api/aspecto_normativo/{id}");
}
