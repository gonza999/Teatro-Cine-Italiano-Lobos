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
    public partial class frmTipoEventoAE : Form
    {
        public frmTipoEventoAE(frmTipoEventos frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        public frmTipoEventoAE()
        {
            InitializeComponent();
            frm = null;
        }

        private frmTipoEventos frm;
        private bool esEdicion = false;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            servicio = new ServicioTipoEventos();
            if (tipoevento != null)
            {
                txtTipoEvento.Text = tipoevento.NombreTipoEvento;
                esEdicion = true;
            }
        }

        private TipoEvento tipoevento;
        public void SetTipoEvento(TipoEvento tipoevento)
        {
            this.tipoevento = tipoevento;
        }

        public TipoEvento GetTipoEvento()
        {
            return tipoevento;
        }



        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (string.IsNullOrEmpty(txtTipoEvento.Text.Trim()) &&
                string.IsNullOrWhiteSpace(txtTipoEvento.Text.Trim()))
            {
                valido = false;
                errorProvider1.SetError(txtTipoEvento, "Debe ingresar una tipoevento");
            }

            return valido;
        }

        private void btnGuardar_Click(object sender, System.EventArgs e)
        {
            if (ValidarDatos())
            {

                if (tipoevento == null)
                {
                    tipoevento = new TipoEvento();
                }

                tipoevento.NombreTipoEvento = txtTipoEvento.Text;

                if (ValidarObjeto())
                {
                    if (!esEdicion)
                    {
                        servicio.Guardar(tipoevento);
                        if (frm != null)
                        {
                            frm.AgregarFila(tipoevento);
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
            txtTipoEvento.Clear();
            txtTipoEvento.Focus();
            tipoevento = null;
        }

        private IServicioTipoEventos servicio;
        private bool ValidarObjeto()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (servicio.Existe(tipoevento))
            {
                errorProvider1.SetError(txtTipoEvento, "TipoEvento repetida");
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

