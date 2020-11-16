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
    public partial class frmTiposDocumentosAE : Form
    {
        public frmTiposDocumentosAE(frmTiposDocumentos frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        public frmTiposDocumentosAE()
        {
            InitializeComponent();
            frm = null;
        }

        private frmTiposDocumentos frm;
        private bool esEdicion = false;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            servicio = new ServicioTiposDocumentos();
            if (tipoDocumento != null)
            {
                txtTipoDocumento.Text = tipoDocumento.NombreTipoDocumento;
                esEdicion = true;
            }
        }

        private TipoDocumento tipoDocumento;
        public void SetTipoDocumento(TipoDocumento tipoDocumento)
        {
            this.tipoDocumento = tipoDocumento;
        }

        public TipoDocumento GetTipoDocumento()
        {
            return tipoDocumento;
        }



        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (string.IsNullOrEmpty(txtTipoDocumento.Text.Trim()) &&
                string.IsNullOrWhiteSpace(txtTipoDocumento.Text.Trim()))
            {
                valido = false;
                errorProvider1.SetError(txtTipoDocumento, "Debe ingresar un tipo de Documento");
            }

            return valido;
        }

        private void btnGuardar_Click(object sender, System.EventArgs e)
        {
            if (ValidarDatos())
            {

                if (tipoDocumento == null)
                {
                    tipoDocumento = new TipoDocumento();
                }

                tipoDocumento.NombreTipoDocumento = txtTipoDocumento.Text;

                if (ValidarObjeto())
                {
                    if (!esEdicion)
                    {
                        servicio.Guardar(tipoDocumento);
                        if (frm != null)
                        {
                            frm.AgregarFila(tipoDocumento);
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
            txtTipoDocumento.Clear();
            txtTipoDocumento.Focus();
            tipoDocumento = null;
        }

        private IServicioTiposDocumentos servicio;
        private bool ValidarObjeto()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (servicio.Existe(tipoDocumento))
            {
                errorProvider1.SetError(txtTipoDocumento, "Tipo de Documento repetida");
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
