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
    public partial class frmEmpleadosAE : Form
    {
        public frmEmpleadosAE(frmEmpleados frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        public frmEmpleadosAE()
        {
            InitializeComponent();
            frm = null;
        }

        private frmEmpleados frm;
        private bool esEdicion = false;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            servicio = new ServicioEmpleados();
            if (empleado != null)
            {
                txtNombre.Text = empleado.Nombre;
                txtApellido.Text = empleado.Apellido;
                txtMail.Text = empleado.Mail;
                txtNroDoc.Text = empleado.NumeroDocumento;
                cmbTipoDocumento.SelectedValue = empleado.TipoDocumento.TipoDocumentoId;
                txtTelFijo.Text = empleado.TelefonoFijo;
                txtTelMovil.Text = empleado.TelefonoMovil;
                esEdicion = true;
            }
        }

        private Empleado empleado;
        public void SetEmpleado(Empleado empleado)
        {
            this.empleado = empleado;
        }

        public Empleado GetEmpleado()
        {
            return empleado;
        }



        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (string.IsNullOrEmpty(txtNombre.Text.Trim()) &&
                string.IsNullOrWhiteSpace(txtNombre.Text.Trim()))
            {
                valido = false;
                errorProvider1.SetError(txtNombre, "Debe ingresar un nombre");
            }
            if (string.IsNullOrEmpty(txtApellido.Text.Trim()) &&
                string.IsNullOrWhiteSpace(txtApellido.Text.Trim()))
            {
                valido = false;
                errorProvider1.SetError(txtApellido, "Debe ingresar un apellido");
            }
            if (cmbTipoDocumento.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(cmbTipoDocumento, "Debe seleccionar una tipo de Documento");

            }
            if (string.IsNullOrEmpty(txtNroDoc.Text.Trim()) &&
              string.IsNullOrWhiteSpace(txtNroDoc.Text.Trim()))
            {
                valido = false;
                errorProvider1.SetError(txtNroDoc, "Debe ingresar un numero de documento");
            }

            return valido;
        }

        private void btnGuardar_Click(object sender, System.EventArgs e)
        {
            if (ValidarDatos())
            {

                if (empleado == null)
                {
                    empleado = new Empleado();
                }

                empleado.Nombre = txtNombre.Text;
                empleado.Apellido = txtApellido.Text;
                empleado.Mail = txtMail.Text;
                empleado.NumeroDocumento = txtNroDoc.Text;
                empleado.TelefonoFijo = txtTelFijo.Text;
                empleado.TipoDocumento = (TipoDocumento)cmbTipoDocumento.SelectedItem;
                empleado.TelefonoMovil = txtTelMovil.Text;

                if (ValidarObjeto())
                {
                    if (!esEdicion)
                    {
                        servicio.Guardar(empleado);
                        if (frm != null)
                        {
                            frm.AgregarFila(empleado);
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
            txtNombre.Clear();
            txtApellido.Clear();
            txtMail.Clear();
            txtNroDoc.Clear();
            txtTelFijo.Clear();
            txtTelMovil.Clear();
            Helper.CargarTipoDocumentoComboBox(ref cmbTipoDocumento);
            txtNombre.Focus();
            empleado = null;
        }

        private IServicioEmpleados servicio;
        private bool ValidarObjeto()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (servicio.Existe(empleado))
            {
                errorProvider1.SetError(txtNroDoc, "Empleado repetido");
                valido = false;
            }
            return valido;
        }

        private void btnCancelar_Click(object sender, System.EventArgs e)
        {
            InicializarControles();
            DialogResult = DialogResult.Cancel;

        }

        private void frmEmpleadosAE_Load(object sender, EventArgs e)
        {
            Helper.CargarTipoDocumentoComboBox(ref cmbTipoDocumento);
        }

    }
}
