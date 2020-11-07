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
    public partial class frmUbicaciones : Form
    {
        public frmUbicaciones()
        {
            InitializeComponent();
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private IServicioUbicaciones servicio;
        private List<Ubicacion> lista = new List<Ubicacion>();
        private void frmUbicaciones_Load(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void Actualizar()
        {
            try
            {
                servicio = new ServicioUbicaciones();
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
            foreach (var ubicacion in lista)
            {
                AgregarFila(ubicacion);
            }
        }

        public void AgregarFila(Ubicacion ubicacion)
        {
            DataGridViewRow r = ConstruirFila();
            SetearFila(r, ubicacion);
            AgregarFila(r);
        }

        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, Ubicacion ubicacion)
        {
            r.Cells[cmnUbicacion.Index].Value = ubicacion.NombreUbicacion;

            r.Tag = ubicacion;
        }

        private DataGridViewRow ConstruirFila()
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmUbicacionesAE frm = new frmUbicacionesAE(this);
            frm.Text = "Nuevo";
            frm.ShowDialog(this);
        }

        private void frmUbicaciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (string.IsNullOrEmpty(txtBuscar.Text)
                    || string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    return;
                }
                lista = servicio.BuscarUbicacion(txtBuscar.Text);
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
                Ubicacion ubicacion = (Ubicacion)r.Tag;

                DialogResult dr = MessageBox.Show(this, $"¿Desea dar de baja a la ubicacion {ubicacion.NombreUbicacion}?",
                    "Confirmar Baja",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        if (!servicio.EstaRelacionado(ubicacion))
                        {
                            servicio.Borrar(ubicacion.UbicacionId);
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
                Ubicacion ubicacion = (Ubicacion)r.Tag;
                Ubicacion ubicacionAux = (Ubicacion)ubicacion.Clone();
                frmUbicacionesAE frm = new frmUbicacionesAE(this);
                frm.Text = "Editar Ubicacion";
                frm.SetUbicacion(ubicacion);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        ubicacion = frm.GetUbicacion();
                        servicio.Guardar(ubicacion);
                        SetearFila(r, ubicacion);
                        Helper.MensajeBox("Registro Editado", Tipo.Success);
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
