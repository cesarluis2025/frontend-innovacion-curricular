using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

public class CarInnovacionCliente
{
    private readonly HttpClient _http;
    public CarInnovacionCliente(HttpClient http) => _http = http;

    public async Task<List<CarInnovacion>> ListarAsync()
        => await _http.GetFromJsonAsync<List<CarInnovacion>>("api/car_innovacion") ?? new();

    public async Task<CarInnovacion?> ObtenerPorIdAsync(int id)
    {
        var r = await _http.GetAsync($"api/car_innovacion/{id}");
        if (!r.IsSuccessStatusCode) return null;
        return await r.Content.ReadFromJsonAsync<CarInnovacion>();
    }

    public async Task<string?> CrearAsync(CarInnovacion item)
    {
        var r = await _http.PostAsJsonAsync("api/car_innovacion", item);
        if (r.IsSuccessStatusCode) return null;
        var e = await r.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        return e != null && e.TryGetValue("mensaje", out var m) ? m.ToString() : "no se pudo crear el registro";
    }

    public async Task<string?> ActualizarAsync(int id, CarInnovacion item)
    {
        var r = await _http.PutAsJsonAsync($"api/car_innovacion/{id}", item);
        if (r.IsSuccessStatusCode) return null;
        var e = await r.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        return e != null && e.TryGetValue("mensaje", out var m) ? m.ToString() : "no se pudo actualizar el registro";
    }

    public async Task EliminarAsync(int id) => await _http.DeleteAsync($"api/car_innovacion/{id}");
}
