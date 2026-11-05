using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;

namespace FrontendInnovacionCurricular.Servicios;

// Pone la credencial de LA PERSONA de esta petición en cada llamada a la API.
//
// Por qué aquí y no en HttpClient.DefaultRequestHeaders: el HttpClient tipado se
// comparte entre todas las personas conectadas. Una cabecera fijada ahí le pasaría la
// credencial de la última persona que entró a TODAS las demás: el sistema respondería,
// y lo haría con la credencial de otro, sin dar ningún error. Aquí se lee del
// HttpContext de la petición en curso, así que cada llamada lleva la suya.
//
// El manejador no guarda nada en campos: solo el IHttpContextAccessor, que da el
// contexto de la petición actual. Por eso es seguro que el factory lo reutilice.
public class TokenHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _contexto;
    public TokenHandler(IHttpContextAccessor contexto) => _contexto = contexto;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage peticion, CancellationToken ct)
    {
        var http = _contexto.HttpContext;
        var credencial = http is null ? null : await http.GetTokenAsync("access_token");
        if (!string.IsNullOrEmpty(credencial))
            peticion.Headers.Authorization = new AuthenticationHeaderValue("Bearer", credencial);

        var respuesta = await base.SendAsync(peticion, ct);

        // La API dice «no sé quién eres»: la sesión venció. (El 403, «no puedes hacer
        // esto», NO entra aquí: lo muestra la pantalla como un mensaje.)
        if (respuesta.StatusCode == HttpStatusCode.Unauthorized)
        {
            respuesta.Dispose();
            throw new SesionVencidaExcepcion();
        }
        return respuesta;
    }
}
