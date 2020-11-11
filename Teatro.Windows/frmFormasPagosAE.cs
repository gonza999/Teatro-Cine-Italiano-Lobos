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
    public partial class frmFormasPagosAE : Form
    {
        public frmFormasPagosAE(frmFormasPagos frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        public frmFormasPagosAE()
        {
            InitializeComponent();
            frm = null;
        }

        private frmFormasPagos frm;
        private bool esEdicion = false;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            servicio = new ServicioFormasPagos();
            if (formaPago != null)
            {
                txtFormaPago.Text = formaPago.NombreFormaPago;
                esEdicion = true;
            }
        }

        private FormaPago formaPago;
        public void SetFormaPago(FormaPago formaPago)
        {
            this.formaPago = formaPago;
        }

        public FormaPago GetFormaPago()
        {
            return formaPago;
        }



        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (string.IsNullOrEmpty(txtFormaPago.Text.Trim()) &&
                string.IsNullOrWhiteSpace(txtFormaPago.Text.Trim()))
            {
                valido = false;
                errorProvider1.SetError(txtFormaPago, "Debe ingresar una forma de Pago");
            }

            return valido;
        }

        private void btnGuardar_Click(object sender, System.EventArgs e)
        {
            if (ValidarDatos())
            {

                if (formaPago == null)
                {
                    formaPago = new FormaPago();
                }

                formaPago.NombreFormaPago = txtFormaPago.Text;

                if (ValidarObjeto())
                {
                    if (!esEdicion)
                    {
                        servicio.Guardar(formaPago);
                        if (frm != null)
                        {
                            frm.AgregarFila(formaPago);
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
            txtFormaPago.Clear();
            txtFormaPago.Focus();
            formaPago = null;
        }

        private IServicioFormasPagos servicio;
        private bool ValidarObjeto()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (servicio.Existe(formaPago))
            {
                errorProvider1.SetError(txtFormaPago, "Forma de Pago repetida");
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
