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

namespace Teatro.Windows
{
    public partial class frmTickets : Form
    {
        public frmTickets()
        {
            InitializeComponent();
        }

        public frmTickets(VentaTicket ventaTicket)
        {
            this.ventaTicket = ventaTicket;
            InitializeComponent();
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
  

        private IServicioTickets servicio;
        private List<Ticket> lista = new List<Ticket>();
        private VentaTicket ventaTicket=null;

        private void Actualizar()
        {
            try
            {
                servicio = new ServicioTickets();
                if (ventaTicket==null)
                {
                    lista = servicio.GetLista();
                }
                else
                {
                    lista = ventaTicket.Venta.Tickets;
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
            foreach (var ticket in lista)
            {
                AgregarFila(ticket);
            }
        }

        public void AgregarFila(Ticket ticket)
        {
            DataGridViewRow r = ConstruirFila();
            SetearFila(r, ticket);
            AgregarFila(r);
        }

        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, Ticket ticket)
        {
            r.Cells[cmnEvento.Index].Value = ticket.Horario.Evento.NombreEvento;
            r.Cells[cmnFecha.Index].Value = ticket.FechaVenta;
            r.Cells[cmnImporte.Index].Value = ticket.Importe;
            r.Cells[cmnLocalidad.Index].Value = ticket.Localidad.Numero;
            r.Cells[cmnUbicacion.Index].Value = ticket.Localidad.Ubicacion.NombreUbicacion;
            r.Cells[cmnAnulada.Index].Value = ticket.Anulada;
            r.Tag = ticket;
        }

        private DataGridViewRow ConstruirFila()
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }


        private void frmTickets_Load(object sender, EventArgs e)
        {
            Actualizar();
        }
    }
}
