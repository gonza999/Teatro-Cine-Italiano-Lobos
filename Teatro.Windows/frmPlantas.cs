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
    public partial class frmPlantas : Form
    {
        public frmPlantas()
        {
            InitializeComponent();
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private IServicioPlantas servicio;
        private List<Planta> lista = new List<Planta>();
        private void frmPlantas_Load(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void Actualizar()
        {
            try
            {
                servicio = new ServicioPlantas();
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
            foreach (var planta in lista)
            {
                AgregarFila(planta);
            }
        }

        public void AgregarFila(Planta planta)
        {
            DataGridViewRow r = ConstruirFila();
            SetearFila(r, planta);
            AgregarFila(r);
        }

        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, Planta planta)
        {
            r.Cells[cmnPlanta.Index].Value = planta.NombrePlanta;

            r.Tag = planta;
        }

        private DataGridViewRow ConstruirFila()
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmPlantasAE frm = new frmPlantasAE(this);
            frm.Text = "Nuevo";
            frm.ShowDialog(this);
        }

        private void frmPlantas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (string.IsNullOrEmpty(txtBuscar.Text)
                    || string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    return;
                }
                lista = servicio.BuscarPlanta(txtBuscar.Text);
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
                Planta planta = (Planta)r.Tag;

                DialogResult dr = MessageBox.Show(this, $"¿Desea dar de baja a la planta {planta.NombrePlanta}?",
                    "Confirmar Baja",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        if (!servicio.EstaRelacionado(planta))
                        {
                            servicio.Borrar(planta.PlantaId);
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
                Planta planta = (Planta)r.Tag;
                Planta plantaAux = (Planta)planta.Clone();
                frmPlantasAE frm = new frmPlantasAE(this);
                frm.Text = "Editar Planta";
                frm.SetPlanta(planta);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        planta = frm.GetPlanta();
                        servicio.Guardar(planta);
                        SetearFila(r, planta);
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
