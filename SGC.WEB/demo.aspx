<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="demo.aspx.cs" Inherits="NUT.WEB.demo" %>

<%@ MasterType VirtualPath="~/Plantilla.Master" %>
<asp:Content ID="cCss" ContentPlaceHolderID="cCss" runat="server">
</asp:Content>
<asp:Content ID="cLey" ContentPlaceHolderID="cLey" runat="server">
    Esta es una de ejemplo mostrando los distintos tipos de controles para ingreso, formato y validación de datos, para formularios y listas. Se
    muestra en <strong><em>cursiva</em></strong> comentarios asociados a cada ejemplo (no es algo a incluir en las páginas reales).
</asp:Content>
<asp:Content ID="c" ContentPlaceHolderID="c" runat="server">
    <div class="toolbar">
        <a class="btn guardar" data-ico="ui-icon-disk">Guardar</a>
        <a data-ico="ui-icon-document">Nuevo</a>
        <a data-ico="ui-icon-cancel">Cancelar</a>
        <a data-ico="ui-icon-signal-diag">Exportar</a>
    </div>
    <div class="panel" id="pnlValoresUnicos">
        <h3>Valores únicos</h3>
        <table class="frm" data-gru="1">
            <tr>
                <td class="oblig" style="width: 100px">Nombre de la<br />
                    empresa</td>
                <td>
                    <input type="text" id="txbNombre" maxlength="100" style="width: 300px" data-eti="nombre" />
                    <br />
                    <em>Texto normal, obligatorio</em>
                </td>
            </tr>
            <tr>
                <td>Dirección
                </td>
                <td>
                    <textarea id="txbDireccion" rows="3" cols="0" data-maxlength="20" style="width: 350px" data-nul="S"></textarea>
                    <br />
                    <em>Texto amplio, límite 20 caracteres, opcional</em>
                </td>
            </tr>
            <tr>
                <td class="oblig">País</td>
                <td>
                    <select id="ddlPais" style="width: 304px" data-eti="país">
                        <option value="">&lt;Seleccione un país&gt;</option>
                        <option value="1">Argentina</option>
                        <option value="2">Bolivia</option>
                        <option value="3">Perú</option>
                    </select>
                    <br />
                    <em>Lista normal, obligatorio</em>
                </td>
            </tr>
            <tr>
                <td class="oblig">Patrimonio</td>
                <td>
                    <input type="text" id="txbPatrimonio" maxlength="10" style="width: 100px" data-tip="dec"
                        data-eti="patrimonio" />
                    <br />
                    <em>Número decimal, obligatorio</em>
                </td>
            </tr>
            <tr>
                <td class="oblig">Saldo</td>
                <td>
                    <input type="text" id="txbSaldo" maxlength="11" style="width: 100px" data-tip="decn"
                        data-eti="saldo" />
                    <br />
                    <em>Número decimal que permite NEGATIVO, obligatorio</em>
                </td>
            </tr>
            <tr>
                <td class="oblig">Hijos</td>
                <td>
                    <input type="text" id="txbHijos" maxlength="2" style="width: 100px" data-tip="num"
                        data-eti="cantidad de hijos" data-gen="F" />
                    <br />
                    <em>Número entero, obligatorio</em>
                </td>
            </tr>
            <tr>
                <td class="oblig">Correo</td>
                <td>
                    <input type="text" id="txbCorreo" maxlength="100" style="width: 300px" data-tip="cor"
                        data-eti="correo" />
                    <br />
                    <em>Correo electrónico, obligatorio</em>
                </td>
            </tr>
            <tr>
                <td>Sitio web</td>
                <td>
                    <input type="text" id="txbSitioWeb" maxlength="100" style="width: 300px" data-tip="url"
                        data-eti="sitio web" data-nul="S" data-vnu="http://" />
                    <br />
                    <em>Url, opcional, valor nulo "http://"</em>
                </td>
            </tr>
            <tr>
                <td class="oblig">Nombre corto</td>
                <td>
                    <input type="text" id="txbNombreCorto" maxlength="100" style="width: 300px" data-tip="numlet"
                        data-eti="nombre corto" />
                    <br />
                    <em>Números y letras, obligatorio</em>
                </td>
            </tr>
            <tr>
                <td class="oblig">Supervisor</td>
                <td>
                    <input type="text" id="txbSupervisor" maxlength="100" style="width: 300px" data-tip="aut"
                        data-eti="supervisor" data-data="lSupervisores" />
                    <br />
                    <em>Autocomplete cliente, origen de datos en variable<br />
                        "lSupervisores"</em>
                </td>
            </tr>
            <tr>
                <td class="oblig">Dependiente</td>
                <td>
                    <input type="text" id="txbSupervisado" maxlength="100" style="width: 300px" data-tip="autx"
                        data-metodo="FiltrarDependientes" data-eti="dependiente" />
                    <br />
                    <em>Autocomplete AJAX, origen de datos en método web<br />
                        "FiltrarDependiente"</em>
                </td>
            </tr>
            <tr>
                <td class="oblig">Hora</td>
                <td>
                    <input type="text" id="txbHora" maxlength="5" style="width: 45px" data-tip="hmd"
                        data-eti="hora" data-gen="F" />
                    <br />
                    <em>Hora (h:m)</em>
                </td>
            </tr>
            <tr>
                <td class="oblig">Fecha de<br />
                    fundación</td>
                <td>
                    <input type="text" id="txbFechaFundacion" maxlength="10" data-tip="fec" data-eti="fecha de fundación"
                        data-gen="F" />
                    <br />
                    <em>Fecha, obligatorio</em>
                </td>
            </tr>
        </table>
    </div>
    <div class="panel" id="pnlValoresMultiples">
        <h3>Valores múltiples</h3>
        <table class="frm" data-gru="1">
            <tbody>
                <tr>
                    <td class="oblig">Ciudad</td>
                    <td>
                        <select id="ddlCiudad" data-eti="ciudad" multiple="multiple" data-leyendaseleccion="Seleccione una ciudad" data-gen="F">
                            <option value="1">Cobija</option>
                            <option value="2">Cochabamba</option>
                            <option value="3">La Paz</option>
                            <option value="4">Oruro</option>
                            <option value="5">Potosí</option>
                            <option value="6">Santa Cruz</option>
                            <option value="7">Sucre</option>
                            <option value="8">Tarija</option>
                            <option value="9">Trinidad</option>
                        </select>
                        <br />
                        <em>Lista selección múltiple, obligatorio</em>
                    </td>
                </tr>
                <tr>
                    <td style="width: 70px" class="oblig">Estado civil</td>
                    <td id="tdEstadoCivil" data-tip="rad" data-eti="estado civil">
                        <asp:listview runat="server" id="lvwEstadosCiviles">
                            <ItemTemplate>
                                <label>
                                    <input type="radio" name="estadocivil" value="<%#Eval("val")%>" /><%#Eval("des")%>
                                </label>
                            </ItemTemplate>
                        </asp:listview>
                    </td>
                </tr>
                <tr>
                    <td class="oblig">Pasatiempos</td>
                    <td id="tdPasatiempos" data-tip="che" data-eti="pasatiempo">
                        <asp:listview runat="server" id="lvwPasatiempos">
                            <ItemTemplate>
                                <label>
                                    <input type="checkbox" value="<%#Eval("val")%>" /><%#Eval("des")%>
                                </label>
                                <br />
                            </ItemTemplate>
                        </asp:listview>
                    </td>
                </tr>
                <tr>
                    <td class="oblig">Comidas</td>
                    <td id="tdComidas" data-tip="lst_drop" data-eti="comida" data-gen="F">
                        <div class="frm_lst">
                            <asp:listview runat="server" id="lvwComidas">
                                <ItemTemplate>
                                    <div class="item" data-identificador="<%#Eval("val")%>">
                                        <%#Eval("des") %>
                                    </div>
                                </ItemTemplate>
                            </asp:listview>
                        </div>
                        <select id="ddlComidaAgregar" data-ign="S" class="ingreso">
                            <option value="">&lt;SELECCIONE&gt;</option>
                            <option value="4">Queperí</option>
                            <option value="5">Pique macho</option>
                            <option value="6">Feijoada</option>
                            <option value="7">Pacumuto</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="oblig">Feriados</td>
                    <td id="tdFeriados" data-tip="lst_fec" data-eti="feriado">
                        <div class="frm_lst">
                            <asp:listview runat="server" id="lvwFeriados">
                                <ItemTemplate>
                                    <div class="item" data-identificador="<%#Eval("val")%>">
                                        <%#Eval("des") %>
                                    </div>
                                </ItemTemplate>
                            </asp:listview>
                        </div>
                        <input type="text" maxlength="10" data-ign="S" class="ingreso" />
                    </td>
                </tr>
                <tr>
                    <td class="oblig">Respuestas</td>
                    <td id="tdRespuestas" data-tip="lst_txb" data-eti="respuesta" data-gen="F">
                        <div class="frm_lst">
                            <asp:listview runat="server" id="lvwRespuestas">
                                <ItemTemplate>
                                    <div class="item">
                                        <input type="text" maxlength="50" style="width: 150px" value="<%#Eval("des")%>" /><br />
                                    </div>
                                </ItemTemplate>
                            </asp:listview>
                        </div>
                        <input type="text" maxlength="50" style="width: 150px; display: none" class="ingreso" />
                    </td>
                </tr>
                <tr>
                    <td class="oblig">Personas</td>
                    <td id="tdPersonas" data-tip="lst_autx" data-eti="persona" data-gen="F" data-metodo="FiltrarDependientes">
                        <div class="frm_lst">
                            <asp:listview runat="server" id="lvwPersonas">
                                <ItemTemplate>
                                    <div class="item" data-identificador="<%#Eval("val")%>"><%#Eval("des")%></div>
                                </ItemTemplate>
                            </asp:listview>
                        </div>
                        <input type="text" maxlength="50" style="width: 200px" class="ingreso" />
                    </td>
                </tr>
                <tr>
                    <td class="oblig">Varios adjuntos</td>
                    <td id="tdAdjuntos" data-tip="lst_adj" data-eti="adjunto" data-urlbajar="bajaradjunto.ashx">
                        <div class="frm_lst">
                            <asp:listview runat="server" id="lvwAdjuntos">
                                <ItemTemplate>
                                    <div class="item" data-identificador="<%#Eval("val")%>">
                                        <span><%#Eval("des")%></span>
                                    </div>
                                </ItemTemplate>
                            </asp:listview>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="panel" style="width: 350px">
        <h3>Dependencias complejas</h3>
        <table class="frm" data-gru="1">
            <tbody>
                <tr>
                    <td>Gerencia</td>
                    <td>
                        <select runat="server" id="ddlGerencia" multiple="true" data-leyendaseleccion="Seleccione una o varias gerencias" datatextfield="des" datavaluefield="val">
                        </select>
                        <br />
                        <em>No depende de ningún otro filtro</em>
                    </td>
                </tr>
                <tr>
                    <td>Jefatura</td>
                    <td>
                        <span id="lblJefatura" style="line-height: 26px">Seleccione primero uno o varias gerencias</span>
                        <select id="ddlJefatura" multiple="multiple" data-leyendaseleccion="Seleccione una o varias jefaturas" style="display: none"></select>
                        <br />
                        <em>Los datos se cargan en función a las gerencias seleccionadas</em>
                    </td>
                </tr>
                <tr>
                    <td>Empleados
                    </td>
                    <td>
                        <input type="text" id="txbEmpleados" maxlength="100" style="width: 220px" data-tip="autx"
                            data-metodo="FiltrarEmpleados" data-search="LeerParametrosEmpleados" data-eti="ítem" />
                        <br />
                        <em>Las gerencias y jefaturas seleccionadas se pasan como parámetros al método ajax que realiza la búsqueda</em>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="panel" id="pnlArchivos">
        <h3>Adjuntos y archivos</h3>
        <table class="frm" data-gru="1">
            <tbody>
                <tr>
                    <td>Adjunto automático externo
                    </td>
                    <td>
                        <span id="contAdjuntoAutomaticoExterno" data-direccion="/css/img/cargando2.gif" data-tip="adj"
                            data-eti="logo automático externo"></span>
                    </td>
                </tr>
                <tr>
                    <td>Logo automático interno
                    </td>
                    <td>
                        <span id="contLogoAutomaticoInterno" data-direccion="/css/img/cargando2.gif" data-tip="adj"
                            data-eti="logo automático interno" data-modo="i" data-ancho="131"></span>
                    </td>
                </tr>
                <tr>
                    <td>Adjunto manual externo
                    </td>
                    <td>
                        <span id="contAdjuntoManual" data-direccion="/img/logo.png"></span>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
<asp:Content ID="cScr" ContentPlaceHolderID="cScr" runat="server">
    <script type="text/javascript" src='<%=this.ResolveUrl("~/scripts/ref/demo.aspx" + this.Application["version"].ToString() + ".js") %>'></script>
</asp:Content>
