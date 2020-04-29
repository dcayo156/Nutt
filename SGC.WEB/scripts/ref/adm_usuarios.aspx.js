/// <reference path="xpectro.references.js" />


//#region DECLARACIONES


var jtxbNombreCompleto2 = $("");
var jtxbNombreCompleto = $("");
var jtxbCuenta = $("");
var jddlEstado = $("");

var jpnlFiltro = $("");
var jpnlResultado = $("");
var jtmpResultado = null;


//#endregion

//#region CARGA INICIAL

$(function () {
    jpnlFiltro = $("#pnlFiltro");
    jpnlFiltro.xpfiltro();
    jpnlFiltro.find("div.simple").formatearControles();

    $("table.frm").frm();

    jpnlResultado = $("#pnlResultado");
    jtmpResultado = $.templates("#tmpResultado");

    jtxbNombreCompleto2 = $("#txbNombreCompleto2");
    $("#btnBuscar,#btnBuscar2").click(btnBuscar_click);

    jpnlResultado.on("click", ".cmd", cmdresultado_click);

    RestaurarFiltrosBusqueda();
    
});

//#endregion

//#region MANEJO DE EVENTOS
function btnBuscar_click(evt) {
    Buscar(1);
}
function cmdresultado_click(evt) {
    
}

//#endregion

//#region MÉTODOS
function Buscar(pagina) {
    pagina = pagina || 1;

    var nombreCompleto, cuenta = "", estado = -1;
    if (jpnlFiltro.dataset("modo") === "simple") {
        nombreCompleto = $.trim(jtxbNombreCompleto2.val());
    }
    else {
        xpval.limpiarMensaje();
        xpval.valForms();
        nombreCompleto = jtxbNombreCompleto.valor;
        cuenta = jtxbCuenta.valor;
        estado = Number(jddlEstado.val());
    }

    var dto = {
        filter: {
            NombreCompleto: nombreCompleto,
            Cuenta: cuenta,
            Estado: estado
        },
        pagina: pagina
    };

    GuardarFiltrosBusqueda(dto);

    jpnlResultado.inicioEsperaBusqueda("20px");

    xpaj.pageMethod("Buscar", dto, function (data, status) {
        jpnlResultado.html(jtmpResultado.render(data));
        jpnlResultado.find(".grd").grd().pagGrid(data.CantidadRegistros, data.CantidadPaginas, pagina, Buscar).formatearTabla();
    });
}

function GuardarFiltrosBusqueda(dto) {
    if (localStorage) {
        localStorage.setItem("adm_usuarios_busqueda" + xpglob.IdUsuario, JSON.stringify({
            modoBusqueda: jpnlFiltro.dataset("modo"),
            nombreCompleto: dto.nombreCompleto,
            cuenta: dto.cuenta,
            estado: dto.estado,
            pagina: dto.pagina
        }));
    }
}

function RestaurarFiltrosBusqueda() {
    if (localStorage && localStorage.getItem("adm_usuarios_busqueda" + xpglob.IdUsuario)) {
        var dto = JSON.parse(localStorage.getItem("adm_usuarios_busqueda" + xpglob.IdUsuario));

        jpnlFiltro.dataset("modo", dto.modoBusqueda);
        jpnlFiltro.xpfiltro("recalcularmodo");

        if (dto.modoBusqueda == "simple") {
            jtxbNombreCompleto2.val(dto.nombreCompleto);
        }
        else {
            jtxbNombreCompleto.val(dto.nombreCompleto);
            jtxbCuenta.val(dto.cuenta);
            jddlEstado.val(dto.estado);
        }
        Buscar(dto.pagina);
    }
}
//#endregion