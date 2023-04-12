<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="FrmVistaProducto.aspx.cs" Inherits="VentasWeb.FrmVistaProducto" %>
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
            <div class="col-8">
                <h1>Listado de productos</h1>
                <asp:Label runat="server" Text="Descripción" CssClass="form-label" ID="lbldescripcion"></asp:Label>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-success mt-2" OnClick="btnBuscar_Click" />
                <asp:Button ID="btnNuevo" runat="server" Text="Agregar nuevo producto" CssClass="btn btn-primary mt-2" OnClick="btnNuevo_Click" />
                <hr />
                <asp:GridView ID="grdLista" runat="server" AllowPaging="True" EmptyDataText="No hay datos disponibles" Width="100px" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" OnPageIndexChanging="grdLista_PageIndexChanging">

                    <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripci&#243;n"></asp:BoundField>
                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad"></asp:BoundField>
                        <asp:BoundField DataField="Precio" HeaderText="Precio"></asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkModificar" runat="server" CommandName="Modificar" CommandArgument='<%# Eval("Id").ToString() %>' OnCommand="lnkModificar_Command">Modificar</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("Id").ToString() %>' OnCommand="lnkEliminar_Command">Eliminar</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC"></FooterStyle>

                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White"></HeaderStyle>

                    <PagerStyle HorizontalAlign="Center" BackColor="#999999" ForeColor="Black"></PagerStyle>

                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White"></SelectedRowStyle>

                    <SortedAscendingCellStyle BackColor="#F1F1F1"></SortedAscendingCellStyle>

                    <SortedAscendingHeaderStyle BackColor="#808080"></SortedAscendingHeaderStyle>

                    <SortedDescendingCellStyle BackColor="#CAC9C9"></SortedDescendingCellStyle>

                    <SortedDescendingHeaderStyle BackColor="#383838"></SortedDescendingHeaderStyle>
                </asp:GridView>

            </div>
        </div>
    </div>
</asp:Content>
