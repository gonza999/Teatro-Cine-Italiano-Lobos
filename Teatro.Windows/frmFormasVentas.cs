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
    public partial class frmFormasVentas : Form
    {
        public frmFormasVentas()
        {
            InitializeComponent();
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private IServicioFormasVentas servicio;
        private List<FormaVenta> lista = new List<FormaVenta>();
        private void frmFormasVentas_Load(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void Actualizar()
        {
            try
            {
                servicio = new ServicioFormasVentas();
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
            foreach (var formaVenta in lista)
            {
                AgregarFila(formaVenta);
            }
        }

        public void AgregarFila(FormaVenta formaVenta)
        {
            DataGridViewRow r = ConstruirFila();
            SetearFila(r, formaVenta);
            AgregarFila(r);
        }

        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, FormaVenta formaVenta)
        {
            r.Cells[cmnFormaVenta.Index].Value = formaVenta.NombreFormaVenta;

            r.Tag = formaVenta;
        }

        private DataGridViewRow ConstruirFila()
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmFormasVentasAE frm = new frmFormasVentasAE(this);
            frm.Text = "Nuevo";
            frm.ShowDialog(this);
        }

        private void frmFormasVentas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (string.IsNullOrEmpty(txtBuscar.Text)
                    || string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    return;
                }
                lista = servicio.BuscarFormaVenta(txtBuscar.Text);
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
                FormaVenta formaVenta = (FormaVenta)r.Tag;

                DialogResult dr = MessageBox.Show(this, $"¿Desea dar de baja a la forma de Venta {formaVenta.NombreFormaVenta}?",
                    "Confirmar Baja",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        if (!servicio.EstaRelacionado(formaVenta))
                        {
                            servicio.Borrar(formaVenta.FormaVentaId);
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
                FormaVenta formaVenta = (FormaVenta)r.Tag;
                formaVenta = servicio.GetFormaVentaPorId(formaVenta.FormaVentaId);
                //FormaVenta formaVentaAux = (FormaVenta)formaVenta.Clone();
                frmFormasVentasAE frm = new frmFormasVentasAE(this);
                frm.Text = "Editar FormaVenta";
                frm.SetFormaVenta(formaVenta);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        formaVenta = frm.GetFormaVenta();
                        servicio.Guardar(formaVenta);
                        SetearFila(r, formaVenta);
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

