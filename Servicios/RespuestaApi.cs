using System.Text.Json;

namespace FrontendInnovacionCurricular.Servicios;

// Traduce una respuesta de error de la API a una frase para la pantalla.
// La API responde { "mensaje": "..." } en los errores de negocio (no existe, ya existe,
// tiene registros activos) y un objeto «errors» cuando un dato no cumple el formato.
public static class RespuestaApi
{
    public static async Task<string> LeerErrorAsync(HttpResponseMessage respuesta, string porDefecto)
    {
        try
        {
            var texto = await respuesta.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(texto);
            var raiz = doc.RootElement;
            if (raiz.TryGetProperty("mensaje", out var mensaje) && mensaje.GetString() is string m)
                return m;
            if (raiz.TryGetProperty("errors", out var errores) && errores.ValueKind == JsonValueKind.Object)
            {
                var frases = errores.EnumerateObject()
                    .SelectMany(p => p.Value.EnumerateArray().Select(v => v.GetString()))
                    .Where(s => !string.IsNullOrWhiteSpace(s));
                var unidas = string.Join(" · ", frases);
                if (unidas.Length > 0) return unidas;
            }
        }
        catch (JsonException) { }
        return porDefecto;
    }
}
