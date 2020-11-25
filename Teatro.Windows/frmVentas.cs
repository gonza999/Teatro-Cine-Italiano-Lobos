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
    public partial class frmVentas : Form
    {
        public frmVentas()
        {
            InitializeComponent();
        }
        private IServicioVentas servicio; 
        private void frmVentas_Load(object sender, EventArgs e)
        {
            Helper.CargarEmpleadoComboBox(ref cmbEmpleado);
            Helper.CargarFormaPagoComboBox(ref cmbFormaPago);
            Helper.CargarFormaVentaComboBox(ref cmbFormaVenta);
            Helper.CargarEventoComboBox(ref cmbEventos);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void cmbEventos_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbEventos.SelectedIndex!=0)
            {
                cmbHorarios.Enabled = true;
                Evento evento =(Evento) cmbEventos.SelectedItem;
                Helper.CargarHorarioComboBox(ref cmbHorarios,evento);
                cmbUbicaciones.Enabled = true;
                Helper.CargarUbicacionComboBox(ref cmbUbicaciones);
                horario = (Horario)cmbHorarios.SelectedItem;

            }
            else
            {
                cmbHorarios.Enabled = false;
                cmbUbicaciones.Enabled = false;

            }
        }
        private Horario horario;
        private Localidad localidad;
        private void cmbHorarios_SelectedValueChanged(object sender, EventArgs e)
        {
              horario = (Horario)cmbHorarios.SelectedItem;
        }

        private void cmbUbicaciones_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbUbicaciones.SelectedIndex != 0)
            {
                cmbLocalidades.Enabled = true;
                Ubicacion ubicacion = (Ubicacion)cmbUbicaciones.SelectedItem;
                Helper.CargarLocalidadComboBox(ref cmbLocalidades,ubicacion);
                localidad =(Localidad) cmbLocalidades.SelectedItem;
                VerificarOcupado();
            }
            else
            {
                cmbLocalidades.Enabled = false;

            }
        }

        private void cmbLocalidades_SelectedValueChanged(object sender, EventArgs e)
        {
            localidad = (Localidad)cmbLocalidades.SelectedItem;
            VerificarOcupado();
            foreach (var t in listaTickets)
            {
                if (t.Localidad==localidad && t.Horario==horario)
                {
                    Ocupado();
                }
            }

        }
        private IServicioTickets servicioTickets = new ServicioTickets();
        private void VerificarOcupado()
        {
            try
            {
                if (servicioTickets.Existe(localidad,horario))
                {
                    Ocupado();
                }
                else
                {
                    lblOcupado.Text = "DESOCUPADO";
                    lblOcupado.BackColor = Color.LightBlue;
                    txtImporte.Enabled = true;
                    cmbFormaPago.Enabled = true;
                }
            }
            catch (Exception e)
            {
                Helper.MensajeBox(e.Message,Tipo.Error);
                
            }
        }

        private void Ocupado()
        {
            lblOcupado.Text = "OCUPADO";
            lblOcupado.BackColor = Color.Red;
            txtImporte.Enabled = false;
            cmbFormaPago.Enabled = false;
            cmbFormaVenta.Enabled = false;
            btnAgregar.Enabled = false;
        }

        private void cmbFormaPago_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbFormaPago.SelectedIndex != 0)
            {
                cmbFormaVenta.Enabled = true;
                Helper.CargarFormaVentaComboBox(ref cmbFormaVenta);
            }
            else
            {
                cmbFormaVenta.Enabled = false;

            }
        }

        private void cmbFormaVenta_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbFormaVenta.SelectedIndex != 0)
            {
                btnAgregar.Enabled = true;
            }
            else
            {
                btnAgregar.Enabled = false;

            }
        }
        private Ticket ticket;
        private List<Ticket> listaTickets = new List<Ticket>();
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarTickets())
            {
                ticket = new Ticket();
                ticket.Anulada = false;
                ticket.FechaVenta = DateTime.Now;
                ticket.FormaPago =(FormaPago) cmbFormaPago.SelectedItem;
                ticket.FormaVenta = (FormaVenta)cmbFormaVenta.SelectedItem;
                ticket.Horario = (Horario)cmbHorarios.SelectedItem;
                ticket.Importe =decimal.Parse(txtImporte.Text);
                ticket.Localidad = (Localidad)cmbLocalidades.SelectedItem;
                if (VerificarRepetido(ticket))
                {
                    AgregarFila(ticket);
                    listaTickets.Add(ticket);
                    if (listaTickets.Count > 0)
                    {
                        btnVender.Enabled = true;
                    }
                    else
                    {
                        btnVender.Enabled = false;
                    }
                    Ocupado();
                    InicializarControles();
                }
            }
        }

        private void InicializarControles()
        {
            cmbEmpleado.Focus();
            cmbEventos.SelectedIndex = 0;
            txtImporte.Text = "0";
            cmbHorarios.Enabled = false;
            cmbUbicaciones.Enabled = false;
            cmbLocalidades.Enabled = false;
            txtImporte.Enabled = false;
            cmbFormaPago.Enabled = false;
            cmbFormaVenta.Enabled = false;
            btnAgregar.Enabled = false;
        }

        private bool VerificarRepetido(Ticket ticket)
        {
            bool valido = true;
            foreach (var t in listaTickets)
            {
                if (t.Horario==ticket.Horario && t.Localidad==ticket.Localidad)
                {
                    valido = false;
                }
            }
            return valido;
        }

        private bool ValidarTickets()
        {
            bool valido = true;
            errorProvider1.Clear();
            decimal importe = 0;
            if (!decimal.TryParse(txtImporte.Text,out importe))
            {
                valido = false;
                errorProvider1.SetError (txtImporte,"Debe ingresar un importe valido");
            }
            if (importe < 0)
            {
                valido = false;
                errorProvider1.SetError(txtImporte, "El importe no puede ser negativo");

            }
            return valido;
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
            r.Cells[cmnHorario.Index].Value = ticket.Horario.Fecha;
            r.Cells[cmnImporte.Index].Value = ticket.Importe;
            r.Cells[cmnLocalidad.Index].Value = ticket.Localidad.Numero;
        
            r.Tag = ticket;
        }

        private DataGridViewRow ConstruirFila()
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex==4)
            {
                DataGridViewRow r = dgvDatos.Rows[e.RowIndex];
                Ticket ticket =(Ticket) r.Tag;
                listaTickets.Remove(ticket);
                dgvDatos.Rows.RemoveAt(e.RowIndex);
                if (listaTickets.Count > 0)
                {
                    btnVender.Enabled = true;
                }
                else
                {
                    btnVender.Enabled = false;
                }
                
            }
        }

        private void btnVender_Click(object sender, EventArgs e)
        {
            if (ValidarEmpleado())
            {
                Venta venta = new Venta();
                venta.Tickets = listaTickets;
                foreach (var t in venta.Tickets)
                {
                    venta.Total += t.Importe;
                }
                venta.Empleado = (Empleado)cmbEmpleado.SelectedItem;
                venta.Fecha = DateTime.Now;
                venta.Estado = true;
                try
                {
                    servicio = new ServicioVentas();
                    servicio.Guardar(venta);
                    Helper.MensajeBox("Registro guardado", Tipo.Success);
                    DialogResult dr = MessageBox.Show("Desea agregar otro registro?", "Confirmar",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.No)
                    {
                        DialogResult = DialogResult.Cancel;
                        Close();
                    }
                    else
                    {
                        InicializarControles();
                        dgvDatos.Rows.Clear();
                        listaTickets.Clear();

                    }
                }
                catch (Exception ex)
                {
                    Helper.MensajeBox(ex.Message,Tipo.Error);
                    
                }
            }
        }

        private bool ValidarEmpleado()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (cmbEmpleado.SelectedIndex==0)
            {
                valido = false;
                errorProvider1.SetError(cmbEmpleado,"Debe seleccionar un Empleado");
            }
            return valido;
        }
    }
}
