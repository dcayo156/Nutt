/// <reference path="xpectro.references.js" />

//#region DECLARACIONES

var jbtnIngresar = $("");
var jtxbCuenta = $("");
var jtxbContrasena = $("");
var jlblMensaje = $("");

//#endregion

//#region CARGA INICIAL

$(function () {
    $("#btnIngresar").button().click(btnIngresar_Click);

    $("#btnIngresar2").click(btnIngresar_Click);

    jtxbCuenta = $("#txbCuenta");
    jtxbContrasena = $("#txbContrasena");

    jlblMensaje = $("#lblMensaje");
    var aux = jtxbCuenta.val();
    if (aux) {
        jtxbCuenta.val("").val(aux).select();
    }
    else {
        jtxbCuenta.focus();
    }
});

//#endregion

//#region MANEJO DE EVENTOS

function btnIngresar_Click(evt) {
    return Ingresar();
}

//#endregion

//#region MÉTODOS

function Ingresar() {
    jlblMensaje.hide();
    var correo = $.trim(jtxbCuenta.val());
    var contrasena = $.trim(jtxbContrasena.val());
    if (!correo || !contrasena) {
        jlblMensaje.show().find("span.contenido").text("Debe indicar su cuenta de usuario y contraseña para ingresar al sitio.");
        return false;
    }
    setTimeout(function () {
        jbtnIngresar.button("disable");
    }, 100);
    return true;
}

//#endregion