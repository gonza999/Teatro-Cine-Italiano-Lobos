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
                checkSuspendido.Checked = evento.Suspendido;
                cmbClasificacion.SelectedValue = evento.Clasificacion.ClasificacionId ;
                cmbTipoEvento.SelectedValue = evento.TipoEvento.TipoEventoId;
                cmbDistribucion.SelectedValue = evento.Distribucion.DistribucionId;
                MostrarGrilla(evento);
                esEdicion = true;
            }
        }
        private List<Horario> lista=new List<Horario>();
        private IServicioHorarios servicioHorarios;
        private void MostrarGrilla(Evento evento)
        {
            try
            {
                servicioHorarios = new ServicioHorarios();
                lista = servicioHorarios.GetLista(evento);
                evento.Horarios = lista;
                MostrarDatosEnGrilla();
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Error",
                    MessageBoxButtons.OK);
            }
        }

        private void MostrarDatosEnGrilla()
        {
            dgvDatos.Rows.Clear();
            foreach (var horario in lista)
            {
                if (ValidarObjeto())
                {
                    AgregarFila(horario); 
                }
            }
        }

        public void AgregarFila(Horario horario)
        {
            DataGridViewRow r = ConstruirFila();
            SetearFila(r, horario);
            AgregarFila(r);
        }

        private DataGridViewRow ConstruirFila()
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }

        private void SetearFila(DataGridViewRow r, Horario horario)
        {
            r.Cells[cmnFecha.Index].Value = horario.Fecha.Date;
            r.Cells[cmnHorario.Index].Value = horario.Hora.TimeOfDay;
            r.Tag = horario;
            listaHorarios.Add(horario);
        }

        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }
        private List<Horario> listaHorarios = new List<Horario>();
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
            if (dgvDatos.Rows.Count == 0)
            {
                valido = false;
                errorProvider1.SetError(btnAgregarFecha, "Debe agregar mínimo una fecha");
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
                evento.Descripcion = txtDescripcion.Text;
                evento.Suspendido = checkSuspendido.Checked;
                evento.TipoEvento = (TipoEvento)cmbTipoEvento.SelectedItem;
                evento.Clasificacion = (Clasificacion)cmbClasificacion.SelectedItem;
                evento.Distribucion = (Distribucion)cmbDistribucion.SelectedItem;
                foreach (var h in listaHorarios)
                {

                    evento.Horarios.Add(h);
                    if (ValidarObjeto())
                    {
                        servicioHorarios.Guardar(h);

                    }

                }
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
            pickerHora.Value = DateTime.Today;
            checkSuspendido.Checked = false;
            dgvDatos.Rows.Clear();
            listaHorarios.Clear();
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
            foreach (var h in evento.Horarios)
            {
                if (servicioHorarios.Existe(h))
                {
                    valido = false;
                    errorProvider1.Clear();
                    errorProvider1.SetError(pickerFecha, "No pueden suceder dos eventos distintos en la misma fecha");
                }
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidarFechas())
            {
                Horario horario = new Horario();
                horario.Evento = evento;
                horario.Fecha = pickerFecha.Value;
                horario.Hora = pickerHora.Value;
                if (ValidarFechas())
                {
                    AgregarFila(horario);
                }
            }
        }

        private bool ValidarFechas()
        {
            errorProvider2.Clear();
            bool valido = true;

            if (pickerFecha.Value.Date < DateTime.Today)
            {
                valido = false;
                errorProvider2.SetError(pickerFecha, "La fecha del evento no puede ser anterior a la actual");
            }
            foreach (var h in listaHorarios)
            {
                if ((h.Hora.TimeOfDay==pickerHora.Value.TimeOfDay) && h.Fecha.Date==pickerFecha.Value.Date)
                {
                    valido = false;
                    errorProvider2.SetError(pickerFecha, "No puede haber un evento que suceda en el mismo horario en la misma fecha");
                }
                if (servicioHorarios.Existe(h))
                {
                    valido = false;
                    errorProvider2.Clear();
                    errorProvider2.SetError(pickerFecha, "No pueden suceder dos eventos distintos en la misma fecha");
                }
            }
            return valido;
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex==2)
            {
                DataGridViewRow r = dgvDatos.Rows[e.RowIndex];
                Horario horario =(Horario) r.Tag;
                dgvDatos.Rows.RemoveAt(e.RowIndex);
                servicioHorarios.Borrar(horario.HorarioId);
                listaHorarios.Remove(horario);
                MostrarGrilla(evento);
            }
        }
    }
}
