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
    public partial class frmEmpleados : Form
    {
        public frmEmpleados()
        {
            InitializeComponent();
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private IServicioEmpleados servicio;
        private List<Empleado> lista = new List<Empleado>();
        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void Actualizar()
        {
            try
            {
                servicio = new ServicioEmpleados();
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
            foreach (var empleado in lista)
            {
                AgregarFila(empleado);
            }
        }

        public void AgregarFila(Empleado empleado)
        {
            DataGridViewRow r = ConstruirFila();
            SetearFila(r, empleado);
            AgregarFila(r);
        }

        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, Empleado empleado)
        {
            r.Cells[cmnNombre.Index].Value = empleado.Nombre;
            r.Cells[cmnTipoDocumento.Index].Value = empleado.TipoDocumento.NombreTipoDocumento;
            r.Cells[cmnApellido.Index].Value = empleado.Apellido;
            r.Cells[cmnMail.Index].Value = empleado.Mail;
            r.Cells[cmnNumeroDocumento.Index].Value = empleado.NumeroDocumento;

            r.Tag = empleado;
        }

        private DataGridViewRow ConstruirFila()
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmEmpleadosAE frm = new frmEmpleadosAE(this);
            frm.Text = "Nuevo";
            frm.ShowDialog(this);
        }

        private void frmEmpleados_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (string.IsNullOrEmpty(txtBuscar.Text)
                    || string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    return;
                }
                lista = servicio.BuscarEmpleado(txtBuscar.Text);
                MostrarDatosEnGrilla();
            }
        }

        private void tsbActualizar_Click(object sender, EventArgs e)
        {
            Actualizar();
            txtBuscar.Clear();
        }

        private void tsbBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count > 0)
            {
                DataGridViewRow r = dgvDatos.SelectedRows[0];
                Empleado empleado = (Empleado)r.Tag;

                DialogResult dr = MessageBox.Show(this, $"¿Desea dar de baja al empleado {empleado.Nombre} {empleado.Apellido}?",
                    "Confirmar Baja",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        if (!servicio.EstaRelacionado(empleado))
                        {
                            servicio.Borrar(empleado.EmpleadoId);
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

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count > 0)
            {
                DataGridViewRow r = dgvDatos.SelectedRows[0];
                Empleado empleado = (Empleado)r.Tag;
                //Empleado empleadoAux = (Empleado)empleado.Clone();
                frmEmpleadosAE frm = new frmEmpleadosAE(this);
                frm.Text = "Editar Empleado";
                frm.SetEmpleado(empleado);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        empleado = frm.GetEmpleado();
                        servicio.Guardar(empleado);
                        SetearFila(r, empleado);
                        Helper.MensajeBox("Registro Editado", Tipo.Success);
                    }
                    catch (Exception exception)
                    {
                        Helper.MensajeBox(exception.Message, Tipo.Error);
                    }
                }
            }
        }
        //private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.ColumnIndex == 4)
        //    {
        //        if ((bool)dgvDatos.Rows[e.RowIndex].Cells[cmnSuspendido.Index].Value)
        //        {
        //            dgvDatos.Rows[e.RowIndex].Cells[cmnSuspendido.Index].Value=false;
        //        }
        //        else
        //        {
        //            dgvDatos.Rows[e.RowIndex].Cells[cmnSuspendido.Index].Value = true;
        //        }
        //    }
        //}
    }
}
