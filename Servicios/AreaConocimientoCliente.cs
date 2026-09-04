using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

// Esta clase es la única que le habla a la API. Las páginas Razor
// nunca hacen HttpClient directo, siempre pasan por aquí.
public class AreaConocimientoCliente
{
    private readonly HttpClient _http;
    public AreaConocimientoCliente(HttpClient http) => _http = http;

    public async Task<List<AreaConocimiento>> ListarAsync()
        => await _http.GetFromJsonAsync<List<AreaConocimiento>>("api/area_conocimiento") ?? new();

    public async Task<AreaConocimiento?> ObtenerPorIdAsync(int id)
    {
        var respuesta = await _http.GetAsync($"api/area_conocimiento/{id}");
        if (!respuesta.IsSuccessStatusCode) return null;
        return await respuesta.Content.ReadFromJsonAsync<AreaConocimiento>();
    }

    // Devuelve el mensaje de error de la API si algo sale mal (por ejemplo,
    // id repetido), o null si todo salió bien — así la página sabe qué mostrar.
    public async Task<string?> CrearAsync(AreaConocimiento area)
    {
        var respuesta = await _http.PostAsJsonAsync("api/area_conocimiento", area);
        if (respuesta.IsSuccessStatusCode) return null;
        var error = await respuesta.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        return error != null && error.TryGetValue("mensaje", out var m) ? m.ToString() : "no se pudo crear el registro";
    }

    public async Task<string?> ActualizarAsync(int id, AreaConocimiento area)
    {
        var respuesta = await _http.PutAsJsonAsync($"api/area_conocimiento/{id}", area);
        if (respuesta.IsSuccessStatusCode) return null;
        var error = await respuesta.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        return error != null && error.TryGetValue("mensaje", out var m) ? m.ToString() : "no se pudo actualizar el registro";
    }

    public async Task EliminarAsync(int id)
        => await _http.DeleteAsync($"api/area_conocimiento/{id}");
}
