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
    public partial class frmEventosAE : Form
    {
        public frmEventosAE(frmEventos frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        public frmEventosAE()
        {
            InitializeComponent();
            frm = null;
        }

        private frmEventos frm;
        private bool esEdicion = false;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            servicio = new ServicioEventos();
            if (evento != null)
            {
                txtEvento.Text = evento.NombreEvento;
                txtDescripcion.Text = evento.Descripcion;
                pickerFecha.Value = evento.FechaEvento;
                checkSuspendido.Checked = evento.Suspendido;
                cmbClasificacion.SelectedValue = evento.Clasificacion.ClasificacionId ;
                cmbTipoEvento.SelectedValue = evento.TipoEvento.TipoEventoId;
                cmbDistribucion.SelectedValue = evento.Distribucion.DistribucionId;
                esEdicion = true;
            }
        }

        private Evento evento;
        public void SetEvento(Evento evento)
        {
            this.evento = evento;
        }

        public Evento GetEvento()
        {
            return evento;
        }



        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (string.IsNullOrEmpty(txtEvento.Text.Trim()) &&
                string.IsNullOrWhiteSpace(txtEvento.Text.Trim()))
            {
                valido = false;
                errorProvider1.SetError(txtEvento, "Debe ingresar una evento");
            }
            if (string.IsNullOrEmpty(txtDescripcion.Text.Trim()) &&
                string.IsNullOrWhiteSpace(txtDescripcion.Text.Trim()))
            {
                valido = false;
                errorProvider1.SetError(txtDescripcion, "Debe ingresar una descripcion");
            }
            if (cmbClasificacion.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(cmbClasificacion, "Debe seleccionar una clasificacion");

            }
            if (cmbTipoEvento.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(cmbTipoEvento, "Debe seleccionar un tipo de evento");

            }
            if (cmbDistribucion.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(cmbDistribucion, "Debe seleccionar una Distribucion");

            }
            if (pickerFecha.Value.Date > DateTime.Today)
            {
                valido = false;
                errorProvider1.SetError(pickerFecha, "Fecha mal ingresada");
            }
            return valido;
        }

        private void btnGuardar_Click(object sender, System.EventArgs e)
        {
            if (ValidarDatos())
            {

                if (evento == null)
                {
                    evento = new Evento();
                }

                evento.NombreEvento = txtEvento.Text;
                evento.FechaEvento = pickerFecha.Value.Date;
                evento.Descripcion = txtDescripcion.Text;
                evento.Suspendido = checkSuspendido.Checked;
                evento.TipoEvento = (TipoEvento)cmbTipoEvento.SelectedItem;
                evento.Clasificacion = (Clasificacion)cmbClasificacion.SelectedItem;
                evento.Distribucion = (Distribucion)cmbDistribucion.SelectedItem;
                if (ValidarObjeto())
                {
                    if (!esEdicion)
                    {
                        servicio.Guardar(evento);
                        if (frm != null)
                        {
                            frm.AgregarFila(evento);
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
            txtEvento.Clear();
            txtDescripcion.Clear();
            pickerFecha.Value = DateTime.Today;
            checkSuspendido.Checked = false;
            Helper.CargarClasificacionComboBox(ref cmbClasificacion);
            Helper.CargarTipoEventoComboBox(ref cmbTipoEvento);
            Helper.CargarDistribucionComboBox(ref cmbDistribucion) ;
            txtEvento.Focus();
            evento = null;
        }

        private IServicioEventos servicio;
        private bool ValidarObjeto()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (servicio.Existe(evento))
            {
                errorProvider1.SetError(txtEvento, "No pueden haber dos eventos diferentes el mismo día");
                valido = false;
            }
            return valido;
        }

        private void btnCancelar_Click(object sender, System.EventArgs e)
        {
            InicializarControles();
            DialogResult = DialogResult.Cancel;

        }

        private void frmEventosAE_Load(object sender, EventArgs e)
        {
            Helper.CargarClasificacionComboBox(ref cmbClasificacion);
            Helper.CargarTipoEventoComboBox(ref cmbTipoEvento);
            Helper.CargarDistribucionComboBox(ref cmbDistribucion);
        }
    }
}
