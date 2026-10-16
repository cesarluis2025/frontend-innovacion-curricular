// Filas del maestro-detalle: agregar una fila copia la última y renumera sus campos.
document.querySelectorAll(".agregar-fila").forEach(function (boton) {
    boton.addEventListener("click", function () {
        var cuerpo = document.querySelector("#" + boton.dataset.tabla + " tbody");
        var ultima = cuerpo.lastElementChild;
        var nueva = ultima.cloneNode(true);
        var n = cuerpo.children.length;
        nueva.querySelectorAll("input, select").forEach(function (campo) {
            campo.name = campo.name.replace(/\[\d+\]/, "[" + n + "]");
            campo.id = campo.id.replace(/_\d+__/, "_" + n + "__");
            if (campo.tagName === "SELECT") campo.selectedIndex = 0;
            else if (campo.type !== "date") campo.value = "";
            campo.disabled = false;
        });
        cuerpo.appendChild(nueva);
        alternarEspejo(nueva);
    });
});
// Actividad espejo: si la fila no es espejo, entidad y país se deshabilitan (la API guarda «No aplica»).
function alternarEspejo(fila) {
    var sel = fila.querySelector("select.espejo");
    if (!sel) return;
    fila.querySelectorAll(".espejo-dep").forEach(function (c) { c.disabled = sel.value !== "1"; });
}
document.querySelectorAll("tr").forEach(alternarEspejo);
document.addEventListener("change", function (e) {
    if (e.target.classList && e.target.classList.contains("espejo")) alternarEspejo(e.target.closest("tr"));
});
