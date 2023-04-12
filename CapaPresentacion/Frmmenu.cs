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
    public partial class Frmmenu : Form
    {
        public Frmmenu()
        {
            InitializeComponent();
        }

        private void mnusalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuproductos_Click(object sender, EventArgs e)
        {
            frmProductos form = new frmProductos();
            form.Show(this);
        }

        private void mnuVentas_Click(object sender, EventArgs e)
        {
            frmventas form = new frmventas();
            form.Show(this);
        }
    }
}
