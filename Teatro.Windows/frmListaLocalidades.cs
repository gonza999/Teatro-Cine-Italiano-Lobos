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
    public partial class frmListaLocalidades : Form
    {
        public frmListaLocalidades()
        {
            InitializeComponent();
        }

        private Evento evento;
        private Horario horario;
        private IServicioLocalidades servicio = new ServicioLocalidades();
        private List<Localidad> lista = new List<Localidad>();
        private IServicioTickets servicioTickets = new ServicioTickets();
        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmListaLocalidades_Load(object sender, EventArgs e)
        {
            Helper.CargarEventoComboBox(ref cmbEventos);
            cmbHorarios.Enabled = false;
        }

        private void cmbEventos_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbEventos.SelectedIndex != 0)
            {
                evento = (Evento)cmbEventos.SelectedItem;
                if (!evento.Suspendido)
                {
                    cmbHorarios.Enabled = true;
                    Helper.CargarHorarioComboBox(ref cmbHorarios, evento);
                    horario = (Horario)cmbHorarios.SelectedItem;
                    Actualizar();
                }
                else
                {
                    cmbHorarios.Enabled = false;
                }

            }
            else
            {
                cmbHorarios.Enabled = false;
            }
        }
        private void Actualizar()
        {
            try
            {
                servicio = new ServicioLocalidades();
                lista = servicio.GetLista();
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
            foreach (var localidad in lista)
            {
                AgregarFila(localidad);
            }
        }

        public void AgregarFila(Localidad localidad)
        {
            DataGridViewRow r = ConstruirFila();
            SetearFila(r, localidad);
            AgregarFila(r);
        }

        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, Localidad localidad)
        {
            r.Cells[cmnPlanta.Index].Value = localidad.Planta.NombrePlanta;
            r.Cells[cmnNumero.Index].Value = localidad.Numero;
            r.Cells[cmnUbicacion.Index].Value = localidad.Ubicacion.NombreUbicacion;
            r.Cells[cmnOcupado.Index].Value = VerificarOcupado(localidad);
            r.Tag = localidad;
        }

        private bool VerificarOcupado(Localidad localidad)
        {
            try
            {

                if (servicioTickets.Existe(localidad, horario))
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception e)
            {
                Helper.MensajeBox(e.Message, Tipo.Error);
                return false;
            }
        }

        private DataGridViewRow ConstruirFila()
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }

        private void cmbHorarios_SelectedValueChanged(object sender, EventArgs e)
        {
            horario = (Horario)cmbHorarios.SelectedItem;
            Actualizar();
        }

        private void tsbActualizar_Click(object sender, EventArgs e)
        {
            if (horario!=null)
            {
                Actualizar();

            }
        }
    }
}
