/// <reference path="../ref/xpectro.references.js" />

xpectro = {
    global: { //GLOBAL
        pathRaiz: "",
        tamanoPagina: 16,
        limiteResultadosBusqueda: 5,
        IdUsuario: -1,
        IdEmpresa: -1,
        terminos: {
            Aceptar: "Aceptar",
            Cancelar: "Cancelar",
            NO: "NO",
            SI: "SI",
            Pagina: "P\341gina",
            PROCESANDO: "PROCESANDOs"
        },
        iconos: {
            NO: "ui-icon-cancel",
            SI: "ui-icon-check"
        },
        limiteTamanoAdjuntos: 20 * 1024 * 1024 //100 MB
    }, //FIN GLOBAL
    utils: {
        jdivEsperaAjax: null,
        jdivFondoEsperaAjax: null,
        pathImagenEsperaAjax: "css/img/procesamiento.gif",
        mostrarEsperaAjax: function () {
            this.jdivEsperaAjax = $("<div class='esperaAjax'><img alt='' src='" + xpectro.global.pathRaiz +
               this.pathImagenEsperaAjax + "' style='margin-bottom:5px' /><br/>" + xpectro.global.terminos.PROCESANDO + "...</div>");
            this.jdivFondoEsperaAjax = $("<div class='fondoEsperaAjax'>&nbsp;</div>");
            this.jdivEsperaAjax.appendTo("body");
            this.jdivFondoEsperaAjax.appendTo("body");
            var width = $(document).width();
            var height = $(document).height();
            this.jdivFondoEsperaAjax.css({ width: width + "px", height: height + "px" }).show();
            this.jdivEsperaAjax.position({
                my: "center",
                at: "center",
                of: window,
                collision: "fit"
            }).show();
        },
        ocultarEsperaAjax: function () {
            if (this.jdivEsperaAjax) {
                this.jdivEsperaAjax.remove();
                this.jdivFondoEsperaAjax.remove();
                this.jdivEsperaAjax = null;
                this.jdivFondoEsperaAjax = null;
            }
        },
        bajarArchivo: function (url, data) {
            if (url) {
                var inputs = '';
                if (data && data.length > 0) {
                    $.each(data.split('&'), function () {
                        var par = this.split('=');
                        inputs += '<input type="hidden" name="' + par[0] + '" value="' + par[1] + '" />';
                    });
                }
                $("<form action='" + url + "' method='post'>" + inputs + "</form>").appendTo('body').submit();
            };
        }
    },
    ajax: { // AJAX
        webServiceMethod: function (urlServicio, nombreMetodo, dto, callbackExito, callbackError) {
            $.ajax({
                type: "POST",
                url: urlServicio + "/" + nombreMetodo,
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(dto),
                dataType: "json",
                success: function (data, status) {
                    if (callbackExito) {
                        if (data.hasOwnProperty("d")) {
                            callbackExito(data.d, status, this);
                        }
                        else {
                            callbackExito(data, status, this);
                        }
                    }
                },
                error: function (response, settings) {
                    xput.ocultarEsperaAjax();
                    if (callbackError) {
                        callbackError(response, settings);
                    }
                    else {
                        try {
                            xpglob.ocultarEsperaAjax();
                        }
                        catch (e) {
                        }
                        if (response.responseText) {
                            try {
                                var dto = JSON.parse(response.responseText);
                                if (dto.Message === "errorSesion") {
                                   location = xpectro.global.pathRaiz + "inicio"
                                } else {
                                   alert(dto.Message);
                                }
                            }
                            catch (e) {
                                alert(response.responseText);
                            }
                        }
                    }
                }
            });
        },
        pathPagina: null,
        pageMethod: function (nombreMetodo, dto, callbackExito, callbackError) {
            var urlServicio;
            if (!this.pathPagina) {
                urlServicio = window.location.pathname;
            }
            else {
                urlServicio = this.pathPagina;
            }
            this.webServiceMethod(urlServicio, nombreMetodo, dto, callbackExito, callbackError);
        }
    }, // FIN AJAX
    dialogos: { //DIÁLOGOS
        anchoDefecto: 400,
        altoDefecto: 250,
        anchoDefectoConfirmacion: 250,
        altoDefectoConfirmacion: 145,
        showBase: function (tipo, titulo, mensaje, callback1, callback2, ancho, alto, clase) {
            ancho = ancho || this.anchoDefecto;
            alto = alto || this.altoDefecto;
            var jdiv = $("<div/>").appendTo("body");
            var buttons = [];
            var terminos = xpectro.global.terminos;
            var iconos = xpectro.global.iconos;
            var callbackCerrar = null;
            jdiv.dest = function () { jdiv.dialog("destroy").remove(); };
            if (tipo === "I") //show Info
            {
                callbackCerrar = callback1;
                buttons.push({
                    text: terminos.Aceptar,
                    icons: {
                        primary: "ui-icon-check"
                    },
                    click: function () {
                        jdiv.dest();
                        if (callbackCerrar) {
                            callbackCerrar();
                        }
                    }
                });
            }
            else {
                buttons.push({
                    text: terminos.SI,
                    icons: {
                        primary: iconos.SI
                    },
                    click: function () {
                        jdiv.dest();
                        if (callback1) {
                            callback1();
                        }
                    }
                });
                buttons.push({
                    text: terminos.NO,
                    icons: {
                        primary: iconos.NO
                    },
                    click: function () {
                        jdiv.dest();
                        if (callback2) {
                            callback2();
                        }
                    }
                });
                if (tipo === "CC") {
                    buttons.push({
                        text: terminos.Cancelar,
                        icons: {
                            primary: "ui-icon-heart"
                        },
                        click: function () {
                            jdiv.dest();
                        }
                    });
                }
            }
            jdiv.dialog({
                autoOpen: false,
                bgiframe: true,
                buttons: buttons,
                height: alto,
                width: ancho,
                modal: true,
                resizable: false,
                title: titulo,
                close: function () {
                    jdiv.dest();
                    if (callbackCerrar) {
                        callbackCerrar();
                    }
                }
            }).html(mensaje);
            if (clase) {
                jdiv.dialog("option", "dialogClass", clase);
            }
            jdiv.dialog("open");
        },
        showInfo: function (titulo, mensaje, callback, ancho, alto) {
            this.showBase("I", titulo, mensaje, callback, null, ancho, alto);
        },
        showConfirm: function (titulo, mensaje, callbackSi, callbackNo, ancho, alto) {
            this.showBase("C", titulo, mensaje, callbackSi, callbackNo, ancho || this.anchoDefectoConfirmacion, alto || this.altoDefectoConfirmacion, "confirmacion");
        },
        showConfirmCancel: function (titulo, mensaje, callbackSi, callbackNo, ancho, alto) {
            this.showBase("CC", titulo, mensaje, callbackSi, callbackNo, ancho, alto);
        }
    }, //FIN DIÁLOGOS
    validacion: { //VALIDACIÓN
        valNum: function (val) {
            return /^\d+$/.test(val);
        },
        patronNumeroDecimal: null,
        separadorMiles: ",",
        separadorDecimales: ".",
        valDec: function (val, posicionesDecimales) {
            var patronValidacionNumeroDecimal;
            if (this.patronNumeroDecimal) {
                patronValidacionNumeroDecimal = new RegExp(this.patronNumeroDecimal);
            }
            else {
                if (posicionesDecimales && /\d+/.test(posicionesDecimales) && Number(posicionesDecimales) > 0) {
                    patronValidacionNumeroDecimal = new RegExp("^\\d+(\\.\\d{0," + posicionesDecimales + "}){0,1}$");
                }
                else {
                    patronValidacionNumeroDecimal = new RegExp("^\\d+(\\.\\d+){0,1}$");
                }
            }
            return patronValidacionNumeroDecimal.test(val);
        },
        valDecNeg: function (val, posicionesDecimales) {
            var patronValidacionNumeroDecimalNegativos;
            if (this.patronNumeroDecimalNegativos) {
                patronValidacionNumeroDecimalNegativos = new RegExp(this.patronNumeroDecimalNegativos);
            }
            else {
                if (posicionesDecimales && /\d+/.test(posicionesDecimales) && Number(posicionesDecimales) > 0) {
                    patronValidacionNumeroDecimalNegativos = new RegExp("^-{0,1}\\d+(\\.\\d{0," + posicionesDecimales + "}){0,1}$");
                }
                else {
                    patronValidacionNumeroDecimalNegativos = new RegExp("^-{0,1}\\d+(\\.\\d+){0,1}$");
                }
            }
            return patronValidacionNumeroDecimalNegativos.test(val);
        },
        valNumLet: function (val, longitudMinima) {
            if (!longitudMinima && longitudMinima !== 0) {
                longitudMinima = 1;
            }
            var numeroLetras = new RegExp("^[a-zA-Z0-9]{" + longitudMinima + ",}$");
            return numeroLetras.test($.trim(val));
        },
        valCorreo: function (val) {
            var correo = /^(([\w-\s]+)|([\w-]+(?:\.[\w-]+)*)|([\w-\s]+)([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i;
            return correo.test(val);
        },
        valUrl: function (val) {
            var url = /^((https?|ftp):\/\/){0,1}(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i;
            return url.test(val);
        },
        patronFecha: null,
        posicionesFecha: null,
        valFecha: function (val, oFecha) {
            var patronValidacionFecha;
            if (this.patronFecha) {
                patronValidacionFecha = new RegExp(this.patronFecha);
            }
            else {
                patronValidacionFecha = /^\d{1,2}\/\d{1,2}\/(\d{2}|\d{4})$/;
            }
            if (!patronValidacionFecha.test(val)) {
                if (oFecha) {
                    oFecha.esValida = false;
                }
                return false;
            }
            if (!this.posicionesFecha) {
                this.posicionesFecha = {
                    dia: 0,
                    mes: 1,
                    anio: 2
                };
            }
            var thatPosiciones = this.posicionesFecha;
            if (oFecha) {
                oFecha.AddDays = function (dias) {
                    var auxFecha = new Date(this.valor.getTime());
                    auxFecha = new Date(auxFecha.setDate(auxFecha.getDate() + dias));
                    var auxArreglo = [];
                    auxArreglo[thatPosiciones.dia] = auxFecha.getDate().toString();
                    auxArreglo[thatPosiciones.mes] = (auxFecha.getMonth() + 1).toString();
                    auxArreglo[thatPosiciones.anio] = auxFecha.getFullYear().toString();
                    return {
                        valorTexto: auxArreglo.join("/"),
                        valor: auxFecha,
                        valorInvariant: auxArreglo[thatPosiciones.mes] + "/" + auxArreglo[thatPosiciones.dia] + "/" + auxArreglo[thatPosiciones.anio],
                        esValida: true,
                        AddDays: this.AddDays
                    };
                };
            }
            try {
                var splitDate = val.split("/");
                var anio = Number(splitDate[this.posicionesFecha.anio]);
                var mes = Number(splitDate[this.posicionesFecha.mes]) - 1;
                var dia = Number(splitDate[this.posicionesFecha.dia]);
                var auxFecha = new Date(anio, mes, dia, 0, 0, 0, 0);
                if (auxFecha.getFullYear() === anio && auxFecha.getMonth() === mes && auxFecha.getDate() === dia) {
                    if (oFecha) {
                        oFecha.valorTexto = val;
                        oFecha.valor = auxFecha;
                        oFecha.valorInvariant = (mes + 1).toString() + "/" + dia.toString() + "/" + anio.toString();
                        oFecha.esValida = true;
                    }
                    return true;
                }
                else {
                    if (oFecha) {
                        oFecha.valorTexto = val;
                        oFecha.valor = auxFecha;
                        oFecha.valorInvariant = null;
                        oFecha.esValida = false;
                    }
                    return false;
                }
            }
            catch (e) {
                if (oFecha) {
                    oFecha.valorTexto = val;
                    oFecha.valor = null;
                    oFecha.valorInvariant = null;
                    oFecha.esValida = false;
                }
                return false;
            }
        },
        valHorasMinutos: function (val, hora) {
            var hoursMinutes = /^\d+:\d{2}$/;
            var ok = hoursMinutes.test(val);
            if (ok) {
                var index = val.indexOf(":", 0);
                if (hora) {
                    hora.horas = Number(val.substr(0, index));
                    hora.minutos = Number(val.substr(index + 1, 2));
                    hora.ticks = ((hora.horas * 60) + hora.minutos) * 60 * 10000000;
                    hora.valorTexto = val;
                }
            }
            return ok;
        },
        valHorasMinutosDia: function (val, hora) {
            var horasMinutos = /^\d{1,2}:\d{2}$/;
            var ok = horasMinutos.test(val);
            if (ok) {
                var indice = val.indexOf(":", 0);
                if (!hora) {
                    hora = {};
                }
                hora.horas = Number(val.substr(0, indice));
                hora.minutos = Number(val.substr(indice + 1, 2));
                hora.ticks = ((hora.horas * 60) + hora.minutos) * 60 * 10000000;
                hora.valorInvariant = '' + hora.horas + ':' + (hora.minutos <= 9 ? "0" : "") + hora.minutos + ":00";
                if (hora.minutos > 59 || hora.ticks > ((24 * 60 * 60) - 1) * 10000000) {
                    return false;
                }
            }
            return ok;
        },
        extraerHora: function (val) {
            var indice = val.indexOf(":", 0);
            var hora = Number(val.substr(0, indice));
            var minuto = Number(val.substr(indice + 1, 2));
            var ticks = ((hora * 60) + minuto) * 60 * 10000000;
            return {
                hora: hora,
                minuto: minuto,
                ticks: ticks
            };
        },
        mensaje: [],
        agregarLinea: function (texto) {
            this.mensaje.push(texto);
        },
        existeMensaje: function () {
            return this.mensaje && this.mensaje.length && this.mensaje.length > 0;
        },
        limpiarMensaje: function () {
            this.mensaje = [];
        },
        traerMensaje: function () {
            var that = this;
            function traerMensajeInterno(currentIndex) {
                if (that.mensaje.length <= currentIndex) {
                    return "";
                }
                else {
                    return that.mensaje[currentIndex] + "<br/>" + traerMensajeInterno(currentIndex + 1);
                }
            }
            return traerMensajeInterno(0);
        },
        armarMensaje: function (leyendaCabecera) {
            return this.armarMensajeArreglo(leyendaCabecera, this.mensaje);
        },
        armarMensajeArreglo: function (leyendaCabecera, arregloMensajes) {
            function traerMensajeInterno(currentIndex) {
                if (arregloMensajes.length <= currentIndex) {
                    return "";
                }
                else {
                    return "<tr><td style='vertical-align:top'>-</td><td style='vertical-align:top'>" + arregloMensajes[currentIndex] + "</td></tr>" + traerMensajeInterno(currentIndex + 1);
                }
            }
            return "<div style='padding-bottom:5px'>" + leyendaCabecera + "</div><table style='margin-left:5px'>" + traerMensajeInterno(0) + "</table>";
        },
        valElementoForms: function (jElemento, mensajes, validacionesComparativas) {
            var idElemento = jElemento.attr("id");
            var etiqueta = jElemento.dataset("eti") || idElemento;
            var etiquetaPlural = jElemento.dataset("etiplu") || (etiqueta + "s");
            var genero = jElemento.dataset("gen") || "M";
            if (genero !== "M" && genero !== "F") {
                genero = "M";
            }
            var permiteNulos = (jElemento.dataset("nul") || "N") === "S";
            var valorNulo = jElemento.dataset("vnu") || "";
            var tipo = jElemento.dataset("tip") || "";
            var menorOIgualQueElemento = jElemento.dataset("mie") || null;
            var mayorOIgualQueConstante = jElemento.dataset("mic") || null;
            var mayorOIgualQueConstanteEtiqueta = jElemento.dataset("mice") || null;
            var tamanoFijo = Number(jElemento.dataset("tfi")) || null;
            var tamanoFijoEtiqueta = jElemento.dataset("tfie") || "caracteres";
            var restriccionInicio = jElemento.dataset("rin") || null;
            if (restriccionInicio) {
                restriccionInicio = restriccionInicio.split(",");
            }
            var tipoNodo = jElemento.get(0).tagName.toLowerCase();

            var valor = $.trim(jElemento.val());
            var sufijoFemeninoUno = (genero === "F") ? "a" : "";
            var sufijoFemeninoValido = (genero === "F") ? "a" : "o";

            jElemento["valor"] = null;

            if ((tipoNodo === "input" || tipoNodo === "textarea") && tipo !== "aut" && tipo !== "autx") {
                if (!valor || valor === valorNulo) {
                    if (!permiteNulos) {
                        var mensajeNulo = jElemento.dataset("msgnul") || "Debe especificar un" + sufijoFemeninoUno + " " + etiqueta + ".";
                        mensajes.push(mensajeNulo);
                    }
                }
                else {
                    var mensajeInvalido = jElemento.dataset("msinv") || "Debe especificar un" + sufijoFemeninoUno + " " + etiqueta + " v\341lid" + sufijoFemeninoValido + ".";
                    switch (tipo) {
                        case "num": //número natural
                            if (!xpval.valNum(valor)) {
                                mensajes.push(mensajeInvalido);
                            }
                            else {
                                jElemento["valor"] = Number(valor);
                            }
                            break;
                        case "dec": // número decimal
                            if (!xpval.valDec(valor)) {
                                mensajes.push(mensajeInvalido);
                            }
                            else {
                                jElemento["valor"] = Number(valor);
                            }
                            break;
                        case "decn": // número decimal negativo
                            if (!xpval.valDecNeg(valor)) {
                                mensajes.push(mensajeInvalido);
                            }
                            else {
                                jElemento["valor"] = Number(valor);
                            }
                            break;
                        case "fec":
                            var oFecha = {};
                            if (!xpval.valFecha(valor, oFecha)) {
                                mensajes.push(mensajeInvalido);
                            }
                            else {
                                jElemento["valor"] = oFecha;
                            }
                            break;
                        case "hmd":
                            var oHora = {};
                            if (!xpval.valHorasMinutosDia(valor, oHora)) {
                                mensajes.push(mensajeInvalido);
                            }
                            else {
                                jElemento["valor"] = oHora;
                            }
                            break;
                        case "cor":
                            if (!xpval.valCorreo(valor)) {
                                mensajes.push(mensajeInvalido);
                            }
                            else {
                                jElemento["valor"] = valor;
                            }
                            break;
                        case "url":
                            if (!xpval.valUrl(valor)) {
                                mensajes.push(mensajeInvalido);
                            }
                            else {
                                jElemento["valor"] = valor;
                            }
                            break;
                        case "numlet":
                            if (!xpval.valNumLet(valor, 0)) {
                                mensajes.push(mensajeInvalido);
                            }
                            else {
                                jElemento["valor"] = valor;
                            }
                            break;
                        default:
                            jElemento["valor"] = valor;
                            break;
                    }
                    if (tamanoFijo && jElemento.valor && valor.length !== tamanoFijo) {
                        mensajes.push((genero === "F" ? "La " : "El ") + etiqueta + " debe tener exactamente " + tamanoFijo + " " + tamanoFijoEtiqueta + ".");
                        jElemento["valor"] = null;
                    }
                    if (restriccionInicio && jElemento.valor) {
                        var inicioEncontrado = false;
                        for (var i = 0; i < restriccionInicio.length; i++) {
                            if (valor.substring(0, restriccionInicio[i].length) === restriccionInicio[i]) {
                                inicioEncontrado = true;
                            }
                        }
                        if (!inicioEncontrado) {
                            mensajes.push((genero === "F" ? "La " : "El ") + etiqueta + " debe empezar con uno de los siguientes valores: " + restriccionInicio.join(", ") + ".");
                            jElemento["valor"] = null;
                        }
                    }
                    for (var i = 0; i < validacionesComparativas.length; i++) {
                        var jElementoPivote = validacionesComparativas[i].Pivote;
                        var tipoPivote = jElementoPivote.dataset("tip") || "";
                        var valorPivote = tipoPivote === "fec" ? (jElementoPivote.valor ? jElementoPivote.valor.valor : null) : jElementoPivote.valor;
                        var valorElemento = tipo === "fec" ? jElemento.valor.valor : jElemento.valor;
                        if (validacionesComparativas[i].IdComparativo === idElemento &&
                            (valorPivote || valorPivote === 0) && (valorElemento || valorElemento === 0) &&
                            valorPivote > valorElemento) {
                            var etiqueta2 = jElementoPivote.dataset("eti") || idElemento;
                            var genero2 = jElementoPivote.dataset("gen") === "F" ? "F" : "M";
                            mensajes.push(
                                (genero2 === "F" ? "La " : "El ") + etiqueta2 + " debe ser menor o igual " +
                                (genero === "F" ? "a la " : "al ") + etiqueta + "."
                                );
                            jElementoPivote["valor"] = null;
                        }
                    }
                    if (menorOIgualQueElemento) {
                        validacionesComparativas.push({
                            Pivote: jElemento,
                            IdComparativo: menorOIgualQueElemento,
                            Comparativo: null
                        });
                    }
                    if (mayorOIgualQueConstante) {
                        var valorElemento = tipo === "fec" ? jElemento.valor.valor : jElemento.valor;
                        if ((valorElemento || valorElemento === 0) &&
                            valorElemento < window[mayorOIgualQueConstante]) {
                            mensajes.push((genero === "F" ? "La " : "El ") + etiqueta + " debe ser igual o mayor a " + mayorOIgualQueConstanteEtiqueta + ".");
                            jElemento["valor"] = null;
                        }
                    }
                }
            }
            else if (tipoNodo === "input" && (tipo === "aut" || tipo === "autx")) {
                var valorActual = jElemento.dataset("valoractual");
                if (tipo === "aut" && valor && (valor !== jElemento.dataset("etiquetaanterior"))) {
                    var dataAutonumerico = window[jElemento.dataset("data")];
                    for (var i = 0; i < dataAutonumerico.length; i++) {
                        if (dataAutonumerico[i].label === valor) {
                            jElemento.dataset("valoractual", '' + dataAutonumerico[i].value);
                            jElemento.dataset("etiquetaanterior", dataAutonumerico[i].label);
                            valorActual = dataAutonumerico[i].value;
                        }
                    }
                }
                if (!valorActual || valorActual === valorNulo) {
                    if (!permiteNulos) {
                        var mensajeNulo = jElemento.dataset("msgnul") || "Debe especificar un" + sufijoFemeninoUno + " " + etiqueta + ".";
                        mensajes.push(mensajeNulo);
                    }
                }
                else {
                    jElemento["valor"] = valorActual;
                }
            }
            else if (tipoNodo === "select") {
                if (jElemento.prop("multiple")) {
                    valor = jElemento.val();
                }
                if (!valor || valor === valorNulo) {
                    if (!permiteNulos) {
                        var mensajeNulo = jElemento.dataset("msgnul") || "Debe seleccionar un" + sufijoFemeninoUno + " " + etiqueta + ".";
                        mensajes.push(mensajeNulo);
                    }
                }
                else {
                    jElemento["valor"] = valor;
                }
            }
            else if (tipoNodo === "td" && tipo === "rad") {
                var jSeleccionado = jElemento.find("input:checked");
                if (jSeleccionado.length === 0) {
                    if (!permiteNulos) {
                        var mensajeNulo = jElemento.dataset("msgnul") || "Debe seleccionar un" + sufijoFemeninoUno + " " + etiqueta + ".";
                        mensajes.push(mensajeNulo);
                    }
                }
                else {
                    jElemento["valor"] = jSeleccionado.val();
                }
            }
            else if (tipoNodo === "td" && tipo === "che") {
                var jSeleccionados = jElemento.find("input:checked");
                if (jSeleccionados.length === 0) {
                    if (!permiteNulos) {
                        var mensajeNulo = jElemento.dataset("msgnul") || "Debe seleccionar por lo menos un" + sufijoFemeninoUno + " " + etiqueta + ".";
                        mensajes.push(mensajeNulo);
                    }
                    else {
                        jElemento["valor"] = [];
                    }
                }
                else {
                    jElemento["valor"] = jSeleccionados.map(function () { return $(this).val(); }).get();
                }
            }
            else if (tipoNodo === "span" && tipo === "adj") {
                var archivo = jElemento.dataset("archivo");
                var modificado = jElemento.dataset("modificado") === "S";
                if (modificado && !archivo) {
                    if (!permiteNulos) {
                        var mensajeNulo = jElemento.dataset("msgnul") || "Debe adjuntar un" + sufijoFemeninoUno + " " + etiqueta + ".";
                        mensajes.push(mensajeNulo);
                    }
                }
                else {
                    jElemento["valor"] = {
                        Archivo: archivo,
                        Modificado: modificado
                    };
                }
            }
            else if (tipoNodo === "td" && (tipo === "lst_drop" || tipo === "lst_fec" || tipo === "lst_txb" || tipo === "lst_autx")) {
                var jItems = jElemento.find("div.frm_lst > div.item");
                if (jItems.length === 0) {
                    if (!permiteNulos) {
                        var mensajeNulo = jElemento.dataset("msgnul") || "Debe seleccionar por lo menos un" + sufijoFemeninoUno + " " + etiqueta + ".";
                        mensajes.push(mensajeNulo);
                    }
                    else {
                        jElemento["valor"] = [];
                    }
                }
                else {
                    if (tipo === "lst_drop" || tipo === "lst_autx") {
                        jElemento["valor"] = jItems.map(function () { return $(this).dataset("identificador"); }).get();
                    }
                    else if (tipo === "lst_fec") {
                        jElemento["valor"] = jItems.map(function () {
                            var auxFecha = {};
                            xpval.valFecha($(this).dataset("identificador"), auxFecha);
                            return auxFecha;
                        }).get();
                    }
                    else if (tipo === "lst_txb") {
                        var hayVacios = false;
                        jItems.each(function () {
                            if (!$.trim($(this).find("input").val())) {
                                hayVacios = true;
                            }
                        });
                        if (hayVacios) {
                            mensajes.push("Debe especificar un valor para " + (genero === "M" ? "todos los " : "todas las ") + etiquetaPlural + " " + (genero === "M" ? "agregados." : "agregadas."));
                        }
                        else {
                            jElemento["valor"] = jItems.map(function () { return $.trim($(this).find("input").val()); }).get();
                        }
                    }
                }
            }
            else if (tipoNodo === "td" && tipo === "bus") {
                var jItem = jElemento.find("span");
                var identificador = jItem.dataset("identificador");
                if (!identificador) {
                    if (!permiteNulos) {
                        var mensajeNulo = jElemento.dataset("msgnul") || "Debe seleccionar un" + sufijoFemeninoUno + " " + etiqueta + ".";
                        mensajes.push(mensajeNulo);
                    }
                }
                else {
                    jElemento["valor"] = identificador;
                }
            }
            else if (tipoNodo === "td" && tipo === "lst_adj") {
                var jItems = jElemento.find("div.frm_lst > div.item");
                if (jItems.length === 0) {
                    if (!permiteNulos) {
                        var mensajeNulo = jElemento.dataset("msgnul") || "Debe agregar por lo menos un" + sufijoFemeninoUno + " " + etiqueta + ".";
                        mensajes.push(mensajeNulo);
                    }
                    else {
                        jElemento["valor"] = [];
                    }
                }
                else {
                    var auxNombreRepetido = {};
                    var hayRepetidos = false;
                    jItems.each(function () {
                        var nombre = $.trim($(this).find("span").text());
                        if (auxNombreRepetido[nombre]) {
                            hayRepetidos = true;
                        }
                        else {
                            auxNombreRepetido[nombre] = true;
                        }
                    });
                    if (hayRepetidos) {
                        mensajes.push("Se ha agregado m\341s de un" + sufijoFemeninoUno + " " + etiqueta + " con el mismo nombre.");
                    }
                    else {
                        jElemento["valor"] = jItems.map(function () {
                            var jdivItem = $(this);
                            return {
                                Identificador: jdivItem.dataset("identificador") || null,
                                ArchivoReal: $.trim(jdivItem.find("span").text()),
                                ArchivoTemporal: jdivItem.dataset("archivotemporal") || null
                            };
                        }).get();
                    }
                }
            }
        },
        valForms: function (opciones) {
            /* OPCIONES:
            * contenedorInstancias:window
            * grupo:---
            * */
            opciones = opciones || {};

            //OPCIONES
            var contenedorInstancias = (opciones.contenedorInstancias || window);
            var grupoValidacion = (opciones.grupo || null);

            var elementos = contenedorInstancias.Elementos || [];
            var mensajes = [];

            $.each(elementos, function () {
                var lElementos = this;
                var mensajes2 = mensajes;
                var validacionesComparativas = [];
                $.each(lElementos, function () {
                    var jElemento = this;
                    if (grupoValidacion) {
                        if (!jElemento.dataset("grupo") || jElemento.dataset("grupo") !== grupoValidacion ||
                            jElemento.dataset("ign") === "S") {
                            return true;
                        }
                    }
                    var longitudAnterior = mensajes2.length;
                    xpectro.validacion.valElementoForms(jElemento, mensajes2, validacionesComparativas);
                });
            });
            return mensajes;
        }
    }, //FIN VALIDACIÓN
    fechas: { //MANIPULACIÓN DE FECHAS
        sumarAFecha: function (oFecha, intervalo, valor) {
            switch (intervalo) {
                case "Y": //año
                    oFecha = new Date(oFecha.getFullYear() + valor, oFecha.getMonth(), oFecha.getDate(), oFecha.getHours(),
                        oFecha.getMinutes(), oFecha.getSeconds(), oFecha.getMilliseconds());
                    break;
                case "M": //mes
                    var i = 1;
                    while (i <= valor) {
                        if (oFecha.getMonth() < 11) {
                            oFecha = new Date(oFecha.getFullYear(), oFecha.getMonth() + 1, oFecha.getDate(), oFecha.getHours(),
                                oFecha.getMinutes(), oFecha.getSeconds(), oFecha.getMilliseconds());
                        }
                        else {
                            oFecha = new Date(oFecha.getFullYear() + 1, 0, oFecha.getDate(), oFecha.getHours(),
                                oFecha.getMinutes(), oFecha.getSeconds(), oFecha.getMilliseconds());
                        }
                        i++;
                    }
                    break;
                default:
                    var milisegundosPorIntervalo = (1000 * 60 * 60 * 24); //por defecto: 1 día
                    switch (interval) {
                        case "W": //semana
                            milisegundosPorIntervalo = (1000 * 60 * 60 * 24 * 7); //7 días
                            break;
                        case "D": //día
                            milisegundosPorIntervalo = (1000 * 60 * 60 * 24); //1 día
                            break;
                        case "H": //hora
                            milisegundosPorIntervalo = (1000 * 60 * 60); //1 hora
                            break;
                        case "MI": //minuto
                            milisegundosPorIntervalo = (1000 * 60); //1 minuto
                            break;
                        case "S": //segundo
                            milisegundosPorIntervalo = (1000); //1 segundo
                            break;
                    }
                    oFecha = new Date(oFecha.getTime() + (milisegundosPorIntervalo * valor));
                    break;
            }
            return oFecha;
        },
        meses: [{ Largo: 'enero', Corto: 'ene' }, { Largo: 'febrero', Corto: 'feb' }, { Largo: 'marzo', Corto: 'mar' },
            { Largo: 'abril', Corto: 'abr' }, { Largo: 'mayo', Corto: 'may' }, { Largo: 'junio', Corto: 'jun' },
            { Largo: 'julio', Corto: 'jul' }, { Largo: 'agosto', Corto: 'ago' }, { Largo: 'septiembre', Corto: 'sep' },
            { Largo: 'octubre', Corto: 'oct' }, { Largo: 'noviembre', Corto: 'nov' }, { Largo: 'diciembre', Corto: 'dic' }],
        formatearFecha: function (oFecha, formato) {
            var that = xpectro.fechas;
            switch (formato) {
                case "mmmyy":
                    return that.meses[oFecha.getMonth()].Corto + ('' + oFecha.getFullYear()).substring(2);
                default:
                    return "";
            }
        },
        calcularPrimerDiaDelMes: function (oFecha) {
            return new Date(oFecha.getFullYear(), oFecha.getMonth(), 1, 0, 0, 0, 0);
        }
    } //FIN MANIPULACIÓN DE FECHAS
};

/***************************************
********** EXTENSIONES JQUERY **********
****************************************/

/************* FORMULARIOS *****************/
// Implementación de maxlength para text area
jQuery.fn.xpmaxlength = function () {
    var max = this.dataset("maxlength");
    if (max) {
        max = Number(max);
        this.keyup(function () {
            var jtxb = $(this);
            var valor = jtxb.val();
            var longitudActua = valor.length;
            if (longitudActua > max) {
                jtxb.val(valor.substr(0, max));
            }
        });
    }
    return this;
};
// Modularización datepicker
jQuery.fn.xpdatepicker = function () {
    if (this.length === 0) {
        return this;
    }
    this.change(function () {
        var jtxb = $(this);
        var patronValidacionFecha = new RegExp(/^\d{1,2}\/\d{1,2}\/(\d{2}|\d{2})$/);
        if (!patronValidacionFecha.test(jtxb.val())) {
            return;
        }
        var splitDate = jtxb.val().split("/");
        jtxb.val(splitDate[0] + "/" + splitDate[1] + "/20" + splitDate[2]);
    });
    this.each(function () {
        var jtxb = $(this);
        var anioInicial = jtxb.dataset("anioinicial") || "-10";
        var anioFinal = jtxb.dataset("aniofinal") || "+10";
        jtxb.datepicker({
            changeMonth: true,
            changeYear: true,
            showOn: 'button',
            buttonImage: xpglob.pathRaiz + 'css/img/fecha.png',
            buttonImageOnly: true,
            yearRange: anioInicial + ":" + anioFinal
        });
    });
    return this;
};
// Modularización de autocomplete
jQuery.fn.xpautocomplete = function (data, propiedadEtiqueta, propiedadValor, selectCallback) {
    if (this.length === 0) {
        return this;
    }
    data = data || window[this.dataset("data")];
    propiedadEtiqueta = propiedadEtiqueta || this.dataset("propeti") || "e";
    propiedadValor = propiedadValor || this.dataset("propval") || "v";
    for (var i = 0; i < data.length; i++) {
        data[i].value = data[i][propiedadValor];
        data[i].label = data[i][propiedadEtiqueta];
    }
    this.keyup(function () {
        var jtxb = $(this);
        if (jtxb.dataset("valoractual") && jtxb.val() !== jtxb.dataset("etiquetaanterior")) {
            jtxb.dataset("valoractual", null);
            jtxb.dataset("etiquetaanterior", null);
        }
    });
    this.autocomplete({
        minLength: 2,
        source: data,
        focus: function (event, ui) {
            $(this).val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            var jtxb = $(this);
            jtxb.dataset("valoractual", '' + ui.item.value);
            jtxb.val(ui.item.label).dataset("etiquetaanterior", ui.item.label);
            if (selectCallback) {
                selectCallback(ui.item.value, ui.item.label);
            }
            return false;
        }
    });
    return this;
}
jQuery.fn.xpautocompleteajax = function (nombreMetodo, propiedadEtiqueta, propiedadValor, selectCallback, limpiarCallback, searchCallback) {
    if (this.length === 0) {
        return this;
    }

    this.each(function () {
        var jtxb = $(this);

        var objetoContexto = jtxb.dataset("objetocontexto") ? window[jtxb.dataset("objetocontexto")] : window;
        var nombreMetodo = nombreMetodo || jtxb.dataset("metodo");
        var nombreServicio = jtxb.dataset("servicio") || null;
        var propiedadEtiqueta = propiedadEtiqueta || jtxb.dataset("propeti") || "e";
        var propiedadValor = propiedadValor || jtxb.dataset("propval") || "v";
        var selectCallback = selectCallback || (jtxb.dataset("select") && objetoContexto[jtxb.dataset("select")]);
        var limpiarCallback = limpiarCallback || (jtxb.dataset("limpiar") && objetoContexto[jtxb.dataset("limpiar")]);
        var searchCallback = searchCallback || (jtxb.dataset("search") && objetoContexto[jtxb.dataset("search")]);
        var sourceCallback = sourceCallback || (jtxb.dataset("source") && objetoContexto[jtxb.dataset("source")]);

        jtxb.prop("placeholder", jtxb.prop("placeholder") || "Escriba 3 letras para  buscar");

        jtxb.keyup(function () {
            var jtxb = $(this);
            if (jtxb.dataset("valoractual") && jtxb.val() !== jtxb.dataset("etiquetaanterior")) {
                jtxb.dataset("valoractual", null);
                jtxb.dataset("etiquetaanterior", null);
                jtxb.dataset("autxextra", null);
                if (limpiarCallback) {
                    limpiarCallback(jtxb);
                }
            }
        });

        jtxb.autocomplete({
            minLength: 3,
            source: function (request, response) {
                var propiedadEtiqueta2 = propiedadEtiqueta;
                var propiedadValor2 = propiedadValor;
                var dto = {
                    filtro: request.term
                };
                if (jtxb.dataset("parametros")) {
                    var lPars = JSON.parse(jtxb.dataset("parametros"));
                    for (var i = 0; i < lPars.length - 1; i += 2) {
                        dto[lPars[i]] = lPars[i + 1];
                    }
                }
                if (!nombreServicio) {
                    xpaj.pageMethod(nombreMetodo, dto, function (data, status) {
                        for (var i = 0; i < data.length; i++) {
                            data[i].value = data[i][propiedadValor2];
                            data[i].label = data[i][propiedadEtiqueta2];
                            data[i].extra = data[i]["x"];
                        }
                        response(data);
                        if (sourceCallback) {
                            sourceCallback(data);
                        }
                    });
                }
                else {
                    xpaj.webServiceMethod(nombreServicio, nombreMetodo, dto, function (data, status) {
                        for (var i = 0; i < data.length; i++) {
                            data[i].value = data[i][propiedadValor2];
                            data[i].label = data[i][propiedadEtiqueta2];
                            data[i].extra = data[i]["x"];
                        }
                        response(data);
                        if (sourceCallback) {
                            sourceCallback(data);
                        }
                    });
                }
            },
            focus: function (event, ui) {
                return false;
            },
            search: function (event, ui) {
                if (searchCallback) {
                    searchCallback($(this));
                }
                return true;
            },
            select: function (event, ui) {
                var jtxb = $(this);
                jtxb.dataset("valoractual", '' + ui.item.value);
                jtxb.val(ui.item.label).dataset("etiquetaanterior", ui.item.label);
                if (ui.item.extra) {
                    jtxb.dataset("autxextra", ui.item.extra);
                }
                if (selectCallback) {
                    selectCallback(ui.item.value, ui.item.label, jtxb);
                }
                return false;
            }
        });
    });
    return this;
}
jQuery.fn.xpcontrasena = function () {
    if (this.length === 0) {
        return this;
    }
    this.each(function () {
        var jtxb = $(this);
        var jdivContenedor = $("<div class='contenedorContrasena'><span class='mostrarContrasena' title='Mostrar contraseña'></span></div>")
        if (jtxb.dataset("anchocontenedor")) {
            jdivContenedor.css("width", jtxb.dataset("anchocontenedor"));
        }
        jtxb.parent().append(jdivContenedor);
        jtxb.insertBefore(jdivContenedor.find("span"));
        jdivContenedor.on("click", "span", function (evt) {
            var jspan = $(evt.target);
            if (jtxb.attr("type") === "password") {
                jtxb.removeAttr("type", "password");
                jtxb.prop("type", "text");
                jspan.removeAttr("title");
                jspan.prop("title", "Ocultar contraseña");
                jspan.removeClass("mostrarContrasena");
                jspan.addClass("ocultarContrasena");
            }
            else {
                jtxb.removeAttr("type", "text");
                jtxb.prop("type", "password");
                jspan.removeAttr("title");
                jspan.prop("title", "Mostrar contraseña");
                jspan.removeClass("ocultarContrasena");
                jspan.addClass("mostrarContrasena");
            }
            jtxb.focus().end();
        });
    });
    return this;
}
jQuery.fn.xpSumoSelect = function () {
    if (this.length === 0) {
        return this;
    }
    this.each(function () {
        var jddl = $(this);
        jddl.SumoSelect({
            placeholder: jddl.dataset("leyendaseleccion") || "Seleccione...",
            selectAll: true,
            selectAlltext: jddl.dataset("leyendatodos") || (jddl.dataset("gen") === "F" ? "Seleccionar todas" : "Seleccionar todos"),
            captionFormat: "{0} Seleccionados",
            triggerChangeCombined: true
        });
    });
    return this;
}
jQuery.fn.xpSumoSelectUnload = function () {
    if (this.length === 0) {
        return this;
    }
    this.each(function () {
        var jddl = $(this);
        if (jddl[0].sumo) {
            jddl[0].sumo.unload();
        }
    });
    return this;
}
// Validación de números naturales
jQuery.fn.xpnumeronatural = function () {
    return this.keydown(function (e) {
        var tecla = e.which || e.keyCode;
        if (!e.shiftKey && !e.altKey && !e.ctrlKey &&
            (
                (tecla >= 48 && tecla <= 57) || //números principal
                (tecla >= 96 && tecla <= 105) || //números derecha
                tecla === 8 || //borrar atrás
                tecla === 9 ||  //tab
                tecla === 13 || // enter
                tecla === 35 || //fin
                tecla === 36 || //inicio
                tecla === 37 || //izquierda
                tecla === 39 || //derecha
                tecla === 46 || //suprimir
                tecla === 45 //insertar
            ) || //TECLAS COMBINADAS
            (
                (e.shiftKey && tecla === 9) || //retroceder foco
                (e.ctrlKey && tecla === 67) || //copiar
                (e.ctrlKey && tecla === 86) || //pegar
                (e.shiftKey && tecla === 36) || //seleccionar todo inicio (SHIFT-INICIO)
                (e.shiftKey && tecla === 35) //seleccionar todo fin (SHIFT-FIN)
            )
            ) {
            return true;
        }
        return false;
    });
}
// Validación de números decimales
jQuery.fn.xpnumerodecimal = function (incluirNegativos) {
    this.dataset("xptip", "S");
    this.each(function (indice, elemento) {
        $(this).val($(this).val());
    });
    this.blur(function (evt) {
        $(this).val($(this).val());
    });
    return this.keydown(function (e) {
        var tecla = e.which || e.keyCode;
        if (!e.shiftKey && !e.altKey && !e.ctrlKey &&
            (
                (tecla >= 48 && tecla <= 57) || //números principal
                (tecla >= 96 && tecla <= 105) || //números derecha
                tecla === 190 || //punto centro
                tecla === 110 || //punto derecha
                tecla === 8 || //borrar atrás
                tecla === 9 || //tab
                tecla === 13 || // enter
                tecla === 35 || //fin
                tecla === 36 || //inicio
                tecla === 37 || //izquierda
                tecla === 39 || //derecha
                tecla === 46 || //suprimir
                tecla === 45 || //insertar
                (incluirNegativos && tecla === 109) || //menos centro
                (incluirNegativos && tecla === 189) //menos derecha
            ) || //TECLAS COMBINADAS
            (
                (e.shiftKey && tecla === 9) || //retroceder foco
                (e.ctrlKey && tecla === 67) || //copiar
                (e.ctrlKey && tecla === 86) || //pegar
                (e.shiftKey && tecla === 36) || //seleccionar todo inicio (SHIFT-INICIO)
                (e.shiftKey && tecla === 35) //seleccionar todo fin (SHIFT-FIN)
            )
            ) {
            return true;
        }
        return false;
    });
}
// Validación de caracteres alfanuméricos
jQuery.fn.xpnumeroletra = function () {
    return this.keydown(function (e) {
        var tecla = e.which || e.keyCode;
        if (!e.shiftKey && !e.altKey && !e.ctrlKey &&
            (
                (tecla >= 48 && tecla <= 57) || //números principal
                (tecla >= 96 && tecla <= 105) || //números derecha
                tecla === 8 || //borrar atrás
                tecla === 9 ||  //tab
                tecla === 13 || //enter
                tecla === 35 || //fin
                tecla === 36 || //inicio
                tecla === 37 || //izquierda
                tecla === 39 || //derecha
                tecla === 46 || //suprimir
                tecla === 45 || //insertar
                (tecla >= 65 && tecla <= 90) || //a-z
                tecla === 192 //ñ
            ) || //TECLAS COMBINADAS
            (
                (e.shiftKey && tecla === 9) || //retroceder foco
                (e.ctrlKey && tecla === 67) || //copiar
                (e.ctrlKey && tecla === 86) || //pegar
                (e.shiftKey && tecla === 36) || //seleccionar todo inicio (SHIFT-INICIO)
                (e.shiftKey && tecla === 35) || //seleccionar todo fin (SHIFT-FIN)
                (e.shiftKey && tecla >= 65 && tecla <= 90) //a-z mayúsculas con SHIFT
            )
            ) {
            return true;
        }
        return false;
    });
}
// Validación de horas y minutos
jQuery.fn.xphorasminutos = function () {
    return this.keydown(function (e) {
        var tecla = e.which || e.keyCode;
        if (//TECLAS SUELTAS
            (!e.shiftKey && !e.altKey && !e.ctrlKey &&
                (
                    (tecla >= 48 && tecla <= 57) || //números principal
                    (tecla >= 96 && tecla <= 105) || //números derecha
                    tecla === 8 || //borrar atrás
                    tecla === 9 || //tab
                    tecla === 13 || //enter
                    tecla === 35 || //fin
                    tecla === 36 || //inicio
                    tecla === 37 || //izquierda
                    tecla === 39 || //derecha
                    tecla === 46 || //suprimir
                    tecla === 45 //insertar
                )
            ) || //TECLAS COMBINADAS
            (
                (e.shiftKey && tecla === 9) || //retroceder foco
                (e.ctrlKey && tecla === 67) || //copiar
                (e.ctrlKey && tecla === 86) || //pegar
                (e.shiftKey && tecla === 36) || //seleccionar todo inicio (SHIFT-INICIO)
                (e.shiftKey && tecla === 35) || //seleccionar todo fin (SHIFT-FIN)
                (e.shiftKey && tecla === 190) //dos puntos: shift + punto

            )) {
            return true;
        }
        return false;
    });
}
// Encapsulación de fileuploader
jQuery.fn.xpadjuntos = function (opciones) {
    if (this.length === 0) {
        return this;
    }

    opciones = opciones || {};
    var urlSubirArchivo = opciones.UrlSubirArchivo || "subirarchivo.ashx";
    var extensionesPermitidas = opciones.ExtensionesPermitidas || ['jpg', 'png'];
    var carpetaTemporal = opciones.CarpetaTemporal || "temp";
    var limiteTamano = opciones.LimiteTamano || xpectro.global.limiteTamanoAdjuntos;

    for (var i = 0; i < this.length; i++) {
        var jEl = $(this.get(i));
        var modo = jEl.dataset("modo") || "e"; // e: externo (ventana aparte); i:interno (imagen)
        var ancho = Number(jEl.dataset("ancho")) || null;
        var alto = Number(jEl.dataset("alto")) || null;

        jEl.html("<div class='cmdcontext cont_up cargar'><a class='cmdcontint cargar'>Cargar</a></div>" +
            (modo === "e" ?
            (
                "<a class='nocmdtxt cargando espera16_gif' style='display:none'>Cargando...</a>" +
                "<a class='cmdtxt ver' target='_blank'>Ver</a>"
            ) : "") +
            "<a class='cmdtxt quitar'>Quitar</a>" +
            (modo === "i" ?
            (
                "<div style='clear: both;margin-top:3px'>" +
                    "<div class='imagen' style='" + (ancho ? "width: " + ancho + "px;" : "") + (alto ? "height: " + alto + "px;" : "") + "float: left; border: 1px solid #888; " +
                        "background-color: #f8f8f8'>SIN IMAGEN</div>" +
                "</div>"
            ) : ""));
        jEl.find("a.cargar").button({ icon: "ui-icon-arrowthickstop-1-n" });
        jEl.find("a.ver").button({ icon: "ui-icon-search" });
        jEl.find("a.quitar").button({ icon: "ui-icon-close" });
        jEl.dataset("modificado", "N");
        var direccion = jEl.dataset("direccion");
        if (direccion) {
            if (modo === "e") {
                jEl.find("a.cargar").button({ label: "Reemplazar" }).end()
                    .find("a.ver").attr("href", direccion);
            }
            else {
                jEl.find("a.cargar").button({ label: "Reemplazar" });
                jEl.find("div.imagen").html("<img src='" + direccion +
                    "' alt='' style='" + (ancho ? "width: " + ancho + "px;" : "") + (alto ? "height: " + alto + "px;" : "") + "' />");
            }
        }
        else {
            if (modo === "e") {
                jEl.find("a.ver").hide().end()
                    .find("a.quitar").hide();
            }
            else {
                jEl.find("a.quitar").hide().end()
                    .find("div.imagen").text("SIN IMAGEN");
            }
        }

        (function () {
            var jElEfectivo = jEl;
            var modoEfectivo = modo;
            var extensionesPermitidasEfectivas = jElEfectivo.dataset("ext") ? (jElEfectivo.dataset("ext") === "TODAS" ? [] : jElEfectivo.dataset("ext").split(",")) : extensionesPermitidas;
            var limiteTamanoEfectivo = jElEfectivo.dataset("lim") ? Number(jElEfectivo.dataset("lim")) : limiteTamano;
            var fu = new qq.FineUploaderBasic({
                button: jEl.find("div.cargar").get(0),
                request: {
                    endpoint: xpglob.pathRaiz + urlSubirArchivo
                },
                validation: {
                    allowedExtensions: extensionesPermitidasEfectivas,
                    sizeLimit: limiteTamanoEfectivo
                },
                callbacks: {
                    onSubmit: function (id, name) {
                        if (modoEfectivo === "e") {
                            jElEfectivo.find("div.cargar").hide().end()
                                .find("a.cargando").show().end()
                                .find("a.ver").hide().end()
                                .find("a.quitar").hide();
                        }
                        else {
                            jElEfectivo.find("div.cargar").attr("disabled", true).end()
                                .find("a.quitar").hide().end()
                                .find("div.imagen").html("<img src='" + xpglob.pathRaiz + "css/img/cargando.gif' alt='' style='margin: " +
                                    Math.round(((alto || 100) - 32) / 2) + "px 0px 0px " + Math.round(((ancho || 100) - 32) / 2) + "px' />");
                        }
                    },
                    onProgress: function (id, name, uploadedBytes, totalBytes) {
                        console.log("uploadedBytes: " + uploadedBytes);
                        console.log("totalBytes: " + totalBytes);
                    },
                    onComplete: function (id, name, responseJSON) {
                        jElEfectivo.dataset("archivo", responseJSON.archivo).dataset("modificado", "S");
                        if (modoEfectivo === "e") {
                            jElEfectivo.find("div.cargar").show().end()
                                .find("a.cargar").button({ label: "Reemplazar" }).end()
                                .find("a.cargando").hide().end()
                                .find("a.ver").attr("href", xpglob.pathRaiz + carpetaTemporal + "/" + responseJSON.archivo).show().end()
                                .find("a.quitar").show();
                        }
                        else {
                            jElEfectivo.find("div.cargar").attr("disabled", false).end()
                                .find("a.cargar").button({ label: "Reemplazar" }).end()
                                .find("a.quitar").show().end()
                                .find("div.imagen").html("<img src='" + xpglob.pathRaiz + carpetaTemporal + "/" + responseJSON.archivo +
                                    "' alt='' style='" + (ancho ? "width: " + ancho + "px;" : "") + (alto ? "height: " + alto + "px;" : "") + "' />");
                        }
                    },
                    onError: function (id, name, errorReason) {
                        if (xpdia) {
                            xpdia.showInfo("Error al subir", errorReason);
                        }
                        else {
                            alert(errorReason);
                        }
                    }
                }
            })

            jElEfectivo.find("a.quitar").click(function (evt) {
                jElEfectivo.dataset("modificado", "S")
                    .dataset("archivo", "");
                if (modoEfectivo === "e") {
                    jElEfectivo.find("div.cargar").show().end()
                        .find("a.cargar").button({ label: "Cargar" }).end()
                        .find("a.ver").hide().end()
                        .find("a.quitar").hide();
                }
                else {
                    jElEfectivo.find("div.cargar").attr("disabled", false).end()
                        .find("a.cargar").button({ label: "Cargar" }).end()
                        .find("a.quitar").hide().end()
                        .find("div.imagen").text("SIN IMAGEN");
                }
            });
        })();
    }
    return this;
}
// Procesamiento inicial de formularios
jQuery.fn.frm = function (opciones) {
    /* OPCIONES GENERALES
    * contenedorInstancias:window
    * clipboard:false (copiar al clipboard un string con las declaraciones de variables jquery instanciadas)
    * */
    opciones = opciones || {};

    //Opciones generales
    var contenedorInstancias = (opciones.contenedorInstancias || window);
    var clipboard = opciones.clipboard === true;

    if (!contenedorInstancias.Elementos) {
        contenedorInstancias.Elementos = [];
    }

    this.each(function () {
        var jtbl = $(this);
        if (jtbl.dataset("identificadorfrm")) {
            return true;
        }

        jtbl.find("td.oblig").append("<span class='obli'>&nbsp;*</span>");
        jtbl.find("span.oblig").append("<span class='obli'>&nbsp;*</span>");

        /* OPCIONES FORMULARIO
        * distribucion:true (borde, botón búsqueda & ENTER por defecto)
        * formatoTipos:true (formato diferenciado por tipo de datos)
        * crearInstancias:true (crea instancias jquery para cada element input,textarea,select que tenga id y la asigna al objeto contenedor
        * grupo: ---- (grupo de validación)
        * */
        var distribucion = jtbl.dataset("dis");
        distribucion = !distribucion || (distribucion === "S");
        var formatoTipos = jtbl.dataset("for");
        formatoTipos = !formatoTipos || (formatoTipos === "S");
        var crearInstancias = jtbl.dataset("ins");
        crearInstancias = !crearInstancias || (crearInstancias === "S");
        var crearTooltips = jtbl.dataset("tooltips");
        crearTooltips = !crearTooltips || (crearTooltips === "S");
        var grupoValidacion = jtbl.dataset("gru") || null;

        var jInputs = jtbl.find("input:not([data-ign])");
        var jTextAreas = jtbl.find("textarea:not([data-ign])");
        var jSelects = jtbl.find("select:not([data-ign])");
        var jAdjuntos = jtbl.find("span[data-tip=adj],div[data-tip=adj]").filter(":not([data-ign])");
        var jtds = jtbl.find("td[id]:not([data-ign])");
        var jtrs = jtbl.find("tr[id]:not([data-ign])");

        if (distribucion) {
            var idBtnBusqueda = jtbl.dataset("btnbusqueda");
            var jContFrm = $("<div class='frm_cont'></div>");
            jtbl.before(jContFrm);
            jtbl.appendTo(jContFrm);
            jtbl.find("tr:last td").css("border-bottom", "none");
            if (idBtnBusqueda) {
                var jbtn = $("#" + idBtnBusqueda);
                jtbl.on("keypress", "input:text,select,input:checkbox,input:radio", function (evt) {
                    if (evt.keyCode === 13) {
                        jbtn.trigger("click");
                        return false;
                    }
                });
                jtbl.on("enterwhenclosed", "select", function () {
                    jbtn.trigger("click");
                    return false;
                });

                //Para formularios de búsqueda, todos los campos deben permitir nulos por defecto;
                //entonces, se configura el attributo de nulidad en todos menos en los que ya lo tienen configurado
                jInputs.filter(":not([data-nul])").dataset("nul", "S");
                jTextAreas.filter(":not([data-nul])").dataset("nul", "S");
                jSelects.filter(":not([data-nul])").dataset("nul", "S");
                jAdjuntos.filter(":not([data-nul])").dataset("nul", "S");
                jtds.filter(":not([data-nul])").dataset("nul", "S");
            }
        }
        if (formatoTipos) {
            jtbl.formatearControles();

            //Listas dinámicas
            jtds.filter("[data-tip=che],[data-tip=rad]").addClass("lista");
            //jtds.filter("[data-tip=che]").find("input").checkboxradio();
            //jtds.filter("[data-tip=rad]").find("input").checkboxradio();

            jtds.filter("[data-tip=bus]").each(function () {
                var jtd = $(this);
                jtd.find("span").addClass("etiquetaBusquedaSeleccion").before("<a class='cmd seleccionar ver_png' style='vertical-align:bottom'></a>&nbsp;");
                var valorObjeto = jtd.dataset("obj");
                var objeto = null;
                if (valorObjeto) {
                    objeto = window[valorObjeto];
                    objeto.Inicializar(jtd);
                    jtd.find("a.cmd.seleccionar").click(function () {
                        objeto.IniciarSeleccion();
                    });
                }
            });
            //Lista drop down, fechas, textos
            jtds.filter("[data-tip=lst_drop],[data-tip=lst_fec],[data-tip=lst_txb],[data-tip=lst_autx]").each(function () {
                var jtd = $(this);
                var tipo = jtd.dataset("tip");
                var etiqueta = jtd.dataset("eti") || jtd.attr("id");
                var genero = jtd.dataset("gen") || "M";
                jtd.addClass("lista");
                var jbtnAgregar = $("");
                if (tipo !== "lst_drop" && tipo !== "lst_autx") {
                    jbtnAgregar = $("<a class='cmd agregar'>Agregar " + etiqueta + "</a>");
                    jbtnAgregar.button({ icon: "ui-icon-plusthick" });
                    jtd.append(jbtnAgregar);
                }
                var jdivContenedor = jtd.find("div.frm_lst");
                var identificadores = [];
                jdivContenedor.children("div.item").each(function () {
                    var jdivItem = $(this);
                    jdivItem.html("<a title='Quitar " + etiqueta + "' class='cmd quitar'>&nbsp;</a><span class='item_texto'>" +
                        $.trim(jdivItem.html()) + "</span>");
                    jdivItem.find("a.quitar").button({ icon: "ui-icon-close", showLabel: false });
                    if (jdivItem.dataset("identificador")) {
                        identificadores.push(jdivItem.dataset("identificador"));
                    }
                });

                var tipo = jtd.dataset("tip");

                //Específico para drop downs
                if (tipo === "lst_drop") {
                    var jSelect = jtd.find("select.ingreso");
                    for (var i = 0; i < identificadores.length; i++) {
                        jSelect.find("option[value=" + identificadores[i] + "]").remove();
                    }
                    jSelect.selectmenu({ width: null });

                    //Lógica para agregar
                    jSelect.on("selectmenuchange", function (evt) {
                        var valor = jSelect.val();
                        if (valor) {
                            var jdivItem = $("<div class='item' data-identificador=''><a title='' class='cmd quitar'>&nbsp;</a><span class='item_texto'></span></div>");
                            jdivItem.dataset("identificador", valor)
                                .find("a.quitar").attr("title", "Quitar " + etiqueta).end()
                                .find("span.item_texto").html(jSelect.find("option:selected").html());
                            jdivContenedor.append(jdivItem);
                            jdivItem.find("a.quitar").button({ icon: "ui-icon-close", showLabel: false });
                            jSelect.find("option:selected").remove();
                            jSelect.children("option").first().attr("selected", true);
                            jSelect.selectmenu("refresh");
                        }
                    });
                }
                else if (tipo === "lst_autx") {    //Específico para autocomplete ajax
                    var nombreMetodo = jtd.dataset("metodo");
                    var propiedadEtiqueta = jtd.dataset("propeti") || "e";
                    var propiedadValor = jtd.dataset("propval") || "v";
                    var searchCallback = searchCallback || (jtd.dataset("search") && window[jtd.dataset("search")]);
                    var jtxbIngreso = jtd.find("input.ingreso");
                    jtxbIngreso.addClass("frm_txb");
                    jtxbIngreso.prop("placeholder", jtxbIngreso.prop("placeholder") || "Escriba 3 letras para buscar");
                    jtxbIngreso.autocomplete({
                        minLength: 3,
                        source: function (request, response) {
                            var propiedadEtiqueta2 = propiedadEtiqueta;
                            var propiedadValor2 = propiedadValor;
                            var dto = {
                                filtro: request.term
                            };
                            if (jtd.dataset("parametros")) {
                                var lPars = JSON.parse(jtd.dataset("parametros"));
                                for (var i = 0; i < lPars.length - 1; i += 2) {
                                    dto[lPars[i]] = lPars[i + 1];
                                }
                            }
                            xpaj.pageMethod(nombreMetodo, dto, function (data, status) {
                                for (var i = 0; i < data.length; i++) {
                                    data[i].value = data[i][propiedadValor2];
                                    data[i].label = data[i][propiedadEtiqueta2];
                                }
                                response(data);
                            });
                        },
                        focus: function (event, ui) {
                            return false;
                        },
                        search: function (event, ui) {
                            if (searchCallback) {
                                searchCallback(jtd);
                            }
                            return true;
                        },
                        select: function (event, ui) {
                            var valor = ui.item.value;
                            if (valor && jdivContenedor.find("div[data-identificador=" + valor + "]").length === 0) {
                                var jdivItem = $("<div class='item' data-identificador=''><a title='' class='cmd quitar'>&nbsp;</a><span></span></div>");
                                jdivItem.dataset("identificador", valor)
                                    .find("a.quitar").attr("title", "Quitar " + etiqueta).end()
                                    .find("span").html(ui.item.label);
                                jdivItem.find("a.quitar").button({ icon: "ui-icon-close", showLabel: false });
                                jdivContenedor.append(jdivItem);
                            }
                            jtxbIngreso.val("");
                            return false;
                        }
                    });
                }
                else if (tipo === "lst_fec") {//Específico para fechas
                    var jtxbIngreso = jtd.find("input.ingreso");
                    jtxbIngreso.addClass("frm_txbFec").xpdatepicker();

                    //Lógica para agregar
                    jbtnAgregar.click(function (evt) {
                        var valor = $.trim(jtxbIngreso.val());
                        var oFecha = {};
                        if (valor && xpval.valFecha(valor, oFecha)) {
                            //Verificar si ya está agregado, para no agregarlo nuevamente
                            var yaExiste = false;
                            jdivContenedor.children("div.item").each(function () {
                                var oFechaItem = {};
                                xpval.valFecha($(this).dataset("identificador"), oFechaItem);
                                if (oFecha.valor.valueOf() === oFechaItem.valor.valueOf()) {
                                    yaExiste = true;
                                }
                            });
                            if (!yaExiste) {
                                var jdivItem = $("<div class='item' data-identificador=''><a title='' class='cmd quitar'>&nbsp;</a><span></span></div>");
                                jdivItem.dataset("identificador", valor)
                                .find("a.quitar").attr("title", "Quitar " + etiqueta).end()
                                .find("span").text(valor);
                                jdivContenedor.append(jdivItem);
                                jdivItem.find("a.quitar").button({ icon: "ui-icon-close", showLabel: false });
                                jtxbIngreso.val("");
                            }
                            else {
                                xpdia.showInfo("Agregar " + etiqueta, "La fecha especificada ya est\341 incluida.");
                            }
                        }
                        else {
                            xpdia.showInfo("Agregar " + etiqueta, "Debe especificar una fecha v\341lida.");
                        }
                    });
                }
                else if (tipo === "lst_txb") {//Específico para textos
                    var jtxbIngreso = jtd.find("input.ingreso");
                    //Lógica para agregar
                    jbtnAgregar.click(function (evt) {
                        var jdivItem = $("<div class='item'><a title='' class='cmd quitar eliminar_png'>&nbsp;</a></div>");
                        jdivItem.find("a.quitar").attr("title", "Quitar " + etiqueta).end()
                            .append(jtxbIngreso.clone().show());
                        jdivContenedor.append(jdivItem);
                        jdivItem.find("a.quitar").button({ icon: "ui-icon-close", showLabel: false });
                        jdivItem.find("input").focus();
                    });
                }

                jdivContenedor.delegate("a.cmd", "click", function (evt) {
                    var jcmd = $(evt.currentTarget);
                    if (jcmd.hasClass("quitar")) {
                        var jdivItem = jcmd.closest("div.item");
                        var identificador = jdivItem.dataset("identificador");
                        var textoHtml = jdivItem.find("span.item_texto").html();
                        jdivItem.remove();

                        //Específico para drop downs
                        if (tipo === "lst_drop") {
                            var jOption = $("<option></option>");
                            jOption.attr("value", identificador)
                                .html(textoHtml);
                            jtd.find("select.ingreso").append(jOption);
                            jtd.find("select.ingreso").selectmenu("refresh");
                        }
                    }
                });
            });
            jtds.filter("[data-tip=lst_adj]").each(function () {
                var jtd = $(this);

                //Parámetros configurados
                var etiqueta = jtd.dataset("eti") || jtd.attr("id");
                var genero = jtd.dataset("gen") || "M";
                var urlBajarCargado = jtd.dataset("urlbajar");
                var extensionesPermitidas = jtd.dataset("ext") ? jtd.dataset("ext").split(",") : [];
                var limiteTamano = jtd.dataset("lim") ? Number(jtd.dataset("lim")) : xpectro.global.limiteTamanoAdjuntos;
                var urlSubirArchivo = jtd.dataset("urlsubir") || "subirarchivo.ashx";

                //Formatear lista de adjuntos ya existente
                jtd.addClass("lista");
                var jdivContenedor = jtd.find("div.frm_lst");
                jdivContenedor.children("div.item")
                    .addClass("cargado")
                    .prepend("<a class='cmd quitar' title='Quitar'>&nbsp;</a>")
                    .prepend("<a class='cmd bajar' title='Descargar'>&nbsp;</a>");
                jdivContenedor.find("a.quitar").button({ icon: "ui-icon-close", showLabel: false });
                jdivContenedor.find("a.bajar").button({ icon: "ui-icon-arrowthickstop-1-s", showLabel: false });
                jdivContenedor.on("click", "a.cmd", function (evt) {
                    var jcmd = $(evt.currentTarget);
                    if (jcmd.hasClass("bajar")) {
                        var jdivItem = jcmd.closest("div.item");
                        if (jdivItem.hasClass("cargado")) {
                            xput.bajarArchivo(xpglob.pathRaiz + urlBajarCargado, "identificador=" + jdivItem.dataset("identificador"));
                        }
                        else {
                            xput.bajarArchivo(xpglob.pathRaiz + "bajartemporal.ashx",
                                "archivotemporal=" + jdivItem.dataset("archivotemporal") +
                                "&archivoreal=" + jdivItem.find("span").text());
                        }
                    }
                    else if (jcmd.hasClass("quitar")) {
                        jcmd.closest("div.item").remove();
                    }
                });

                //Botón para agregar nuevos adjuntos
                var jbtnAgregar = $("<div class='cmdcontext cont_up cargar'><a class='cmdcontint cargar'>Agregar " + etiqueta + "</a></div>");
                jbtnAgregar.button({ icon: "ui-icon-arrowthickstop-1-n" });
                jtd.append(jbtnAgregar);


                var fu = new qq.FineUploaderBasic({
                    button: jbtnAgregar.get(0),
                    request: {
                        endpoint: xpglob.pathRaiz + urlSubirArchivo
                    },
                    validation: {
                        allowedExtensions: extensionesPermitidas,
                        sizeLimit: limiteTamano
                    },
                    callbacks: {
                        onSubmit: function (id, name) {
                            var jdivNuevoItem = $("<div class='item'><a class='nocmdtxt cargando espera16_gif'>Cargando...</a></div>");
                            jdivNuevoItem.dataset("iduploader", id);
                            jdivContenedor.append(jdivNuevoItem);
                        },
                        onComplete: function (id, name, responseJSON) {
                            var jdivNuevoItem = jdivContenedor.find("div.item[data-iduploader=" + id + "]");
                            jdivNuevoItem.dataset("archivotemporal", responseJSON.archivo)
                                .html("")
                                .append("<a class='cmd bajar bajar_png' title='Descargar'>&nbsp;</a>")
                                .append("<a class='cmd quitar eliminar_png' title='Quitar'>&nbsp;</a>")
                                .append("&nbsp;<span></span>")
                                .find("span").text(name);
                            jdivNuevoItem.find("a.quitar").button({ icon: "ui-icon-close", showLabel: false });
                            jdivNuevoItem.find("a.bajar").button({ icon: "ui-icon-arrowthickstop-1-s", showLabel: false });
                        },
                        onError: function (id, name, errorReason, xhr) {
                            var jdivNuevoItem = jdivContenedor.find("div.item[data-iduploader=" + id + "]");
                            jdivNuevoItem.remove();
                            if (xpdia) {
                                xpdia.showInfo("Error al subir", errorReason);
                            }
                            else {
                                alert(errorReason);
                            }
                        }
                    }
                });
            });
        }
        var lElementos = [];
        if (crearInstancias) {
            jInputs.filter("[id]")
                .add(jTextAreas.filter("[id]"))
                .add(jSelects.filter("[id]"))
                .add(jAdjuntos.filter("[id]"))
                .add(jtds).add(jtrs).each(function () {
                    var jEl = $(this);
                    lElementos.push(jEl);
                    var variable = "j" + jEl.attr("id");
                    contenedorInstancias[variable] = jEl;
                    if (grupoValidacion) {
                        jEl.dataset("grupo", grupoValidacion);
                    }
                });
        }
        if (crearTooltips) {
            jtbl.find("span.ayuda").addClass("ayuda16_png").tooltip();
        }

        lElementos.sort(function (jNode1, jNode2) {
            var a = jNode1.get(0);
            var b = jNode2.get(0);
            var resultadoComparacion = a.compareDocumentPosition ?
                                            a.compareDocumentPosition(b) :
                                            a.contains ?
                                                (a !== b && a.contains(b) && 16) +
                                                (a !== b && b.contains(a) && 8) +
                                                (a.sourceIndex >= 0 && b.sourceIndex >= 0 ?
                                                    (a.sourceIndex < b.sourceIndex && 4) +
                                                    (a.sourceIndex > b.sourceIndex && 2) :
                                                    1) +
                                                0 :
                                                0;
            if (resultadoComparacion === 0) {
                return 0;
            }
            else if ((resultadoComparacion & 2) === 2) {
                return 1;
            }
            else if ((resultadoComparacion & 4) === 4) {
                return -1;
            }
            return 0;
        });
        var identificadorFormulario = xpectro.validacion.siguienteIdentificadorFormulario ? xpectro.validacion.siguienteIdentificadorFormulario : 1;
        xpectro.validacion.siguienteIdentificadorFormulario = identificadorFormulario + 1;
        lElementos.identificadorFormulario = identificadorFormulario;
        contenedorInstancias.Elementos.push(lElementos);
        jtbl.dataset("identificadorfrm", identificadorFormulario);
    });
    if (clipboard && window.clipboardData && contenedorInstancias.Elementos.length > 0) {
        var declaraciones = $.map(contenedorInstancias.Elementos, function (lElementos, indice) {
            return $.map(lElementos, function (jEl, indice2) {
                return "j" + jEl.attr("id");
            });
        });
        if (contenedorInstancias === window) {
            window.clipboardData.setData("Text", "var " + declaraciones.join(" = $(\"\");\nvar ") + " = $(\"\");");
        }
        else {
            window.clipboardData.setData("Text", "objetoContenedor = {\n\t" + declaraciones.join(" : $(\"\"),\n\t") + " : $(\"\")\n};");
        }
    }
    contenedorInstancias.Elementos = $.map(contenedorInstancias.Elementos, function (lElementos, indice) {
        if ($("table[data-identificadorfrm=" + lElementos.identificadorFormulario + "]").length == 0) {
            return;
        }
        return [lElementos];
    });
};

// Procesamiento inicial de grillas
jQuery.fn.grd = function (opciones) {
    this.each(function () {
        var jtbl = $(this);

        /* OPCIONES
        * formatoTipos:true (formato diferenciado por tipo de datos)
        * formatearTabla:false (estilos para filas de tabla, bordes redondeados de la tabla)
        * */
        var formatoTipos = (jtbl.dataset("for") || "S") === "S";
        var formatearTabla = (jtbl.dataset("fortab") || "S") === "S";
        var crearTooltips = (jtbl.dataset("tooltips") || "S") === "S";
        var configurarComandos = (jtbl.dataset("confbtn") || "S") === "S";

        var plantillaDetalle = jtbl.dataset("agrtmp");
        if (plantillaDetalle) {
            var cantidadColumnas = jtbl.dataset("agrcolspan");
            var columnaCorrelativo = jtbl.dataset("agrattrcorr") || "corr";
            var correlativoInicial = Number(jtbl.dataset("agrcorini")) || -1;
            var textoDetalle = jtbl.dataset("agrtxtdet") || "detalle";
            if (cantidadColumnas) {
                var jtfoot = jtbl.find("tfoot");
                if (jtfoot.length === 0) {
                    jtfoot = $("<tfoot></tfoot>");
                    jtbl.append(jtfoot);
                }
                jtfoot.append("<tr><td colspan='" + cantidadColumnas + "'><a class='cmd agregar'>Agregar " + textoDetalle + "</a></td></tr>");

                var jbtnAgregar = jtfoot.find("a.agregar");
                jbtnAgregar.button({ icon: "ui-icon-plusthick" });
            }
            var htmlDetalle = $.trim($("#" + plantillaDetalle).html());
            var jtbody = jtbl.children("tbody");
            jtbody.dataset("siguientecorrelativo", correlativoInicial);
            jtbl.delegate(".cmd.agregar", "click", function (evt) {
                var jtrAgregado = $(htmlDetalle);
                var siguienteCorrelativo = Number(jtbody.dataset("siguientecorrelativo"));
                jtrAgregado.dataset(columnaCorrelativo, siguienteCorrelativo);
                jtbody.dataset("siguientecorrelativo", siguienteCorrelativo - 1);
                if (formatoTipos) {
                    jtrAgregado.find("input").filter(":text").addClass("grd_txb")
                        .filter("[data-tip=fec]").addClass("grd_txbFec").xpdatepicker().end()
                        .filter("[data-tip=hmd]").xphorasminutos().end()
                        .filter("[data-tip=num]").xpnumeronatural().end()
                        .filter("[data-tip=dec]").xpnumerodecimal().end()
                        .filter("[data-tip=decn]").xpnumerodecimal(true).end()
                        .filter("[data-tip=autx]").xpautocompleteajax();
                    jtrAgregado.find("textarea").addClass("grd_txb").filter("[data-maxlength]").xpmaxlength();
                    jtrAgregado.find("select").addClass("grd_txb");
                }
                jtbody.append(jtrAgregado);
                if (formatearTabla) {
                    jtbody.formatearTabla();
                }
                jtbody.trigger("filaagregada", [jtrAgregado]);
            });
        }

        if (formatoTipos) {
            jtbl.find("input").filter(":text").addClass("grd_txb")
                .filter("[data-tip=fec]").addClass("grd_txbFec").xpdatepicker()
                .end().filter("[data-tip=hmd]").xphorasminutos()
                .end().filter("[data-tip=num]").xpnumeronatural()
                .end().filter("[data-tip=dec]").xpnumerodecimal()
                .end().filter("[data-tip=decn]").xpnumerodecimal(true)
                .end().filter("[data-tip=autx]").xpautocompleteajax();
            jtbl.find("textarea").addClass("grd_txb").filter("[data-maxlength]").xpmaxlength();
            jtbl.find("select").addClass("grd_txb");
        }

        if (formatearTabla) {
            jtbl.formatearTabla();
        }

        if (crearTooltips) {
            jtbl.find("span.ayuda").addClass("ayuda16_png").tooltip();
        }

        if (configurarComandos) {
            jtbl.find(".cmd").each(function () {
                var jcmd = $(this);
                var dto = {};
                if (jcmd.dataset("ico")) {
                    dto["icon"] = jcmd.dataset("ico");
                }
                if (!$.trim(jcmd.text())) {
                    dto["showLabel"] = false;
                }
                jcmd.button(dto);
            });
        }

        var cmdDefecto = jtbl.dataset("cmd");
        if (cmdDefecto) {
            var claseFilaPrincipal = jtbl.dataset("claseprincipal");
            jtbl.find("tbody").addClass("hvr");
            jtbl.on("click", "tbody > tr > td, tbody > tr > td > span", function (evt) {
                var jtarget = $(evt.target);
                if (jtarget.get(0).tagName.toLowerCase() !== "td" && jtarget.parent().get(0).tagName.toLowerCase()) {
                    return;
                }
                var jElemento = jtarget.closest("tr" + (claseFilaPrincipal ? "." + claseFilaPrincipal : "")).find(".cmd." + cmdDefecto + ",.nocmd." + cmdDefecto);
                if (jElemento.length > 0 && !jElemento.prop("disabled")) {
                    if (jElemento.attr("href") && jElemento.attr("href").indexOf("javascript:") === -1 && !jElemento.attr("target")) {
                        location.href = jElemento.attr("href");
                    }
                    else if (jElemento.is(":checkbox")) {
                        jElemento.prop("checked", !jElemento.prop("checked"));
                        jElemento.trigger("change");
                    }
                    else {
                        jElemento.trigger("click");
                    }
                }
                evt.stopPropagation();
                return false;
            });
        }
    });
    return this;
};

//Procesamiento de toolbars
jQuery.fn.tlb = function () {
    this.each(function () {
        var jtlb = $(this);
        var izquierda = jtlb.find("a:not(ul li a)").not(".izq").length === 0;
        if (izquierda) {
            jtlb.find("a:not(ul li a)").removeClass("izq");
            jtlb.css("text-align", "left");
        }
        jtlb.addClass("ui-widget-header ui-corner-all");
        jtlb.find("a:not(ul li a)").each(function () {
            var jbtn = $(this);
            jbtn.button({ icons: { primary: jbtn.dataset("ico") } });
        });
        jtlb.find(".buttonset").buttonset();
        if (izquierda && jtlb.is(":visible")) {
            setTimeout(function () {
                jtlb.hide().show();
            }, 100);
        }
        jtlb.find("a.opciones").each(function () {
            var jddl = $(this);
            var ulMenu = jddl.next("ul");
            ulMenu.css({ "position": "absolute", "text-align": "left" });
            ulMenu.find("li[data-ico]").each(function () {
                var jli = $(this);
                jli.find("a").prepend($("<span/>").addClass("ui-icon").addClass(jli.dataset("ico")));
            });
            jddl.button({ icons: { secondary: "ui-icon-triangle-1-s" } });
            jddl.click(function () {
                ulMenu.show().position({
                    my: "left top",
                    at: "left bottom",
                    of: this
                });
                $(document).one("click", function () {
                    ulMenu.hide();
                });
                return false;
            });
            ulMenu.hide().menu();
        });
    });
    return this;
};
//Formateo genérico de controles internos
jQuery.fn.formatearControles = function () {
    this.each(function () {
        var jcont = $(this);

        var jInputs = jcont.find("input:not([data-ign])");
        var jTextAreas = jcont.find("textarea:not([data-ign])");
        var jSelects = jcont.find("select:not([data-ign])");
        var jAdjuntos = jcont.find("span[data-tip=adj],div[data-tip=adj]").filter(":not([data-ign])");

        jInputs.filter(":text").addClass("frm_txb")
                .filter("[data-tip=fec]").addClass("frm_txbFec").xpdatepicker()
                .end().filter("[data-tip=hmd]").xphorasminutos()
                .end().filter("[data-tip=num]").xpnumeronatural()
                .end().filter("[data-tip=dec]").xpnumerodecimal()
                .end().filter("[data-tip=decn]").xpnumerodecimal(true)
                .end().filter("[data-tip=numlet]").xpnumeroletra()
                .end().filter("[data-tip=aut]").xpautocomplete()
                .end().filter("[data-tip=autx]").xpautocompleteajax();
        jInputs.filter("[type=password]").addClass("frm_txb").xpcontrasena();
        jTextAreas.addClass("frm_txb").filter("[data-maxlength]").xpmaxlength();
        jSelects.each(function () {
            var jddl = $(this);
            if (jddl.prop("multiple")) {
                jddl.xpSumoSelect();
            }
            else {
                if (!jddl.dataset("icon")) {
                    jddl.selectmenu({ width: null });
                } else {
                    jddl.iconselectmenu().iconselectmenu("menuWidget").addClass("ui-menu-icons avatar");
                }
                if (jddl.dataset("ancho")) {
                    jddl.next("span.ui-selectmenu-button").css("width", jddl.dataset("ancho"));
                }
            }
        });
        jAdjuntos.xpadjuntos();

        //Botones e íconos
        jcont.find(".cmd").each(function () {
            var jcmd = $(this);
            var dto = {};
            if (jcmd.dataset("ico")) {
                dto["icon"] = jcmd.dataset("ico");
            }
            if (!$.trim(jcmd.text())) {
                dto["showLabel"] = false;
            }
            jcmd.button(dto);
        });
    });
    return this;
};

/************* PAGINACIÓN *************/
// Con llamada ajax
jQuery.fn.pagGrid = function (cantidadRegistros, cantidadPaginas, paginaActual, funcionBusqueda, args) {
    var jtable = this.closest("table");
    var jthead = jtable.find("thead");
    if (jthead.length === 0) {
        return this;
    }
    var etiquetaSingular = jtable.dataset("eti") || "registro";
    var etiquetaPlural = jtable.dataset("etiplural") || (etiquetaSingular + "s");

    var jfila = $("<tr><td class='paginador'></td></tr>");
    var jcelda = jfila.find("td");
    jcelda.prop("colspan", jtable.dataset("col"));
    if (cantidadRegistros < xpectro.global.limiteResultadosBusqueda) {
        if (cantidadRegistros > 1) {
            jcelda.append("<div class='textoresultado'>Se encontraron " + cantidadRegistros + " " + etiquetaPlural + ".</div>")
        }
        else {
            jcelda.append("<div class='textoresultado'>Se encontró 1 " + etiquetaSingular + ".</div>")
        }
    }
    else {
        jcelda.append("<div class='textoresultado'>Se están mostrando los primeros " + cantidadRegistros + " " + etiquetaPlural + " encontrados.</div>")
    }
    jthead.prepend(jfila);

    if (cantidadPaginas > 1) {
        for (var iPagina = cantidadPaginas; iPagina >= 1; iPagina--) {
            var jpagina = $("<div class='pagina'>" + iPagina + "</div>");
            jpagina.dataset("pagina", iPagina);
            if (iPagina === paginaActual) {
                jpagina.addClass("actual");
            }
            else {
                jpagina.addClass("noactual");
            }
            jcelda.append(jpagina);
        }
    }

    if (funcionBusqueda) {
        jfila.on("click", "div.pagina.noactual", function (evt) {
            var paginaSeleccionada = Number($(evt.currentTarget).dataset("pagina"));
            if (!args || !args.length || args.length === 0) {
                args = [paginaSeleccionada];
            }
            else {
                args.push(paginaSeleccionada);
            }
            funcionBusqueda.apply(null, args);
        });
    }

    jtable.dataset("paginaactual", paginaActual);
    return this;
};

/************* VISUALIZACIÓN Y FORMATO ************/
// Inicio efecto búsqueda
jQuery.fn.inicioEsperaBusqueda = function (top, bottom) {
    var jdiv = $("<div class='esperaBusqueda'><img alt='' src='" + xpectro.global.pathRaiz + "css/img/cargando.gif' /></div>");
    if (top) {
        jdiv.css("paddingTop", top);
    }
    if (bottom) {
        jdiv.css("paddingBottom", bottom);
    }
    this.html("");
    this.append(jdiv);
    return this;
};
// Fin efecto búsqueda
jQuery.fn.finEsperaBusqueda = function () {
    this.children("div.esperaBusqueda").remove();
    return this;
};
// Configuar panel procesamiento
jQuery.fn.procesamiento = function (padding) {
    this.each(function () {
        var jdivContenedor = $(this);
        var jdiv = $("<div/>");
        jdiv.html("<img alt='' /></div><div class='titulo'>PROCESANDO...</div><div class='subtitulo'>Espere un momento por favor</div>");
        jdiv.find("img").attr("src", xpectro.global.pathRaiz + "img/procesamiento.gif");
        jdiv.addClass("procesamiento");
        if (padding) {
            jdiv.css("padding-top", padding).css("padding-bottom", padding);
        }
        jdivContenedor.html("");
        jdivContenedor.append(jdiv);
    });
    return this;
};
/* Formatear filas de una tabla (tbody) */
jQuery.fn.formatearTabla = function () {
    if (this.length > 0) {
        this.each(function () {
            //var jItem = $(this);
            //if (jItem.get(0).tagName.toLowerCase() !== "table" && jItem.get(0).tagName.toLowerCase() !== "body") {
            //    return;
            //}
            var jtbl = $(this).closest("table");
            var jtbody = jtbl.children("tbody");
            var aplicarEstilos = (jtbl.dataset("estilos") || "S") === "S";
            if (aplicarEstilos) {
                var claseFila1 = jtbl.dataset("fil1") || null;
                var claseFila2 = jtbl.dataset("fil2") || null;
                if (claseFila1) {
                    jtbody.children("tr." + claseFila1).removeClass("fil1 fil2").addClass("fil1");
                    jtbody.children("tr." + claseFila2).removeClass("fil1 fil2").addClass("fil2");
                }
                else {
                    jtbody.children("tr:even").removeClass("fil1 fil2").addClass("fil1");
                    jtbody.children("tr:odd").removeClass("fil1 fil2").addClass("fil2");
                }
            }
            var jtrs = jtbl.children("thead").find("tr").add(
                jtbl.children("tbody").children("tr")).add(
                jtbl.children("tfoot").children("tr"));
            jtrs.first().find("td:first").css({ "-moz-border-top-left-radius": "8px", "-webkit-border-top-left-radius": "8px", "border-top-left-radius": "8px" }).end()
                .find("td:last-child").css({ "-moz-border-top-right-radius": "8px", "-webkit-border-top-right-radius": "8px", "border-top-right-radius": "8px" });
            jtrs.last().find("td:first").css({ "-moz-border-bottom-left-radius": "8px", "-webkit-border-bottom-left-radius": "8px", "border-bottom-left-radius": "8px" }).end()
                .find("td:last-child").css({ "-moz-border-bottom-right-radius": "8px", "-webkit-border-bottom-right-radius": "8px", "border-bottom-right-radius": "8px" });
        });
    }
    return this;
};
/* Formatear ítems de una lista (div) */
jQuery.fn.formatearLista = function () {
    this.children("div.cue").children("div.item:even").removeClass("par impar").addClass("impar");
    this.children("div.cue").children("div.item:odd").removeClass("par impar").addClass("par");
};

/************* OTROS VARIOS ************/
/* Para facilitar el acceso a atributos personalizados según su definición en HTML 5 */
jQuery.fn.dataset = function (name, value) {
    var PREFIX = "data-";
    if (value !== undefined) {
        return this.attr(PREFIX + name, value);
    }
    if (this.length == 0) {
        return null;
    }
    if (this[0].dataset) {
        return this[0].dataset[name];
    }
    return this.attr(PREFIX + name);
};
/* Formatear selectmenu con iconos*/
jQuery.widget("custom.iconselectmenu", $.ui.selectmenu, {
    _renderItem: function (ul, item) {
        var li = $("<li>"),
            wrapper = $("<div>", { text: item.label });

        if (item.disabled) {
            li.addClass("ui-state-disabled");
        }
        if (item.value) {
            $("<span>", {
                style: item.element.attr("data-style"),
                "class": "ui-icon " + (item.element.attr("data-ico") || "")
            }).appendTo(wrapper);
        }
        return li.append(wrapper).appendTo(ul);
    }
});

/* Para configurar un botón que suba archivos, fuera de un formulario*/
jQuery.fn.xpadjuntosnoform = function (extensiones, onSubmitCallback, onCompleteCallback) {
    this.each(function () {
        var jbtn = $(this);
        var fu = new qq.FileUploaderBasic({
            action: xpglob.pathRaiz + "subirarchivo.ashx",
            allowedExtensions: extensiones || ['xlsx'],
            onSubmit: function (id, name) {
                onSubmitCallback(jbtn, id, name);
            },
            onComplete: function (id, name, responseJSON) {
                onCompleteCallback(jbtn, id, name, responseJSON);
            }
        });
        fu._button = fu._createUploadButton(this);
        this.fileUploader = fu;
    });
    return this;
}

//Para configurar botones con íconos
jQuery.fn.xpfiltro = function (metodo) {
    this.each(function () {
        var jcontenedor = $(this);
        var jFiltroSimple = jcontenedor.children("div.simple");
        var jFiltroAvanzado = jcontenedor.children("div.avanzado");

        var recalcularModo = function () {
            var modo = jcontenedor.dataset("modo") || "simple";
            jcontenedor.dataset("modo", modo);
            jcontenedor.removeClass("simple").removeClass("avanzado").addClass(modo);
            if (modo === "simple") {
                jFiltroSimple.find(".focodefecto").first().focus();
            }
            else {
                jFiltroAvanzado.find(".focodefecto").first().focus();
            }
        };
        if (!metodo) {

            jcontenedor.find(".cmd").each(function () {
                var jcmd = $(this);
                var dto = {};
                if (jcmd.dataset("ico")) {
                    dto["icon"] = jcmd.dataset("ico");
                }
                if (!$.trim(jcmd.text())) {
                    dto["showLabel"] = false;
                }
                jcmd.button(dto);
            });

            recalcularModo();

            jFiltroSimple.find(".cmd.busquedaavanzada").click(function (evt) {
                jcontenedor.removeClass("simple").addClass("avanzado");
                jcontenedor.dataset("modo", "avanzado");
                jFiltroAvanzado.find(".focodefecto").first().focus();
            });
            jFiltroAvanzado.find(".cmd.busquedasimple").click(function (evt) {
                jcontenedor.removeClass("avanzado").addClass("simple");
                jcontenedor.dataset("modo", "simple");
                jFiltroSimple.find(".focodefecto").first().focus();
            });

            if (jFiltroSimple.dataset("busqueda")) {
                jFiltroSimple.find("input:text").keypress(function (evt) {
                    if (evt.keyCode === 13) {
                        window[jFiltroSimple.dataset("busqueda")]();
                        return false;
                    }
                });
                jFiltroSimple.find("select").change(function (evt) {
                    window[jFiltroSimple.dataset("busqueda")]();
                    return false;
                });
            }
        }
        if (metodo === "recalcularmodo") {
            recalcularModo();
        }
    });
    return this;
}
jQuery.fn.xperror = function (metodo, opciones) {
    this.each(function () {
        var jcontenedor = $(this);

        if (!jcontenedor.dataset("configurado")) {
            jcontenedor.addClass("ui-widget").css("display", "none");
            jcontenedor.html("<div class='ui-state-highlight ui-corner-all' style='padding: 10px'><span class='ui-icon ui-icon-info' style='float: left; margin-right: 5px;'></span><span class='mensaje'></span></div>");
            jcontenedor.dataset("configurado", "S");
        }
        if (metodo === "mostrar") {
            var yaVisible = jcontenedor.dataset("visible") === "S";
            jcontenedor.dataset("visible", "S");
            jcontenedor.find("span.mensaje").html(opciones ? (opciones.mensaje || "") : "");
            if (!yaVisible) {
                jcontenedor.show("slide", {}, 500);
            }
            else {
                jcontenedor.effect("pulsate", {}, 500);
            }
        }
        if (metodo === "ocultar") {
            jcontenedor.dataset("visible", "N");
            jcontenedor.find("span.mensaje").html("");
            jcontenedor.hide("slide", {}, 500);
        }
    });
    return this;
}
jQuery.fn.xpexito = function (metodo, opciones) {
    this.each(function () {
        var jcontenedor = $(this);

        if (!jcontenedor.dataset("configurado")) {
            jcontenedor.addClass("ui-widget").css("display", "none").css("margin-top", "5px");
            jcontenedor.html("<div class='ui-state-success ui-corner-all' style='padding: 10px'><span class='ui-icon ui-icon-check' style='float: left; margin-right: 5px;'></span><span class='mensaje'></span></div>");
            jcontenedor.dataset("configurado", "S");
        }
        if (metodo === "mostrar") {
            var yaVisible = jcontenedor.dataset("visible") === "S";
            jcontenedor.dataset("visible", "S");
            jcontenedor.find("span.mensaje").html(opciones ? (opciones.mensaje || "") : "");
            if (!yaVisible) {
                jcontenedor.show("slide", {}, 500);
            }
            else {
                jcontenedor.effect("pulsate", {}, 500);
            }
        }
        if (metodo === "ocultar") {
            jcontenedor.dataset("visible", "N");
            jcontenedor.find("span.mensaje").html("");
            jcontenedor.hide("slide", {}, 500);
        }
    });
    return this;
}

/*EXTENSIONES JAVASCRIPT*/
if (!Number.prototype.oldToFixed) {
    Number.prototype.oldToFixed = Number.prototype.toFixed;
    Number.prototype.toFixed = function (fractionDigits) {
        nStr = Number.prototype.oldToFixed.call(this, fractionDigits);
        var x = nStr.split('.');
        var x1 = x[0];
        var x2 = x.length > 1 ? xpectro.validacion.separadorDecimales + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + xpectro.validacion.separadorMiles + '$2');
        }
        return x1 + x2;
    };
}
if (!Number.prototype.toFixed2) {
    Number.prototype.toFixed2 = function () {
        nStr = this.toString();
        var x = nStr.split('.');
        var x1 = x[0];
        var x2 = x.length > 1 ? xpectro.validacion.separadorDecimales + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + xpectro.validacion.separadorMiles + '$2');
        }
        return x1 + x2;
    };
}
xpectro.originalVal = $.fn.val;
$.fn.val = function (value) {
    if (typeof value == 'undefined') {
        if (this.dataset("tip") === "dec" && this.dataset("xptip")) {
            return xpectro.originalVal.call(this).replace(/,/g, "");
        }
        else {
            return xpectro.originalVal.call(this);
        }
    }
    else {
        if (this.dataset("tip") === "dec" && this.dataset("xptip")) {
            var valorDecimal = Number(value);
            if (valorDecimal || valorDecimal === 0) {
                return xpectro.originalVal.call(this, valorDecimal.toFixed2());
            }
            else {
                return xpectro.originalVal.call(this, value);
            }
        }
        else {
            return xpectro.originalVal.call(this, value);
        }
    }
};

/* NOMBRES CORTOS */
var xpglob = xpectro.global;
var xpdia = xpectro.dialogos;
var xpaj = xpectro.ajax;
var xpval = xpectro.validacion;
var xput = xpectro.utils;