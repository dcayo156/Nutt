/// <reference path="xpectro.references.js" />

//#region DECLARACIONES

var jtxbNombre = $("");
var jtxbDireccion = $("");
var jddlPais = $("");
var jddlCiudad = $("");
var jtxbPatrimonio = $("");
var jtxbSaldo = $("");
var jtxbHijos = $("");
var jtxbCorreo = $("");
var jtxbSitioWeb = $("");
var jtxbNombreCorto = $("");
var jtxbSupervisor = $("");
var jtxbSupervisado = $("");
var jtxbHora = $("");
var jtxbFechaFundacion = $("");
var jtdEstadoCivil = $("");
var jtdPasatiempos = $("");
var jtdComidas = $("");
var jtdFeriados = $("");
var jtdRespuestas = $("");
var jtdPersonas = $("");
var jtdAdjuntos = $("");
var jddlGerencia = $("");
var jtmpJefaturas = $.templates("");
var jddlJefatura = $("");
var jlblJefatura = $("");
var jtxbEmpleados = $("");
var jcontAdjuntoAutomaticoExterno = $("");
var jcontLogoAutomaticoInterno = $("");
var jtxbParametro = $("");
var jddlLista = $("");

var jdivHistorico = $("");

//#endregion

//#region INICIALIZACIÓN

$(function () {
    $("div.toolbar").tlb().on("click", "a.btn", btn_click);
    $("table.frm").frm();
    $("table.grd").grd();

    $("#contAdjuntoManual").xpadjuntos();

    $("#tbodyDetalle").on("click", ".cmd", cmdDetalle_click)
        .bind("filaagregada", btnAgregarDetalle_click);
    $("#btnBuscar").click(btnBuscar);

    jlblJefatura = $("#lblJefatura");
    jtmpJefaturas = $.templates("{{for Jefaturas}}<option value='{{>val}}'>{{>des}}</option>{{/for}}");
    jddlGerencia.change(ddlGerencia_change);

    $("#pnlDetalle").find("table").pagGrid(4, 1, 1, null);

    var jtblResultado2 = $("#pnlDetalle2").find("table");
    //Hacer esto si es que se alcanzó el límite máximo a mostrar
    jtblResultado2.dataset("limitealcanzado", "S");
    jtblResultado2.pagGrid(4, 1, 1, null);

    jdivHistorico = $("#divHistorico");
    jdivHistorico.dialog({
        autoOpen: false,
        bgiframe: true,
        buttons: [
            {
                text: "Opción 1",
                icons: {
                    primary: "ui-icon-closethick"
                },
                click: function () {
                    alert("opción 1");
                }
            },
            {
                text: "Opción 2",
                icons: {
                    primary: "ui-icon-lightbulb"
                },
                click: function () {
                    alert("opción 2");
                }
            }
        ],
        height: 400,
        width: 500,
        modal: true,
        resizable: false,
        title: "Prueba"
    })

    jtxbNombre.focus();
});

//#endregion

//#region MANEJO DE EVENTOS

function ddlGerencia_change(evt) {
    GerenciasSeleccionadas();
}
function btnBuscar(evt) {
    Buscar();
}
function btn_click(evt) {
    var jbtn = $(evt.currentTarget);
    if (jbtn.hasClass("guardar")) {
        Guardar();
    }
}
function cmdDetalle_click(evt) {
    var jcmd = $(evt.currentTarget);
    if (jcmd.hasClass("editar")) {
        alert("editar");
    }
    else if (jcmd.hasClass("eliminar")) {
        Eliminar(jcmd);
    }
    else if (jcmd.hasClass("historico")) {
        VerHistorico(jcmd);
    }
}
function btnAgregarDetalle_click(evt, jtr) {
    jtr.find("input.nombre").focus();
}

//#endregion

//#region MÉTODOS

function GerenciasSeleccionadas() {
    jddlJefatura.xpSumoSelectUnload();
    jddlJefatura.hide();
    jddlJefatura.html("");
    jlblJefatura.show();
    if (!jddlGerencia.val()) {
        jlblJefatura.text("Seleccione primero uno o varias gerencias");
        return;
    }
    jlblJefatura.text("Cargando...");
    var idsGerencias = [];
    idsGerencias = $.map(jddlGerencia.val(), function (elemento, indice) {
        return Number(elemento);
    })
    var dto = {
        idsGerencias: idsGerencias
    };
    xpaj.pageMethod("TraerJefaturasxGerencias", dto, function (data, status) {
        if (data.length === 0) {
            jlblJefatura.text("No se encontraron jefaturas");
            return;
        }
        jddlJefatura.html(jtmpJefaturas.render({ Jefaturas: data }));
        jlblJefatura.hide();
        jddlJefatura.show();
        jddlJefatura.xpSumoSelect();
    });
}
function LeerParametrosEmpleados(jtxb) {
    /* Este arreglo debe contener los parámetros adicionales a ser enviados al método ajax.
     * Para cada parámetro se agrega dos elementos: 1) el nombre del parámetro, 2) el valor del parámetro
     */
    var parametros = [];

    //Primer parámetro: primero se agrega el nombre del parámetro
    parametros.push("idsGerencias");
    //Primer parámetro: luego se agrega el valor de parámetro
    if (!jddlGerencia.val()) {
        parametros.push(null);
    }
    else {
        parametros.push($.map(jddlGerencia.val(), function (elemento, indice) {
            return elemento;
        }).join(","));
    }

    //Segundo parámetro: primero se agrega el nombre del parámetro
    parametros.push("idsJefaturas");
    //Segundo parámetro: luego se agrega el valor de parámetro
    if (!jddlJefatura.val()) {
        parametros.push(null);
    }
    else {
        parametros.push($.map(jddlJefatura.val(), function (elemento, indice) {
            return elemento;
        }).join(","));
    }

    //Finalmente se convierte a formato JSON el arreglo con los parámetros y se lo agrega el textbox, en la propiedad "parametros"
    //De dicha propiedad será leído al realizar la llamada AJAX y los parámetros serán agregados automáticamente
    jtxb.dataset("parametros", JSON.stringify(parametros));
}
function Buscar() {
    var mensajes = xpval.valForms({ grupo: "2" });
    if (mensajes.length > 0) {
        xpdia.showInfo("Prueba", xpval.armarMensajeArreglo("No se pudo buscar debido a que:", mensajes), null, 400, 280);
    }
}
function Eliminar(jcmd) {
    /// <param name="jcmd" type="$"></param>
    xpdia.showConfirm("Prueba", "¿Está seguro?", function () {
        alert("Eliminar!");
    }, null);
}
function VerHistorico(jcmd) {
    /// <param name="jcmd" type="$"></param>
    jdivHistorico.dialog("open");
}
function Guardar() {
    var mensajes = xpval.valForms({ grupo: "1" });
    if (mensajes.length > 0) {
        xpdia.showInfo("Prueba", xpval.armarMensajeArreglo("No se pudo guardar debido a que:", mensajes), null, 400, 280);
        return;
    }

    /* EJEMPLO DE OBTENCIÓN DE DATOS CUANDO NO HAY NINGÚN ERROR
     * En cada control, la propiedad "valor" contiene el valor verificado y validado durante la validación (valForms).
     * Si no hubiera introducido nada en el control, "valor" contiene el valor null.
     */

    var dto = {
        nombreEmpresa: jtxbNombre.valor,
        direccion: jtxbDireccion.valor,

        //Para las listas, el valor siempre es tipo string, hay que convertirlo
        idPais: Number(jddlPais.valor),

        //Las listas con selección múltiple devuelven directamente un arreglo ("jddlCiudad.valor" o "jddlCiudad.val()") pero siempre de tipo string, hay que convertirlo
        idsCiudades: $.map(jddlCiudad.valor, function (elemento, indice) {
            return Number(elemento);
        }),

        //Para los campos numéricos y decimales, el valor ya está convertido al tipo de dato requerido
        patrimonio: jtxbPatrimonio.valor,
        saldo: jtxbSaldo.valor,
        hijos: jtxbHijos.valor,

        correo: jtxbCorreo.valor,
        sitioWeb: jtxbSitioWeb.valor,
        nombreCorto: jtxbNombreCorto.valor,

        //Para los autocomplete, la propiedad "valor" contiene el "valoractual" (identificador) configurado para el item seleccionado, y es de tipo string
        supervisor: jtxbSupervisor.valor,
        supervisado: jtxbSupervisado.valor,

        /* La propiedad valor contiene un objeto con las siguientes propiedades: 
         * horas, minutos, ticks (1 seg= 10,000,000 ticks) y valorInvariant (formato h:mm:ss)
         */
        horaHoras: jtxbHora.valor.horas,
        horaMinutos: jtxbHora.valor.minutos,

        /* La propiedad valor contiene un objeto con las siguientes propiedades: 
         * valor: objeto de tipo Date de javascript que representa la fecha introducida
         * valorTexto: mismo valor introducido en el textbox
         * valorInvariant: texto con formato MM/dd/aaaa (formato requerido por .NET para interpretar correctamente las fechas)
         */
        fechaFundacion: jtxbFechaFundacion.valor.valorInvariant,

        //Devuelve el valor de "value" del radiobutton seleccionado
        estadoCivil: jtdEstadoCivil.valor,

        //"valor" contiene un arreglo con todos los valores "value" de los checkbox seleccionados
        //Dependiendo del caso, puede ser necesario convertirlo a algún tipo de dato (como abajo 
        //que se lo convierte a entero, usando la función "map" de jquery y convirtiendo cada item en número)
        pasatiempos: $.map(jtdPasatiempos.valor, function (elemento, indice) {
            return Number(elemento);
        }),

        //"valor" contiene un arreglo con todas los "value" en el combo original que tienen los ítems agregados
        comidas: jtdComidas.valor,

        //"valor" contiene un arreglo de objetos, donde cada objeto contiene las mismas propiedades que en el caso
        //de la fecha individual (fechaFundacion en el ejemplo de arriba)
        //Lo más normal será formatearlo de alguna manera: abajo se lo convierte a un arreglo que será tomado por .NET como arreglo de fechas
        feriados: $.map(jtdFeriados.valor, function (elemento, indice) {
            return elemento.valorInvariant
        }),

        //"valor" contiene un arreglo con todos los textos (trimeados)
        respuestas: jtdRespuestas.valor,

        //"valor" contiene un arreglo en el que cada ítem es la propiedad "propval" del ítem seleccionado del autocomplete cuando fue agregado
        personas: jtdPersonas.valor,

        /* La propiedad valor contiene un objeto con las siguientes propiedades: 
         * - Identificador: identificador del adjunto agregado (normalmente tendrá valor si fue cargado con la página, sino no)
         * - ArchivoReal: nombre real del archivo (ya sea el registrado en la BD o el nombre del archivo recién subido)
         * - ArchivoTemporal: nombre del archivo en la carpeta temp (normalmente sólo tendrá valor cuando sea un archivo RECIÉN AGREGADO)
         * En el ejemplo, se arma un arreglo con los tres valores
         */
        adjuntos: $.map(jtdAdjuntos.valor, function (elemento, indice) {
            return {
                Id: elemento.Identificador || -1, //digamos que se desea mandar -1 en lugar de NULL cuando se trata de uno nuevo
                Nombre: elemento.ArchivoReal,
                Temp: elemento.ArchivoTemporal
            };
        })
    };

    window.testdto = dto;
}

//#endregion