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
    public partial class frmLocalidadesAE : Form
    {
        public frmLocalidadesAE(frmLocalidades frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        public frmLocalidadesAE()
        {
            InitializeComponent();
            frm = null;
        }

        private frmLocalidades frm;
        private bool esEdicion = false;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            servicio = new ServicioLocalidades();
            if (localidad != null)
            {
                nudNumero.Value = localidad.Numero;
                cmbPlanta.SelectedValue = localidad.Planta.PlantaId;
                cmbUbicacion.SelectedValue = localidad.Ubicacion.UbicacionId;
                nudFila.Value = localidad.Fila;
                esEdicion = true;
            }
        }

        private Localidad localidad;
        public void SetLocalidad(Localidad localidad)
        {
            this.localidad = localidad;
        }

        public Localidad GetLocalidad()
        {
            return localidad;
        }



        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (cmbPlanta.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(cmbPlanta, "Debe seleccionar una planta");

            }

            if (cmbUbicacion.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(cmbUbicacion, "Debe seleccionar una Ubicacion");

            }
            if (nudNumero.Value<0)
            {
                valido = false;
                errorProvider1.SetError(nudNumero, "Numero mal ingresado");
            }
            if (nudFila.Value <= 0)
            {
                valido = false;
                errorProvider1.SetError(nudFila, "Fila mal ingresado");
            }
            return valido;
        }

        private void btnGuardar_Click(object sender, System.EventArgs e)
        {
  
            if (ValidarDatos())
            {

                if (localidad == null)
                {
                    localidad = new Localidad();
                }
                localidad.Numero =Convert.ToInt32(nudNumero.Value);
                localidad.Planta = (Planta)cmbPlanta.SelectedItem;
                localidad.Ubicacion = (Ubicacion)cmbUbicacion.SelectedItem;
                localidad.Fila = Convert.ToInt32(nudFila.Value);
                if (ValidarObjeto())
                {
                    if (!esEdicion)
                    {
                        servicio.Guardar(localidad);
                        if (frm != null)
                        {
                            frm.AgregarFila(localidad);
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
            nudNumero.Value = 0;
            nudFila.Value = 0;
            Helper.CargarPlantaComboBox(ref cmbPlanta);
            Helper.CargarUbicacionComboBox(ref cmbUbicacion);
            cmbPlanta.Focus();
            localidad = null;
        }

        private IServicioLocalidades servicio;
        private bool ValidarObjeto()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (servicio.Existe(localidad))
            {
                errorProvider1.SetError(nudNumero, "Localidad repetida");
                valido = false;
            }
            return valido;
        }

        private void btnCancelar_Click(object sender, System.EventArgs e)
        {
            InicializarControles();
            DialogResult = DialogResult.Cancel;

        }

        private void frmLocalidadesAE_Load(object sender, EventArgs e)
        {
            Helper.CargarPlantaComboBox(ref cmbPlanta);
            Helper.CargarUbicacionComboBox(ref cmbUbicacion);
        }
    }
}
