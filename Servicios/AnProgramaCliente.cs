using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

// Tabla puente: el registro se identifica por la pareja (a, b). Asignar es crear;
// retirar quita la pareja de la lista. Los métodos que escriben devuelven null si todo
// salió bien o el mensaje de la API si algo falló.
public class AnProgramaCliente
{
    private readonly HttpClient _http;
    public AnProgramaCliente(HttpClient http) => _http = http;

    public async Task<List<AnPrograma>> ListarAsync()
        => await _http.GetFromJsonAsync<List<AnPrograma>>("api/an_programa") ?? new();

    public async Task<string?> AsignarAsync(AnPrograma item)
    {
        var r = await _http.PostAsJsonAsync("api/an_programa", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo guardar la asignación");
    }

    public async Task<string?> RetirarAsync(int a, int b)
    {
        var r = await _http.DeleteAsync($"api/an_programa/{a}/{b}");
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo retirar la asignación");
    }
}
