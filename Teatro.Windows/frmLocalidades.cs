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
    public partial class frmLocalidades : Form
    {
        public frmLocalidades()
        {
            InitializeComponent();
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private IServicioLocalidades servicio;
        private List<Localidad> lista = new List<Localidad>();
        private void frmLocalidades_Load(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void Actualizar()
        {
            try
            {
                servicio = new ServicioLocalidades();
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
            foreach (var localidad in lista)
            {
                AgregarFila(localidad);
            }
        }

        public void AgregarFila(Localidad localidad)
        {
            DataGridViewRow r = ConstruirFila();
            SetearFila(r, localidad);
            AgregarFila(r);
        }

        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, Localidad localidad)
        {
            r.Cells[cmnPlanta.Index].Value = localidad.Planta.NombrePlanta;
            r.Cells[cmnNumero.Index].Value = localidad.Numero;
            r.Cells[cmnUbicacion.Index].Value = localidad.Ubicacion.NombreUbicacion;
            r.Tag = localidad;
        }

        private DataGridViewRow ConstruirFila()
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmLocalidadesAE frm = new frmLocalidadesAE(this);
            frm.Text = "Nuevo";
            frm.ShowDialog(this);
        }

        //private void frmLocalidades_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == Convert.ToChar(Keys.Enter))
        //    {
        //        if (string.IsNullOrEmpty(txtBuscar.Text)
        //            || string.IsNullOrWhiteSpace(txtBuscar.Text))
        //        {
        //            return;
        //        }
        //        lista = servicio.BuscarLocalidad(txtBuscar.Text);
        //        MostrarDatosEnGrilla();
        //    }
        //}

        private void tsbActualizar_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void tsbBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count > 0)
            {
                DataGridViewRow r = dgvDatos.SelectedRows[0];
                Localidad localidad = (Localidad)r.Tag;

                DialogResult dr = MessageBox.Show(this, $"¿Desea dar de baja a la localidad {localidad.Numero}?",
                    "Confirmar Baja",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        if (!servicio.EstaRelacionado(localidad))
                        {
                            servicio.Borrar(localidad.LocalidadId);
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
                Localidad localidad = (Localidad)r.Tag;
                localidad = servicio.GetLocalidadPorId(localidad.LocalidadId);
                //Localidad localidadAux = (Localidad)localidad.Clone();
                frmLocalidadesAE frm = new frmLocalidadesAE(this);
                frm.Text = "Editar Localidad";
                frm.SetLocalidad(localidad);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        localidad = frm.GetLocalidad();
                        servicio.Guardar(localidad);
                        SetearFila(r, localidad);
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
