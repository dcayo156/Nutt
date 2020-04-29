<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="inicio.aspx.cs" Inherits="NUT.WEB.inicio" %>
<%@ MasterType VirtualPath="~/Plantilla.Master" %>
<asp:Content ID="cCss" ContentPlaceHolderID="cCss" runat="server">
   <style type="text/css">
       .pnlPanel1
       {
           background-color:#d1dad1;
           padding: 0em 0.5em 1em 1em;
           border-radius : 1em;
       }
       .pnlCentroPanel1
       {
           float: left;
           background-color:beige;
           width:10em;
           height:6em;
           border-radius:1em;
           text-align:center;
           margin: 1em 0em 0em 0em;
           vertical-align:middle;
           display:inline-grid
       }
       .pnlCentroNumero
       {
           font-size:2.5em;
           font-weight:bold
       }
       .valorPanel{
             display: inline-block;
              vertical-align: middle;
              line-height: normal;
              padding-top:6px
       }

       .pnlPanel2
       {
             background-color:#d1dad1;
             border-radius : 1em;
       }
       div.pnlPanel2{
            width: 100%;
            max-width: 460px;
       }
      .pnlCentroPanel2
       {
           float: left;
           background-color:beige;
           width:100%;
           height:4em;
           border-radius:1em;
       }
       .pnlCentroNumeroPanel2{
            font-size:2.5em;
            font-weight:bold
       }
       .frm_cont
       {
          background-color:transparent;
          border:0px;
       }
       .frm td
       {
           border-bottom:0px;
       }
      div.SumoSelect, p.SlectBox {
            width: 363px;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
        }
      .trMes{
        display:none;
       }
       @media only screen and (max-width:500px) {
            .pnlPanel1
           {
               width:97%;
           }
            .pnlCentroPanel1
           {
               width:99%;
               height:4em;
               display: inherit;
               text-align:left
           }
           .valorPanel{
             display: inline-block;
              vertical-align: middle;
              line-height: normal;
              padding-left:1em
          }
                div.pnlPanel2{
                width: 100%;
           }

          div.SumoSelect, p.SlectBox {
           width: 100%;
           max-width:330px;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
          }
           .trMes
           {
              display: table-row
           }
        }
         .valorPanelMostrar450 {
               visibility:collapse
        }
        .valorPanelOcultar450 {
                display:normal
        }
       @media only screen and (max-width:490px)
       {
          div.SumoSelect, p.SlectBox {
           width: 100%;
           max-width:300px;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
          }
       }
            @media only screen and (max-width:480px)
       {
          div.SumoSelect, p.SlectBox {
           width: 100%;
           max-width:290px;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
          }
       }
                 @media only screen and (max-width:470px)
       {
          div.SumoSelect, p.SlectBox {
           width: 100%;
           max-width:275px;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
          }
       }
       @media only screen and (max-width:450px)
       {
           .valorPanelMostrar450
           {
            visibility:visible
           }

           .valorPanelOcultar450
           {
               display: none;
           }

            div.SumoSelect, p.SlectBox {
            width: 100%;
            max-width:250px;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
        }
       }
   </style>
</asp:Content>
<asp:Content ID="cLey" ContentPlaceHolderID="cLey" runat="server">
   Esta es su bandeja inicial en el sistema. 
</asp:Content>
<asp:Content ID="c" ContentPlaceHolderID="c" runat="server">
    <div id="pnlContenedor" class="panel" style="width:100%">
            
    </div>
</asp:Content>
<asp:Content ID="cScr" ContentPlaceHolderID="cScr" runat="server">
     <script type="text/javascript" src="<%=ResolveUrl("~/scripts/ref/inicio.aspx" + System.Configuration.ConfigurationManager.AppSettings["version"] + ".js")%>"></script>
</asp:Content>
