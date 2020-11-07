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
    public partial class frmClasificaciones : Form
    {
        public frmClasificaciones()
        {
            InitializeComponent();
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private IServicioClasificaciones servicio;
        private List<Clasificacion> lista = new List<Clasificacion>();
        private void frmClasificaciones_Load(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void Actualizar()
        {
            try
            {
                servicio = new ServicioClasificiones();
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
            foreach (var clasificacion in lista)
            {
                AgregarFila(clasificacion);
            }
        }

        public void AgregarFila(Clasificacion clasificacion)
        {
            DataGridViewRow r = ConstruirFila();
            SetearFila(r, clasificacion);
            AgregarFila(r);
        }

        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, Clasificacion clasificacion)
        {
            r.Cells[cmnClasificacion.Index].Value = clasificacion.NombreClasificacion;

            r.Tag = clasificacion;
        }

        private DataGridViewRow ConstruirFila()
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmClasificacionesAE frm = new frmClasificacionesAE(this);
            frm.Text = "Nuevo";
            frm.ShowDialog(this);
        }

        private void frmClasificaciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (string.IsNullOrEmpty(txtBuscar.Text)
                    || string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    return;
                }
                lista = servicio.BuscarClasificacion(txtBuscar.Text);
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
                Clasificacion clasificacion = (Clasificacion)r.Tag;

                DialogResult dr = MessageBox.Show(this, $"¿Desea dar de baja a la clasificacion {clasificacion.NombreClasificacion}?",
                    "Confirmar Baja",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        if (!servicio.EstaRelacionado(clasificacion))
                        {
                            servicio.Borrar(clasificacion.ClasificacionId);
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
                Clasificacion clasificacion = (Clasificacion)r.Tag;
                Clasificacion clasificacionAux = (Clasificacion)clasificacion.Clone();
                frmClasificacionesAE frm = new frmClasificacionesAE(this);
                frm.Text = "Editar Clasificacion";
                frm.SetClasificacion(clasificacion);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        clasificacion = frm.GetClasificacion();
                        servicio.Guardar(clasificacion);
                        SetearFila(r, clasificacion);
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