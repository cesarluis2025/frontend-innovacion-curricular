using System.Net.Http.Json;
using FrontendInnovacionCurricular.Modelos;

namespace FrontendInnovacionCurricular.Servicios;

// Tabla puente: el registro se identifica por la pareja (a, b). Asignar es crear;
// retirar quita la pareja de la lista. Los métodos que escriben devuelven null si todo
// salió bien o el mensaje de la API si algo falló.
public class ProgramaCiCliente
{
    private readonly HttpClient _http;
    public ProgramaCiCliente(HttpClient http) => _http = http;

    public async Task<List<ProgramaCi>> ListarAsync()
        => await _http.GetFromJsonAsync<List<ProgramaCi>>("api/programa_ci") ?? new();

    public async Task<string?> AsignarAsync(ProgramaCi item)
    {
        var r = await _http.PostAsJsonAsync("api/programa_ci", item);
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo guardar la asignación");
    }

    public async Task<string?> RetirarAsync(int a, int b)
    {
        var r = await _http.DeleteAsync($"api/programa_ci/{a}/{b}");
        return r.IsSuccessStatusCode ? null : await RespuestaApi.LeerErrorAsync(r, "no se pudo retirar la asignación");
    }
}
