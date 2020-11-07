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
    public partial class frmClasificacionesAE : Form
    {
        public frmClasificacionesAE(frmClasificaciones frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        public frmClasificacionesAE()
        {
            InitializeComponent();
            frm = null;
        }

        private frmClasificaciones frm;
        private bool esEdicion = false;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            servicio = new ServicioClasificiones();
            if (clasificacion != null)
            {
                txtClasificacion.Text = clasificacion.NombreClasificacion;
                esEdicion = true;
            }
        }

        private Clasificacion clasificacion;
        public void SetClasificacion(Clasificacion clasificacion)
        {
            this.clasificacion = clasificacion;
        }

        public Clasificacion GetClasificacion()
        {
            return clasificacion;
        }



        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (string.IsNullOrEmpty(txtClasificacion.Text.Trim()) &&
                string.IsNullOrWhiteSpace(txtClasificacion.Text.Trim()))
            {
                valido = false;
                errorProvider1.SetError(txtClasificacion, "Debe ingresar una clasificacion");
            }

            return valido;
        }

        private void btnGuardar_Click(object sender, System.EventArgs e)
        {
            if (ValidarDatos())
            {

                if (clasificacion == null)
                {
                    clasificacion = new Clasificacion();
                }

                clasificacion.NombreClasificacion = txtClasificacion.Text;

                if (ValidarObjeto())
                {
                    if (!esEdicion)
                    {
                        servicio.Guardar(clasificacion);
                        if (frm != null)
                        {
                            frm.AgregarFila(clasificacion);
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
            txtClasificacion.Clear();
            txtClasificacion.Focus();
            clasificacion = null;
        }

        private IServicioClasificaciones servicio;
        private bool ValidarObjeto()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (servicio.Existe(clasificacion))
            {
                errorProvider1.SetError(txtClasificacion, "Clasificacion repetida");
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
