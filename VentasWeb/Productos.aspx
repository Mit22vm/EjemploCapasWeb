<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="VentasWeb.Productos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
         function MostrarMensaje(mensaje) {
             alert(mensaje);
         }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-6 mx-auto">
                <asp:TextBox ID="txtId" runat="server" Visible="False"></asp:TextBox> 
                <asp:Label ID="Label1" runat="server" Text="Descripcion" CssClass="form-label"></asp:Label>
                 

                
                <asp:RequiredFieldValidator ID="rfvdescripcion" runat="server" ErrorMessage="descrpción requerida" Display="None" ControlToValidate="txtdescripcion"  ValidationGroup="Validation"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtdescripcion" runat="server"  CssClass="form-control"></asp:TextBox> 

                <asp:Label ID="Label2" runat="server" Text="Cantidad" CssClass="form-label"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" ErrorMessage="Cantidad requerida" Display="None" ControlToValidate="txtCantidad"  ValidationGroup="Validation"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtCantidad" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>

                <asp:Label ID="Label3" runat="server" Text="Precio" CssClass="form-label"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvprecio" runat="server" ErrorMessage="Precio requerido" Display="None" ControlToValidate="txtPrecio"  ValidationGroup="Validation"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtPrecio" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>

                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success mt-2" OnClick="btnGuardar_Click" ValidationGroup="Validation"/>

                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-secondary mt-2" OnClick="btnRegresar_Click" />
                
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Validation" />

            </div>
        </div>
    </div>
</asp:Content>
