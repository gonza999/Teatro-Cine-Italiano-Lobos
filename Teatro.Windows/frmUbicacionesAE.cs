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
    public partial class frmUbicacionesAE : Form
    {
        public frmUbicacionesAE(frmUbicaciones frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        public frmUbicacionesAE()
        {
            InitializeComponent();
            frm = null;
        }

        private frmUbicaciones frm;
        private bool esEdicion = false;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            servicio = new ServicioUbicaciones();
            if (ubicacion != null)
            {
                txtUbicacion.Text = ubicacion.NombreUbicacion;
                esEdicion = true;
            }
        }

        private Ubicacion ubicacion;
        public void SetUbicacion(Ubicacion ubicacion)
        {
            this.ubicacion = ubicacion;
        }

        public Ubicacion GetUbicacion()
        {
            return ubicacion;
        }



        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (string.IsNullOrEmpty(txtUbicacion.Text.Trim()) &&
                string.IsNullOrWhiteSpace(txtUbicacion.Text.Trim()))
            {
                valido = false;
                errorProvider1.SetError(txtUbicacion, "Debe ingresar una ubicacion");
            }

            return valido;
        }

        private void btnGuardar_Click(object sender, System.EventArgs e)
        {
            if (ValidarDatos())
            {

                if (ubicacion == null)
                {
                    ubicacion = new Ubicacion();
                }

                ubicacion.NombreUbicacion = txtUbicacion.Text;

                if (ValidarObjeto())
                {
                    if (!esEdicion)
                    {
                        servicio.Guardar(ubicacion);
                        if (frm != null)
                        {
                            frm.AgregarFila(ubicacion);
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
            txtUbicacion.Clear();
            txtUbicacion.Focus();
            ubicacion = null;
        }

        private IServicioUbicaciones servicio;
        private bool ValidarObjeto()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (servicio.Existe(ubicacion))
            {
                errorProvider1.SetError(txtUbicacion, "Ubicacion repetida");
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
