// Menú en pantallas angostas.
(function () {
    var armazon = document.querySelector(".armazon");
    var boton = document.getElementById("botonMenu");
    if (boton) boton.addEventListener("click", function () {
        var abierto = armazon.classList.toggle("menu-abierto");
        boton.setAttribute("aria-expanded", abierto ? "true" : "false");
    });
})();
// El aviso de «guardado» se desvanece solo.
(function () {
    var aviso = document.querySelector(".toast");
    if (!aviso) return;
    setTimeout(function () { aviso.classList.add("oculto"); setTimeout(function () { aviso.remove(); }, 450); }, 5000);
})();
// Listados: contador, filtro y mensaje cuando no hay nada.
(function () {
    document.querySelectorAll("div.tabla").forEach(function (caja) {
        var tabla = caja.querySelector("table");
        if (!tabla || tabla.classList.contains("filas")) return;
        var filas = tabla.querySelectorAll("tbody tr");
        var texto = function (n) { return n + (n === 1 ? " registro" : " registros"); };
        if (filas.length === 0) {
            var crear = document.querySelector(".titulo a.principal");
            var vacio = document.createElement("div");
            vacio.className = "vacio-caja";
            vacio.innerHTML = '<svg class="ico" aria-hidden="true"><use href="#i-bandeja"/></svg><p>Todavía no hay registros.</p>';
            if (crear) { var c = crear.cloneNode(true); vacio.appendChild(c); }
            caja.replaceWith(vacio);
            return;
        }
        var contador = document.createElement("p");
        contador.className = "contador";
        contador.textContent = texto(filas.length);
        caja.parentNode.insertBefore(contador, caja);
        if (filas.length < 8) return;
        var campo = document.createElement("input");
        campo.type = "search";
        campo.className = "filtro";
        campo.placeholder = "Filtrar la lista…";
        campo.setAttribute("aria-label", "Filtrar la lista");
        caja.parentNode.insertBefore(campo, contador);
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
// Confirmación al retirar: una ventana propia en lugar del cuadro del navegador.
document.addEventListener("submit", function (e) {
    var f = e.target;
    var mensaje = f.dataset ? f.dataset.confirmar : null;
    if (!mensaje || f.dataset.confirmado) return;
    e.preventDefault();
    var d = document.createElement("dialog");
    d.className = "dialogo";
    d.innerHTML = '<h2>Retirar registro</h2><p></p><div class="botones"><button type="button" class="cancelar">Cancelar</button><button type="button" class="confirmar">Retirar</button></div>';
    d.querySelector("p").textContent = mensaje.replace(/^¿/, "").replace(/\?$/, "") + ". Dejará de aparecer en las listas, pero no se borra de la base de datos.";
    d.querySelector(".cancelar").addEventListener("click", function () { d.close(); d.remove(); });
    d.querySelector(".confirmar").addEventListener("click", function () { f.dataset.confirmado = "1"; d.close(); f.submit(); });
    d.addEventListener("cancel", function () { d.remove(); });
    document.body.appendChild(d);
    d.showModal();
});
