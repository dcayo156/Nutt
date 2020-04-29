/// <reference path="xpectro.references.js" />


//#region DECLARACIONES

var jtxbNombre = $("");
var jtxbApellidoPaterno = $("");
var jtxbApellidoMaterno = $("");
var jtxbCuenta = $("");
var jtxbContrasena = $("");
var jtxbContrasena2 = $("");
var jtxbCorreo = $("");
var jtxbTelefonoMovil = $("");
var jddlEstado = $("");
var jlblUltimaModificacion = $("");
var jtblPermisos = $("");

var idUsuario = -1;

var jmsgExito = $("");
var jmsgError1 = $("");
var jmsgError2 = $("");
var exito = "N";

//#endregion

//#region CARGA INICIAL

$(function () {
    jmsgExito = $("#msgExito");
    if (exito === "S") {
        jmsgExito.xpexito("mostrar", { mensaje: "Se registró el usuario correctamente." });
    }
    $("table.frm").frm();
    $("table.grd").grd();

    $("div.toolbar").tlb().on("click", ".cmd", cmdToolbar_click);

    jmsgError1 = $("#msgError1");
    jmsgError2 = $("#msgError2");

    jlblUltimaModificacion = $("#lblUltimaModificacion");

    jtblPermisos = $("#tblPermisos");

    jtxbNombre.focus();
});

//#endregion

//#region MANEJO DE EVENTOS

function cmdToolbar_click(evt) {
    var jcmd = $(evt.currentTarget);
    if (jcmd.hasClass("guardar")) {
        Guardar(jcmd.closest("div.toolbar").dataset("posicion"));
    }
}

//#endregion

//#region MÉTODOS

function Guardar(posicion) {
    jmsgExito.xpexito("ocultar");
    xpval.limpiarMensaje();
    xpval.mensaje = xpval.valForms();

    if (idUsuario === -1) {
        if (jtxbContrasena.valor && jtxbContrasena2.valor && jtxbContrasena.valor !== jtxbContrasena2.valor) {
            xpval.agregarLinea("Las contraseñas no coinciden.");
        }
    }

    var jmsgError;
    if (posicion === "arriba") {
        jmsgError = jmsgError1;
        jmsgError2.xperror("ocultar");
    }
    else {
        jmsgError = jmsgError2;
        jmsgError1.xperror("ocultar");
    }

    if (xpval.existeMensaje()) {
        jmsgError.xperror("mostrar", { mensaje: xpval.armarMensaje("No se puede registrar el usuario debido a que:") });
        return;
    }

    var dto = {
        eUsuario: {
            Id: idUsuario,
            Nombre: jtxbNombre.valor,
            ApellidoPaterno: jtxbApellidoPaterno.valor,
            ApellidoMaterno: jtxbApellidoMaterno.valor || null,
            Cuenta: jtxbCuenta.valor,
            Contrasena: jtxbContrasena.valor,
            Correo: jtxbCorreo.valor || null,
            Estado: Number(jddlEstado.valor)
        },
        lFunciones: jtblPermisos.find("input:checked").map(function (indice, elemento) {
            return $(this).val()
        }).get()
    };

    jmsgError.xperror("ocultar");
    xput.mostrarEsperaAjax();
    xpaj.pageMethod("Guardar", dto, function (data, status) {
        if (data.Estado === "ok") {
            if (idUsuario === -1) {
                location.href = xpglob.pathRaiz + "usuarios/" + data.Id + "?exito=S";
            }
            else {
                xput.ocultarEsperaAjax();
                alertify.success("Se guardaron los datos correctamente.");
                jlblUltimaModificacion.text(data.Mod);
            }
        }
        else {
            xput.ocultarEsperaAjax();
            jmsgError.xperror("mostrar", { mensaje: xpval.armarMensajeArreglo("No se puede registrar el usuario debido a que:", data.Mensajes) });
        }
    });
}

//#endregion