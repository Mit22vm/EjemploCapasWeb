using Entidades;
using Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmventas : Form
    {
        public frmventas()
        {
            InitializeComponent();
            IniciarControles();
        }

        private void LimpiarProducto() {
            txtcodigo.Tag = 0;
            txtdescripcion.Tag = 0;
            txtcodigo.Text = string.Empty;
            txtdescripcion.Text = string.Empty;
            txtprecio.Text = string.Empty;
            nudCantidad.Value = 1;
            txtcodigo.Focus();
        }

        private void IniciarControles() {
            txtid.Tag = 0;
            txtid.Text = string.Empty;
            txtcliente.Tag= 0;
            txtcliente.Text=string.Empty;
            cboTipo.SelectedItem = -1;
            txtTotal.Text = string.Empty;   
            LimpiarProducto();
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            frmBuscarCliente frm=new frmBuscarCliente();
            frm.AceptarCliente += new EventHandler(AceptarCliente);
            frm.Show(this);
        }

        private void AceptarCliente(object id, EventArgs e)
        {
            try
            {
                int id_cliente = (int)id;
                if (id_cliente > -1) {
                    BuscarCliente(id_cliente);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BuscarProducto(int id)
        {
            Producto producto;
            LN_Producto logica = new LN_Producto();
            logica.CadenaConexion = Config.getConnectionString;
            string condicion = $"Id={id}";
            try
            {
                producto = logica.ObtenerProducto(condicion);
                if (producto.Existe)
                {
                    txtcodigo.Text = producto.Id.ToString();
                    txtdescripcion.Tag= producto.Id.ToString();
                    txtdescripcion.Text = producto.Descripcion.ToString();
                    txtprecio.Text = (producto.Precio*(decimal)1.35).ToString("¢###.##");
                    nudCantidad.Focus();
                }
                else
                {
                    txtcodigo.Text = string.Empty;
                    txtdescripcion.Tag= string.Empty;
                    txtdescripcion.Text= string.Empty;
                    txtprecio.Text= string.Empty;
                    nudCantidad.Value = 1;
                    
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void BuscarCliente(int id)
        {
            Cliente cliente;
            LN_Cliente logica = new LN_Cliente(Config.getConnectionString);
            
            string condicion = $"Id={id}";
            try
            {
                cliente = logica.ObtenerCliente(condicion);
                if (cliente.Existe)
                {
                    txtcliente.Tag = cliente.Id.ToString();
                    txtcliente.Text = $"{cliente.Nombre} {cliente.Apellido}";
                }
                else
                {
                    MessageBox.Show("Imposible cargar el cliente ya que ha tenido cambios", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void BuscarDetalle(int id)
        {
            Detalle detalle;
            LN_Detalle logica = new LN_Detalle(Config.getConnectionString);
            List<Detalle> detalles;
            LN_Ventas logicaV = new LN_Ventas(Config.getConnectionString);

            string condicion = $"Id={id}";
            try
            {
                detalle = logica.ObtenerDetalle(condicion);
                if (detalle.Existe)
                {
                    txtcodigo.Tag= detalle.Id.ToString();
                    txtcodigo.Text=detalle.ProductoId.ToString();
                    txtdescripcion.Tag = detalle.ProductoId.ToString();
                    txtdescripcion.Text = detalle.Descripcion;
                    txtprecio.Text=detalle.PrecioVenta.ToString("¢###.##");
                    nudCantidad.Value=detalle.Cantidad;
                }
                else
                {
                    MessageBox.Show("Imposible cargar el detalle ya que ha tenido cambios", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    detalles=CargarDetalles(id);
                    txtTotal.Text = logicaV.CalcularTotal(detalles).ToString("¢###.##");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            frmBuscarProducto frm=new frmBuscarProducto();
            frm.Aceptar += new EventHandler(Aceptar);
            frm.Show(this);
        }

        private void Aceptar(object id_producto, EventArgs e)
        {
            try
            {
                int id = (int)id_producto;
                if (id > -1) {
                    BuscarProducto(id);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtcodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            int codigo;
            try
            {
                if (e.KeyChar == 13)
                {
                    if (!string.IsNullOrEmpty(txtcodigo.Text))
                    {
                        if (int.TryParse(txtcodigo.Text, out codigo)) { 
                            BuscarProducto(codigo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }         
            
        }

        private void Insertar() {
            List<Detalle> lista;
            try
            {
                if (!string.IsNullOrEmpty(txtcliente.Text) && !string.IsNullOrEmpty(cboTipo.Text))
                {
                    if (!string.IsNullOrEmpty(txtdescripcion.Tag.ToString()) && !string.IsNullOrEmpty(nudCantidad.Value.ToString()))
                    {
                        Venta venta = GenerarVenta();
                        Detalle detalle = GenerarDetalle();
                        LN_Ventas logica = new LN_Ventas(Config.getConnectionString);
                        detalle.VentaId = venta.Id;
                        logica.Insertar(venta, detalle);
                        txtid.Text=logica.IdVenta.ToString();
                        lista=CargarDetalles(logica.IdVenta);
                        txtTotal.Text = logica.CalcularTotal(lista).ToString("¢###.##");
                        LimpiarProducto();
                        //MessageBox.Show(logica.Mensaje);

                    }
                    else {
                        MessageBox.Show("Debe seleccionar un artículo y establecer una cantidad mayor a cero", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else {

                    MessageBox.Show("Faltan datos en el encabezado de la factura", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        private Venta GenerarVenta() { 
            Venta venta = new Venta();
            if (!string.IsNullOrEmpty(txtid.Text)) {
                venta.Id = Convert.ToInt32(txtid.Text);    
            }
            venta.ClienteId = Convert.ToInt32(txtcliente.Tag);
            venta.Tipo = cboTipo.Text;
            venta.Estado = "Pendiente";

            return venta;
        }

        private Detalle GenerarDetalle()
        {
            Detalle detalle = new Detalle();
            if (!string.IsNullOrEmpty(txtcodigo.Tag.ToString()))
            {
                detalle.Id = Convert.ToInt32(txtcodigo.Tag);
            }

            detalle.ProductoId = Convert.ToInt32(txtdescripcion.Tag);
            detalle.Cantidad = Convert.ToInt32(nudCantidad.Value);
            
            return detalle;
        }

        private List<Detalle> CargarDetalles(int idventa) {
            LN_Detalle logica = new LN_Detalle(Config.getConnectionString);
            List<Detalle> lista;
            
            try
            {
                lista = logica.ListarDetalles($"ventaId={idventa}");
                grdLista.DataSource = lista;
                
            }
            catch (Exception e)
            {

                throw e;
            }
            return lista;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Insertar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            IniciarControles();
        }

        private void grdLista_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id;
            try
            {
                if (grdLista.SelectedRows.Count > 0) {
                    id = (int)grdLista.SelectedRows[0].Cells[0].Value;
                    BuscarDetalle(id);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            LN_Detalle logica = new LN_Detalle(Config.getConnectionString);
            LN_Ventas logicaV = new LN_Ventas(Config.getConnectionString);
            List<Detalle> detalles;
            try
            {
                int id = Convert.ToInt32(txtcodigo.Tag);
                if (id > 0)
                {
                    if (logica.Eliminar(id))
                    {
                        MessageBox.Show("Detalle eliminado satisfactoriamente");
                    }
                    else
                    {
                        MessageBox.Show("No fue posible eliminar el detalle");
                    }
                    detalles = CargarDetalles(Convert.ToInt32(txtid.Text));
                    if (detalles.Count > 0)
                    {
                        txtTotal.Text = logicaV.CalcularTotal(detalles).ToString("¢###.##");
                        LimpiarProducto();
                    }
                    else {
                        MessageBox.Show("Al eliminar todos los detalles se procede a eliminar la venta también");
                        IniciarControles();
                    }
                    
                }
                else {
                    MessageBox.Show("Debe seleccionar el detalle que desea eliminar");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
