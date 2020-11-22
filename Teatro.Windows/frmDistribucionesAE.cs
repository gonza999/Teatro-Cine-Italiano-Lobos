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
using Teatro.ServiceLayer.Facades;
using Teatro.ServiceLayer.Servicios;
using Teatro.Windows.Helpers;
using Teatro.Windows.Helpers.Enum;

namespace Teatro.Windows
{
    public partial class frmDistribucionesAE : Form
    {
        public frmDistribucionesAE(frmDistribuciones frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        public frmDistribucionesAE()
        {
            InitializeComponent();
            frm = null;
        }

        private frmDistribuciones frm;
        private bool esEdicion = false;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            servicio = new ServicioDistribuciones();
            if (distribucion != null)
            {
                txtDistribucion.Text = distribucion.NombreDistribucion;
                foreach (var d in distribucion.DistribucionUbicacion)
                {
                    if (d.Ubicacion.UbicacionId == 1)
                    {
                        txtButacas.Text = d.Precio.ToString();
                    }
                    else
                    {
                        txtPalcos.Text = d.Precio.ToString();
                    }
                }



                esEdicion = true;
            }
        }

        private Distribucion distribucion;
        public void SetDistribucion(Distribucion distribucion)
        {
            this.distribucion = distribucion;
        }

        public Distribucion GetDistribucion()
        {
            return distribucion;
        }



        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (string.IsNullOrEmpty(txtDistribucion.Text.Trim()) &&
                string.IsNullOrWhiteSpace(txtDistribucion.Text.Trim()))
            {
                valido = false;
                errorProvider1.SetError(txtDistribucion, "Debe ingresar una distribucion");
            }
            if (string.IsNullOrEmpty(txtButacas.Text.Trim()) &&
                string.IsNullOrWhiteSpace(txtButacas.Text.Trim()))
            {
                valido = false;
                errorProvider1.SetError(txtButacas, "Debe ingresar un precio de butaca");
            }
            decimal precio = 0;
            if (!decimal.TryParse(txtButacas.Text, out precio))
            {
                valido = false;
                errorProvider1.SetError(txtButacas, "Debe ingresar un precio de butaca valido");
            }
            if (precio < 0)
            {
                valido = false;
                errorProvider1.SetError(txtButacas, "El precio no debe ser menor a cero");

            }

            if (string.IsNullOrEmpty(txtPalcos.Text.Trim()) &&
           string.IsNullOrWhiteSpace(txtPalcos.Text.Trim()))
            {
                valido = false;
                errorProvider1.SetError(txtPalcos, "Debe ingresar un precio de palco");
            }
            precio = 0;
            if (!decimal.TryParse(txtPalcos.Text, out precio))
            {
                valido = false;
                errorProvider1.SetError(txtPalcos, "Debe ingresar un precio de palco valido");
            }
            if (precio < 0)
            {
                valido = false;
                errorProvider1.SetError(txtPalcos, "El precio no debe ser menor a cero");

            }

            return valido;
        }

        private void btnGuardar_Click(object sender, System.EventArgs e)
        {
            if (ValidarDatos())
            {

                if (distribucion == null)
                {
                    distribucion = new Distribucion();
                }
                distribucion.NombreDistribucion = txtDistribucion.Text;
                distribucion.DistribucionUbicacion.Clear();
                Ubicacion ubicacion = new Ubicacion()
                {
                    UbicacionId = 1
                };
                DistribucionUbicacion d = new DistribucionUbicacion();
                d.Distribucion = distribucion;
                d.Ubicacion = ubicacion;
                d.Precio = decimal.Parse(txtButacas.Text);
                distribucion.DistribucionUbicacion.Add(d);

                ubicacion = new Ubicacion()
                {
                    UbicacionId = 2
                };
                d = new DistribucionUbicacion();
                d.Distribucion = distribucion;
                d.Ubicacion = ubicacion;
                d.Precio = decimal.Parse(txtPalcos.Text);
                distribucion.DistribucionUbicacion.Add(d);



                if (ValidarObjeto())
                {
                    if (!esEdicion)
                    {
                        servicio.Guardar(distribucion);
                        if (frm != null)
                        {
                            frm.AgregarFila(distribucion);
                        }
                        Helper.MensajeBox("Registro guardado", Tipo.Success);
                        DialogResult dr = MessageBox.Show("Desea agregar otro registro?", "Confirmar",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.No)
                        {
                            DialogResult = DialogResult.Cancel;
                        }
                        else
                        {
                            InicializarControles();
                        }
                    }
                    else
                    {
                        DialogResult = DialogResult.OK;
                    }

                }
            }

        }

        private void InicializarControles()
        {
            txtDistribucion.Clear();
            txtButacas.Clear();
            txtPalcos.Clear();
            txtDistribucion.Focus();
            distribucion = null;
        }

        private IServicioDistribuciones servicio;
        private bool ValidarObjeto()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (servicio.Existe(distribucion))
            {
                errorProvider1.SetError(txtDistribucion, "Distribucion repetida");
                valido = false;
            }
            return valido;
        }

        private void btnCancelar_Click(object sender, System.EventArgs e)
        {
            InicializarControles();
            DialogResult = DialogResult.Cancel;

        }
    }
}
