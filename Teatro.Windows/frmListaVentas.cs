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
    public partial class frmListaVentas : Form
    {
        public frmListaVentas()
        {
            InitializeComponent();
        }


        private IServicioVentasTickets servicio;
        private IServicioTickets servicioTickets;
        private IServicioVentas servicioVentas;
        private List<VentaTicket> lista = new List<VentaTicket>();

        private void Actualizar()
        {
            try
            {
                servicio = new ServicioVentasTickets();
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
            foreach (var ventaTicket in lista)
            {
                AgregarFila(ventaTicket);
            }
        }

        public void AgregarFila(VentaTicket ventaTicket)
        {
            DataGridViewRow r = ConstruirFila();
            SetearFila(r, ventaTicket);
            AgregarFila(r);
        }

        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, VentaTicket ventaTicket)
        {
            r.Cells[cmnTotal.Index].Value = ventaTicket.Venta.Total;
            r.Cells[cmnFecha.Index].Value = ventaTicket.Venta.Fecha;
            r.Cells[cmnAnulada.Index].Value = ventaTicket.Venta.Estado;
            r.Cells[cmnTickets.Index].Value = new Button().Text = "Tickets";
            r.Cells[cmnEmpleado.Index].Value = ventaTicket.Venta.Empleado.Nombre;
            r.Tag = ventaTicket;
        }

        private DataGridViewRow ConstruirFila()
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }

        private void frmListaVentas_Load(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow r = dgvDatos.SelectedRows[0];
            VentaTicket ventaTicket = (VentaTicket)r.Tag;

            if (e.ColumnIndex == 3)
            {
              
                DialogResult dr = MessageBox.Show(this, $"¿Desea anular permanentemente la venta?",
                    "Confirmar Anulado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    ventaTicket.Venta.Estado = true;
                    r.Cells[cmnAnulada.Index].Value = ventaTicket.Venta.Estado;
                    servicioVentas = new ServicioVentas();
                    servicioTickets = new ServicioTickets();
                    servicioVentas.AnularVenta(ventaTicket.Venta.VentaId);
                    foreach (var t in ventaTicket.Venta.Tickets)
                    {
                        servicioTickets.AnularTicket(t.TicketId);
                    }
                }
            }
            else if (e.ColumnIndex == 1)
            {
                frmTickets frmTickets =new frmTickets(ventaTicket);
                frmTickets.Text = "Tickets";
                frmTickets.ShowDialog(this);
            }
        }
    }
}

