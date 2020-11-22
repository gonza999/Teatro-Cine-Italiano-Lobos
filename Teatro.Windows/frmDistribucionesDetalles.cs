using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Teatro.BussinesLayer.Entidades;

namespace Teatro.Windows
{
    public partial class frmDistribucionesDetalles : Form
    {
        public frmDistribucionesDetalles()
        {
            InitializeComponent();
        }
        private Distribucion distribucion;
        internal void SetDistribucion(Distribucion distribucion)
        {
            this.distribucion = distribucion;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmDistribucionesDetalles_Load(object sender, EventArgs e)
        {
            lblDistribucion.Text = distribucion.NombreDistribucion;
            foreach (var d in distribucion.DistribucionUbicacion)
            {
                if (d.Ubicacion.UbicacionId==1)
                {
                    txtButacas.Text = d.Precio.ToString();
                }
                else
                {
                    txtPalcos.Text = d.Precio.ToString();
                }
            }
        }
    }
}
