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
    public partial class frmProductos : Form
    {
        //variable global que va a almacenar un producto buscado
        private Producto EntidadBuscada = new Producto();
        private static frmBuscarProducto buscarProducto;



        public frmProductos()
        {
            InitializeComponent();
        }

        private void Limpiar() {
            txtId.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtCantidad.Value = 0;
            txtPrecio.Text = string.Empty;
        }

        private Producto GenerarEntidad() {
            Producto entidad;
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                entidad = EntidadBuscada;
                //entidad = new Producto();
                //entidad.Id=Convert.ToInt32(((string)txtId.Text).Trim());
            }
            else {
                entidad = new Producto();
            }
            entidad.Descripcion = txtDescripcion.Text;
            entidad.Cantidad = (int)txtCantidad.Value;
            entidad.Precio = Convert.ToDecimal(txtPrecio.Text);
            return entidad;
        }

        private void CargarProductos(string condicion = "") { 
            LN_Producto logica=new LN_Producto(Config.getConnectionString);
            List<Producto> lista;
            try
            {
                lista = logica.ListarProductos(condicion);
                if (lista.Count > 0) {
                    grdLista.DataSource= lista;
                    
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || (int)e.KeyChar == 8 || (int)e.KeyChar == 44)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Producto producto;
            int retorno;
            LN_Producto logica = new LN_Producto(Config.getConnectionString);
            decimal precio;
            try
            {
                if (!string.IsNullOrEmpty(txtDescripcion.Text) && !string.IsNullOrEmpty(txtPrecio.Text))
                {
                    if (decimal.TryParse(txtPrecio.Text, out precio))
                    {
                        producto = GenerarEntidad();
                        //llamar a los métodos de insertar y modificar
                        retorno = logica.InsertarModificar(producto);
                        if ( retorno> 0)
                        {
                            if (retorno == 1)
                            {
                                MessageBox.Show("El producto se registró satisfactoriamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (retorno == 2) {
                                MessageBox.Show("El producto se modificó satisfactoriamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            
                            Limpiar();
                            CargarProductos();
                            //actualizar tabla
                        }
                        else
                        {
                            MessageBox.Show("No fue posible realizar la operación", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else {
                        MessageBox.Show("El precio requiere un valor númerico", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else {
                    MessageBox.Show("La descripción y el precio son datos requeridos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            try
            {
                CargarProductos("");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void grdLista_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(grdLista.SelectedRows[0].Cells[0].Value);
                BuscarProducto(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void BuscarProducto(int id) {
            Producto producto;
            LN_Producto logica= new LN_Producto();
            logica.CadenaConexion = Config.getConnectionString;
            string condicion = $"Id={id}";
            try
            {
                producto = logica.ObtenerProducto(condicion);
                if (producto.Existe)
                {
                    txtId.Text = producto.Id.ToString();
                    txtDescripcion.Text = producto.Descripcion.ToString();
                    txtCantidad.Value = producto.Cantidad;
                    txtPrecio.Text = producto.Precio.ToString();
                    EntidadBuscada = producto;
                }
                else {
                    MessageBox.Show("Imposible cargar los datos ya que el producto ha tenido cambios", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CargarProductos();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            buscarProducto=new frmBuscarProducto();
            buscarProducto.Aceptar += new EventHandler(Aceptar);
            buscarProducto.Show();
        }

        private void Aceptar(object id, EventArgs e)
        {
            try
            {
                int id_producto = (int)id;
                if (id_producto > -1) {
                    BuscarProducto(id_producto);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            LN_Producto logica= new LN_Producto(Config.getConnectionString);
            try
            {
                if (!string.IsNullOrEmpty(txtId.Text))
                {
                    logica.Eliminar(Convert.ToInt32(txtId.Text));
                    MessageBox.Show("Operación realizada satisfactoriamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                    CargarProductos();
                }
                else {
                    MessageBox.Show("Debe seleccionar un producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
