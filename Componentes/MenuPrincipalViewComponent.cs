using Microsoft.AspNetCore.Mvc;
using FrontendInnovacionCurricular.Servicios;

namespace FrontendInnovacionCurricular.Componentes;

// El menú lateral, armado en UN solo lugar a partir de los permisos de la persona.
//
// ESCONDER UNA OPCIÓN NO PROTEGE NADA. Este menú solo hace cómoda la pantalla para
// quien sí tiene permiso. Quien escriba la dirección a mano o llame a la API
// directamente no pasa por aquí: se topa con la negativa de la API (y del filtro de
// páginas). Por eso los permisos se prueban escribiendo la dirección a mano.
//
// Se arma con los permisos de ESTA carga de página (no con los de cuando la persona
// entró): si le quitan un rol, el menú cambia en la siguiente pantalla.
public class MenuPrincipalViewComponent : ViewComponent
{
    private readonly PermisosActuales _permisos;
    public MenuPrincipalViewComponent(PermisosActuales permisos) => _permisos = permisos;

    public IViewComponentResult Invoke() => View(_permisos);
}
