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
    public partial class frmBuscarProducto : Form
    {
        //evento
        public event EventHandler Aceptar;
        //variables globales
        private int id_producto=0;

        public frmBuscarProducto()
        {
            InitializeComponent();
        }

        private void CargarProductos(string condicion = "")
        {
            LN_Producto logica = new LN_Producto(Config.getConnectionString);
            List<Producto> lista;
            try
            {
                lista = logica.ListarProductos(condicion);
                //if (lista.Count > 0)
                //{
                    grdLista.DataSource = lista;

                //}
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        private void SeleccionarProducto() {
            try
            {
                if (grdLista.SelectedRows.Count > 0) {
                    id_producto = (int)grdLista.SelectedRows[0].Cells[0].Value;
                    Aceptar(id_producto, null);
                    Close();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        
        }

        private void frmBuscarProducto_Load(object sender, EventArgs e)
        {
            try
            {
                CargarProductos(string.Empty);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void grdLista_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SeleccionarProducto();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                SeleccionarProducto();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            id_producto= -1;
            Aceptar(id_producto, null);
            Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string condicion;
            try
            {
                if (!string.IsNullOrEmpty(txtdescripcion.Text))
                {
                    condicion = $"ID='{txtId.Text}' or DESCRIPCION like '%{txtdescripcion.Text.Trim()}%'";
                }
                else {
                    condicion = $"ID='{txtId.Text}'";
                }
                
                CargarProductos(condicion);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || (int)e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
