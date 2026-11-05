using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontendInnovacionCurricular.Servicios;

namespace FrontendInnovacionCurricular.Autorizacion;

// Filtro GLOBAL de páginas: corre en TODA página antes de ejecutar su manejador. Por
// cada carga pregunta a la API por los permisos de la persona (una vez; el menú y los
// botones leen la misma respuesta) y niega la página si no los tiene.
//
// Falla CERRADO: una página sin [PermisoPagina] ni [SoloIdentificadoPagina] ni
// [AllowAnonymous] se niega. Una pantalla nueva a la que nadie le puso permiso se nota
// el primer día en vez de quedar abierta.
//
// Esto protege las PANTALLAS; el dato lo protege la API con su propio permiso. Las dos
// capas dicen lo mismo, pero la que cuenta es la de la API.
public class FiltroPaginaPermiso : IAsyncPageFilter
{
    private readonly PermisosActuales _permisos;
    public FiltroPaginaPermiso(PermisosActuales permisos) => _permisos = permisos;

    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext contexto) => Task.CompletedTask;

    // Se decide justo antes de ejecutar el manejador de la página: si se niega, el manejador
    // NO corre (ni lista, ni guarda, ni retira nada).
    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext contexto, PageHandlerExecutionDelegate siguiente)
    {
        if (await EvaluarAsync(contexto) is { } negativa)
        {
            contexto.Result = negativa;
            return;
        }
        await siguiente();
    }

    // null = puede seguir; si no, el resultado que lo detiene.
    private async Task<IActionResult?> EvaluarAsync(PageHandlerExecutingContext contexto)
    {
        var marcas = contexto.ActionDescriptor.EndpointMetadata;
        if (marcas.OfType<IAllowAnonymous>().Any()) return null;

        // Se consulta SIEMPRE (también en las páginas solo-identificado) para que el menú
        // tenga con qué armarse. Una sesión vencida lanza SesionVencidaExcepcion y
        // Program.cs lleva a la entrada.
        await _permisos.ObtenerAsync();
        if (marcas.OfType<SoloIdentificadoPaginaAttribute>().Any()) return null;

        var exige = marcas.OfType<PermisoPaginaAttribute>().LastOrDefault();
        if (exige is null) return new ForbidResult();

        var metodo = contexto.HttpContext.Request.Method;
        var lee = HttpMethods.IsGet(metodo) || HttpMethods.IsHead(metodo);
        var operacion = exige.Operacion == "escribir" || !lee ? "escribir" : "leer";
        // ForbidResult lleva a la pantalla «sin permiso» (AccessDeniedPath de la cookie).
        return _permisos.Puede(exige.Grupo, operacion) ? null : new ForbidResult();
    }
}
