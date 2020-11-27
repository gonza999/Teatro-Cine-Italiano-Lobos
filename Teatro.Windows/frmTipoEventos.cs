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
    public partial class frmTipoEventos : Form
    {
        public frmTipoEventos()
        {
            InitializeComponent();
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private IServicioTipoEventos servicio;
        private List<TipoEvento> lista = new List<TipoEvento>();
        private void frmTipoEventos_Load(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void Actualizar()
        {
            try
            {
                servicio = new ServicioTipoEventos();
                lista = servicio.GetLista();
                MostrarDatosEnGrilla();
            }
            catch (Exception ex)
            {

                MessageBox.Show( this,ex.Message,"Error",
                    MessageBoxButtons.OK);
            }
        }

        private void MostrarDatosEnGrilla()
        {
            dgvDatos.Rows.Clear();
            foreach (var tipoevento in lista)
            {
                AgregarFila(tipoevento);
            }
        }

        public void AgregarFila(TipoEvento tipoevento)
        {
            DataGridViewRow r = ConstruirFila();
            SetearFila(r, tipoevento);
            AgregarFila(r);
        }

        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, TipoEvento tipoevento)
        {
            r.Cells[cmnTipoEventos.Index].Value = tipoevento.NombreTipoEvento;

            r.Tag = tipoevento;
        }

        private DataGridViewRow ConstruirFila()
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmTipoEventoAE frm = new frmTipoEventoAE(this);
            frm.Text = "Nuevo";
            frm.ShowDialog(this);
        }

        private void frmTipoEventos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==Convert.ToChar(Keys.Enter))
            {
                if (string.IsNullOrEmpty(txtBuscar.Text)
                    || string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    return;
                }
                lista=servicio.BuscarTipoEvento(txtBuscar.Text);
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
                TipoEvento tipoevento = (TipoEvento)r.Tag;

                DialogResult dr = MessageBox.Show(this, $"¿Desea dar de baja a la tipoevento {tipoevento.NombreTipoEvento}?",
                    "Confirmar Baja",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        if (!servicio.EstaRelacionado(tipoevento))
                        {
                            servicio.Borrar(tipoevento.TipoEventoId);
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
                TipoEvento tipoevento = (TipoEvento)r.Tag;
                tipoevento = servicio.GetTipoEventoPorId(tipoevento.TipoEventoId);
                //TipoEvento tipoeventoAux = (TipoEvento)tipoevento.Clone();
                frmTipoEventoAE frm = new frmTipoEventoAE(this);
                frm.Text = "Editar TipoEvento";
                frm.SetTipoEvento(tipoevento);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        tipoevento = frm.GetTipoEvento();
                        servicio.Guardar(tipoevento);
                        SetearFila(r, tipoevento);
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
