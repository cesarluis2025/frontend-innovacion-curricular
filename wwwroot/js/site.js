// Menú en pantallas angostas.
(function () {
    var armazon = document.querySelector(".armazon");
    var boton = document.getElementById("botonMenu");
    if (boton) boton.addEventListener("click", function () {
        var abierto = armazon.classList.toggle("menu-abierto");
        boton.setAttribute("aria-expanded", abierto ? "true" : "false");
    });
})();
// Cada listado largo gana un contador y un cuadro para filtrar sus filas por cualquier texto.
(function () {
    document.querySelectorAll("div.tabla").forEach(function (caja) {
        var tabla = caja.querySelector("table");
        if (!tabla || tabla.classList.contains("filas")) return;
        var filas = tabla.querySelectorAll("tbody tr");
        var contador = document.createElement("p");
        contador.className = "contador";
        var texto = function (n) { return n + (n === 1 ? " registro" : " registros"); };
        contador.textContent = texto(filas.length);
        caja.parentNode.insertBefore(contador, caja);
        if (filas.length < 8) return;
        var campo = document.createElement("input");
        campo.type = "search";
        campo.className = "filtro";
        campo.placeholder = "Filtrar la lista…";
        campo.setAttribute("aria-label", "Filtrar la lista");
        caja.parentNode.insertBefore(campo, caja);
        campo.addEventListener("input", function () {
            var q = campo.value.trim().toLowerCase(), visibles = 0;
            filas.forEach(function (f) {
                var ok = f.textContent.toLowerCase().indexOf(q) !== -1;
                f.style.display = ok ? "" : "none";
                if (ok) visibles++;
            });
            contador.textContent = texto(visibles) + (q ? " (filtrados)" : "");
        });
    });
})();
