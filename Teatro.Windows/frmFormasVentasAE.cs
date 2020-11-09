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
    public partial class frmFormasVentasAE : Form
    {
        public frmFormasVentasAE(frmFormasVentas frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        public frmFormasVentasAE()
        {
            InitializeComponent();
            frm = null;
        }

        private frmFormasVentas frm;
        private bool esEdicion = false;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            servicio = new ServicioFormasVentas();
            if (formaVenta != null)
            {
                txtFormaVenta.Text = formaVenta.NombreFormaVenta;
                esEdicion = true;
            }
        }

        private FormaVenta formaVenta;
        public void SetFormaVenta(FormaVenta formaVenta)
        {
            this.formaVenta = formaVenta;
        }

        public FormaVenta GetFormaVenta()
        {
            return formaVenta;
        }



        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (string.IsNullOrEmpty(txtFormaVenta.Text.Trim()) &&
                string.IsNullOrWhiteSpace(txtFormaVenta.Text.Trim()))
            {
                valido = false;
                errorProvider1.SetError(txtFormaVenta, "Debe ingresar una forma de Venta");
            }

            return valido;
        }

        private void btnGuardar_Click(object sender, System.EventArgs e)
        {
            if (ValidarDatos())
            {

                if (formaVenta == null)
                {
                    formaVenta = new FormaVenta();
                }

                formaVenta.NombreFormaVenta = txtFormaVenta.Text;

                if (ValidarObjeto())
                {
                    if (!esEdicion)
                    {
                        servicio.Guardar(formaVenta);
                        if (frm != null)
                        {
                            frm.AgregarFila(formaVenta);
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
            txtFormaVenta.Clear();
            txtFormaVenta.Focus();
            formaVenta = null;
        }

        private IServicioFormasVentas servicio;
        private bool ValidarObjeto()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (servicio.Existe(formaVenta))
            {
                errorProvider1.SetError(txtFormaVenta, "Forma de Venta repetida");
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
