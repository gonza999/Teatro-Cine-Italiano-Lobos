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
    public partial class frmPlantasAE : Form
    {
        public frmPlantasAE(frmPlantas frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        public frmPlantasAE()
        {
            InitializeComponent();
            frm = null;
        }

        private frmPlantas frm;
        private bool esEdicion = false;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            servicio = new ServicioPlantas();
            if (planta != null)
            {
                txtPlanta.Text = planta.NombrePlanta;
                esEdicion = true;
            }
        }

        private Planta planta;
        public void SetPlanta(Planta planta)
        {
            this.planta = planta;
        }

        public Planta GetPlanta()
        {
            return planta;
        }



        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (string.IsNullOrEmpty(txtPlanta.Text.Trim()) &&
                string.IsNullOrWhiteSpace(txtPlanta.Text.Trim()))
            {
                valido = false;
                errorProvider1.SetError(txtPlanta, "Debe ingresar una planta");
            }

            return valido;
        }

        private void btnGuardar_Click(object sender, System.EventArgs e)
        {
            if (ValidarDatos())
            {

                if (planta == null)
                {
                    planta = new Planta();
                }

                planta.NombrePlanta = txtPlanta.Text;

                if (ValidarObjeto())
                {
                    if (!esEdicion)
                    {
                        servicio.Guardar(planta);
                        if (frm != null)
                        {
                            frm.AgregarFila(planta);
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
            txtPlanta.Clear();
            txtPlanta.Focus();
            planta = null;
        }

        private IServicioPlantas servicio;
        private bool ValidarObjeto()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (servicio.Existe(planta))
            {
                errorProvider1.SetError(txtPlanta, "Planta repetida");
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
