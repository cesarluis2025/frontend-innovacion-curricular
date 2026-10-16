using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

// Habla con la API por HTTP; el frontend nunca toca la base de datos. Los métodos que
// escriben devuelven null si todo salió bien o el mensaje de la API si algo falló.
public class ActivAcademicaCliente
{
    private readonly HttpClient _http;
    public ActivAcademicaCliente(HttpClient http) => _http = http;

    public async Task<List<ActivAcademica>> ListarAsync()
        => await _http.GetFromJsonAsync<List<ActivAcademica>>("api/activ_academica") ?? new();

    public async Task<ActivAcademica?> ObtenerPorIdAsync(int id)
    {
        var r = await _http.GetAsync($"api/activ_academica/{id}");
        if (!r.IsSuccessStatusCode) return null;
        return await r.Content.ReadFromJsonAsync<ActivAcademica>();
    }

    public async Task<string?> CrearAsync(ActivAcademica item)
    {
        var r = await _http.PostAsJsonAsync("api/activ_academica", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo crear el registro");
    }

    public async Task<string?> ActualizarAsync(int id, ActivAcademica item)
    {
        var r = await _http.PutAsJsonAsync($"api/activ_academica/{id}", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo actualizar el registro");
    }

    public async Task<string?> EliminarAsync(int id)
    {
        var r = await _http.DeleteAsync($"api/activ_academica/{id}");
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo retirar el registro");
    }
}
