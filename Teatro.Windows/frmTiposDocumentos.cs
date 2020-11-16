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
    public partial class frmTiposDocumentos : Form
    {
        public frmTiposDocumentos()
        {
            InitializeComponent();
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private IServicioTiposDocumentos servicio;
        private List<TipoDocumento> lista = new List<TipoDocumento>();
        private void frmTiposDocumentos_Load(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void Actualizar()
        {
            try
            {
                servicio = new ServicioTiposDocumentos();
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
            foreach (var tipoDocumento in lista)
            {
                AgregarFila(tipoDocumento);
            }
        }

        public void AgregarFila(TipoDocumento tipoDocumento)
        {
            DataGridViewRow r = ConstruirFila();
            SetearFila(r, tipoDocumento);
            AgregarFila(r);
        }

        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, TipoDocumento tipoDocumento)
        {
            r.Cells[cmnTipoDocumento.Index].Value = tipoDocumento.NombreTipoDocumento;

            r.Tag = tipoDocumento;
        }

        private DataGridViewRow ConstruirFila()
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmTiposDocumentosAE frm = new frmTiposDocumentosAE(this);
            frm.Text = "Nuevo";
            frm.ShowDialog(this);
        }

        private void frmTiposDocumentos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (string.IsNullOrEmpty(txtBuscar.Text)
                    || string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    return;
                }
                lista = servicio.BuscarTipoDocumento(txtBuscar.Text);
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
                TipoDocumento tipoDocumento = (TipoDocumento)r.Tag;

                DialogResult dr = MessageBox.Show(this, $"¿Desea dar de baja el tipo de Documento {tipoDocumento.NombreTipoDocumento}?",
                    "Confirmar Baja",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        if (!servicio.EstaRelacionado(tipoDocumento))
                        {
                            servicio.Borrar(tipoDocumento.TipoDocumentoId);
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
                TipoDocumento tipoDocumento = (TipoDocumento)r.Tag;
                TipoDocumento tipoDocumentoAux = (TipoDocumento)tipoDocumento.Clone();
                frmTiposDocumentosAE frm = new frmTiposDocumentosAE(this);
                frm.Text = "Editar TipoDocumento";
                frm.SetTipoDocumento(tipoDocumento);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        tipoDocumento = frm.GetTipoDocumento();
                        servicio.Guardar(tipoDocumento);
                        SetearFila(r, tipoDocumento);
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
