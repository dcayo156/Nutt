/// <reference path="xpectro.references.js" />

//#region DECLARACIONES


var jtxbNombreCompleto2 = $("");
var jtxbNombreCompleto = $("");
var jddlEstado = $("");

var jpnlFiltro = $("");

//#endregion

//#region CARGA INICIAL

$(function () {
    jpnlFiltro = $("#pnlFiltro");
    jpnlFiltro.xpfiltro();
    jpnlFiltro.find("div.simple").formatearControles();

    $("table.frm").frm();
});

//#endregion

//#region MANEJO DE EVENTOS


//#endregion

//#region MÉTODOS

//#endregion