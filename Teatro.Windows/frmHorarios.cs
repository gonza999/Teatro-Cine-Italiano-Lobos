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
    public partial class frmHorarios : Form
    {
        public frmHorarios()
        {
            InitializeComponent();
        }

        public frmHorarios(Evento evento)
        {
            InitializeComponent();
            this.evento = evento;
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private IServicioHorarios servicio;
        private List<Horario> lista = new List<Horario>();
        private Evento evento;

        private void frmHorarios_Load(object sender, EventArgs e)
        {
            Actualizar();
        }
        private void Actualizar()
        {
            try
            {
                servicio = new ServicioHorarios();
                if (evento==null)
                {
                    lista = servicio.GetLista();
                }
                else
                {
                    lista = servicio.GetLista(evento);
                }
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
                AgregarFila(horario);
            }
        }

        public void AgregarFila(Horario horario)
        {
            DataGridViewRow r = ConstruirFila();
            SetearFila(r, horario);
            AgregarFila(r);
        }

        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, Horario horario)
        {
            r.Cells[cmnEvento.Index].Value = horario.Evento.NombreEvento;
            r.Cells[cmnFecha.Index].Value = horario.Fecha.Date;
            r.Cells[cmnHora.Index].Value = horario.Hora.TimeOfDay;
            r.Tag = horario;
        }

        private DataGridViewRow ConstruirFila()
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }

        private void tsbBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count > 0)
            {
                DataGridViewRow r = dgvDatos.SelectedRows[0];
                Horario horario = (Horario)r.Tag;

                DialogResult dr = MessageBox.Show(this, $"¿Desea dar de baja al horario del evento {horario.Evento.NombreEvento}?",
                    "Confirmar Baja",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        if (!servicio.EstaRelacionado(horario))
                        {
                            servicio.Borrar(horario);
                            dgvDatos.Rows.Remove(r);
                            Helper.MensajeBox("Registro borrado", Tipo.Success);
                        }
                        else
                        {
                            Helper.MensajeBox("Baja denegada,registro relacionado", Tipo.Error);
                        }
                    }
                    catch (Exception exception)
                    {
                        Helper.MensajeBox(exception.Message, Tipo.Error);

                    }
                }
            }
        }
    }
}
