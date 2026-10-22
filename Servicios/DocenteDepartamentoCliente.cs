using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

// Tabla puente: el registro se identifica por la pareja (a, b). Asignar es crear;
// retirar quita la pareja de la lista. Los métodos que escriben devuelven null si todo
// salió bien o el mensaje de la API si algo falló.
public class DocenteDepartamentoCliente
{
    private readonly HttpClient _http;
    public DocenteDepartamentoCliente(HttpClient http) => _http = http;

    public async Task<List<DocenteDepartamento>> ListarAsync()
        => await _http.GetFromJsonAsync<List<DocenteDepartamento>>("api/docente_departamento") ?? new();

    public async Task<string?> AsignarAsync(DocenteDepartamento item)
    {
        var r = await _http.PostAsJsonAsync("api/docente_departamento", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo guardar la asignación");
    }

    public async Task<DocenteDepartamento?> ObtenerAsync(int a, int b)
    {
        var r = await _http.GetAsync($"api/docente_departamento/{a}/{b}");
        if (!r.IsSuccessStatusCode) return null;
        return await r.Content.ReadFromJsonAsync<DocenteDepartamento>();
    }

    public async Task<string?> ActualizarAsync(int a, int b, DocenteDepartamento item)
    {
        var r = await _http.PutAsJsonAsync($"api/docente_departamento/{a}/{b}", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo actualizar la asignación");
    }

    public async Task<string?> RetirarAsync(int a, int b)
    {
        var r = await _http.DeleteAsync($"api/docente_departamento/{a}/{b}");
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo retirar la asignación");
    }
}
