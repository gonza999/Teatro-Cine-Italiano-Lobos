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
    public partial class frmDistribuciones : Form
    {
        public frmDistribuciones()
        {
            InitializeComponent();
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private IServicioDistribuciones servicio;
        private List<Distribucion> lista = new List<Distribucion>();
        private void frmDistribuciones_Load(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void Actualizar()
        {
            try
            {
                servicio = new ServicioDistribuciones();
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
            foreach (var distribucion in lista)
            {
                AgregarFila(distribucion);
            }
        }

        public void AgregarFila(Distribucion distribucion)
        {
            DataGridViewRow r = ConstruirFila();
            SetearFila(r, distribucion);
            AgregarFila(r);
        }

        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, Distribucion distribucion)
        {
            r.Cells[cmnDistribucion.Index].Value = distribucion.NombreDistribucion;
            r.Cells[cmnDetalle.Index].Value = new Button().Text="Más detalle";
            r.Tag = distribucion;
        }

        private DataGridViewRow ConstruirFila()
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmDistribucionesAE frm = new frmDistribucionesAE(this);
            frm.Text = "Nuevo";
            frm.ShowDialog(this);
        }

        private void frmDistribuciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (string.IsNullOrEmpty(txtBuscar.Text)
                    || string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    return;
                }
                lista = servicio.BuscarDistribucion(txtBuscar.Text);
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
                Distribucion distribucion = (Distribucion)r.Tag;

                DialogResult dr = MessageBox.Show(this, $"¿Desea dar de baja la distribucion {distribucion.NombreDistribucion}?",
                    "Confirmar Baja",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        if (!servicio.EstaRelacionado(distribucion))
                        {
                            servicio.Borrar(distribucion);
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
                Distribucion distribucion = (Distribucion)r.Tag;
                distribucion = servicio.GetDistribucionPorId (distribucion.DistribucionId);
                distribucion.DistribucionUbicacion = ((Distribucion)r.Tag).DistribucionUbicacion;
                //Distribucion distribucionAux = (Distribucion)distribucion.Clone();
                frmDistribucionesAE frm = new frmDistribucionesAE(this);
                frm.Text = "Editar Distribucion";
                frm.SetDistribucion(distribucion);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        distribucion = frm.GetDistribucion();
                        servicio.Guardar(distribucion);
                        SetearFila(r, distribucion);
                        Helper.MensajeBox("Registro Editado", Tipo.Success);
                    }
                    catch (Exception exception)
                    {
                        Helper.MensajeBox(exception.Message, Tipo.Error);
                    }
                }
            }
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                frmDistribucionesDetalles frm = new frmDistribucionesDetalles();
                frm.Text = "Detalles";
                if (dgvDatos.SelectedRows.Count > 0)
                {
                    DataGridViewRow r = dgvDatos.SelectedRows[0];
                    Distribucion distribucion = (Distribucion)r.Tag;

                    //var r = dgvDatos.SelectedRows[e.RowIndex];
                    //Distribucion distribucion =(Distribucion) r.Tag;
                    frm.SetDistribucion(distribucion);
                    frm.ShowDialog(this);
                }
            }
        }
    }
}
