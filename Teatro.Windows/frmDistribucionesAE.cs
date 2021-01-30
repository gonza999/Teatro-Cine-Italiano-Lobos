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
    public partial class frmDistribucionesAE : Form
    {
        public frmDistribucionesAE(frmDistribuciones frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        public frmDistribucionesAE()
        {
            InitializeComponent();
            frm = null;
        }

        private frmDistribuciones frm;
        private bool esEdicion = false;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            servicio = new ServicioDistribuciones();
            dgvDatos.Rows.Clear();
            cmbFila.SelectedIndex = 0;
            if (distribucion != null)
            {
                txtDistribucion.Text = distribucion.NombreDistribucion;
                foreach (var d in distribucion.DistribucionLocalidad)
                {
                    foreach (var l in d.Localidades)
                    {
                        if (l.Ubicacion.UbicacionId==2)
                        {
                            txtPalcos.Text = d.Precio.ToString();
                        }
                        else
                        {
                            CargarGrilla(d);
                        }
                    }
                }



                esEdicion = true;
            }
        }

        private void CargarGrilla(DistribucionLocalidad d)
        {
            AgregarFila(d);
        }
        //private void Actualizar()
        //{
        //    try
        //    {
        //        servicio = new ServicioPlantas();
        //        lista = servicio.GetLista();
        //        MostrarDatosEnGrilla();
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(this, ex.Message, "Error",
        //            MessageBoxButtons.OK);
        //    }
        //}

        //private void MostrarDatosEnGrilla()
        //{
        //    dgvDatos.Rows.Clear();
        //    foreach (var planta in lista)
        //    {
        //        AgregarFila(planta);
        //    }
        //}
        private List<DistribucionLocalidad> DistribucionLocalidades = new List<DistribucionLocalidad>();
        public void AgregarFila(DistribucionLocalidad distribucionLocalidad)
        {
            DataGridViewRow r = ConstruirFila();
            SetearFila(r, distribucionLocalidad);
            AgregarFila(r);
            DistribucionLocalidades.Add(distribucionLocalidad);
        }

        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, DistribucionLocalidad distribucionLocalidad)
        {
            r.Cells[cmnPrecio.Index].Value = distribucionLocalidad.Precio;
            var fila = distribucionLocalidad.Localidades[0].Fila;
            r.Cells[cmnFila.Index].Value=fila;
            r.Tag = distribucionLocalidad;
        }

        private DataGridViewRow ConstruirFila()
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }

        private Distribucion distribucion;
        public void SetDistribucion(Distribucion distribucion)
        {
            this.distribucion = distribucion;
        }

        public Distribucion GetDistribucion()
        {
            return distribucion;
        }



        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (string.IsNullOrEmpty(txtDistribucion.Text.Trim()) &&
                string.IsNullOrWhiteSpace(txtDistribucion.Text.Trim()))
            {
                valido = false;
                errorProvider1.SetError(txtDistribucion, "Debe ingresar un nombre de distribucion");
            }
            decimal precio;
            if (string.IsNullOrEmpty(txtPalcos.Text.Trim()) &&
           string.IsNullOrWhiteSpace(txtPalcos.Text.Trim()))
            {
                valido = false;
                errorProvider1.SetError(txtPalcos, "Debe ingresar un precio de palco");
            }
            precio = 0;
            if (!decimal.TryParse(txtPalcos.Text, out precio))
            {
                valido = false;
                errorProvider1.SetError(txtPalcos, "Debe ingresar un precio de palco valido");
            }
            if (precio < 0)
            {
                valido = false;
                errorProvider1.SetError(txtPalcos, "El precio no debe ser menor a cero");

            }
            if (dgvDatos.Rows.Count!=20)
            {
                valido = false;
                errorProvider1.SetError(btnAgregar, "Debe ingresar un precio a cada fila");
            }
            if (DistribucionLocalidades.Count != 20)
            {
                valido = false;
                errorProvider1.SetError(btnAgregar, "Debe ingresar un precio a cada fila");
            }
            return valido;
        }

        private void btnGuardar_Click(object sender, System.EventArgs e)
        {
            if (ValidarDatos())
            {

                if (distribucion == null)
                {
                    distribucion = new Distribucion();
                }
                distribucion.NombreDistribucion = txtDistribucion.Text;
                distribucion.DistribucionLocalidad.Clear();
                distribucion.DistribucionLocalidad = DistribucionLocalidades;

                DistribucionLocalidad distribucionPalcos = new DistribucionLocalidad();
                distribucionPalcos.Precio =decimal.Parse (txtPalcos.Text);
                Ubicacion palcos = new Ubicacion();
                palcos.UbicacionId = 2;
                distribucionPalcos.Localidades = servicioLocalidades.GetLista(palcos);

                distribucion.DistribucionLocalidad.Add(distribucionPalcos);
                if (ValidarObjeto())
                {
                    if (!esEdicion)
                    {
                        servicio.Guardar(distribucion);
                        if (frm != null)
                        {
                            frm.AgregarFila(distribucion);
                        }
                        Helper.MensajeBox("Registro guardado", Tipo.Success);
                        DialogResult dr = MessageBox.Show("Desea agregar otro registro?", "Confirmar",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.No)
                        {
                            DialogResult = DialogResult.Cancel;
                        }
                        else
                        {
                            InicializarControles();
                        }
                    }
                    else
                    {
                        DialogResult = DialogResult.OK;
                    }

                }
            }

        }

        private void InicializarControles()
        {
            txtDistribucion.Clear();
            txtButacas.Clear();
            txtPalcos.Clear();
            txtDistribucion.Focus();
            distribucion = null;
            dgvDatos.Rows.Clear();
            cmbFila.SelectedIndex = 0;
            DistribucionLocalidades.Clear();
        }

        private IServicioDistribuciones servicio;
        private bool ValidarObjeto()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (servicio.Existe(distribucion))
            {
                errorProvider1.SetError(txtDistribucion, "Distribucion repetida");
                valido = false;
            }
            return valido;
        }

        private void btnCancelar_Click(object sender, System.EventArgs e)
        {
            InicializarControles();
            DialogResult = DialogResult.Cancel;

        }
        private IServicioLocalidades servicioLocalidades = new ServicioLocalidades();
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            DistribucionLocalidad distribucionLocalidad = new DistribucionLocalidad();
            if (ValidarPrecio())
            {
                if (ValidarFila())
                {
                    distribucionLocalidad.Precio = decimal.Parse(txtButacas.Text);
                    int fila = cmbFila.SelectedIndex;
                    switch (fila)
                    {
                        case 0:
                            distribucionLocalidad.Localidades = servicioLocalidades.GetLista(fila + 1);
                            break;
                        case 1:
                            distribucionLocalidad.Localidades = servicioLocalidades.GetLista(fila + 1);
                            break;
                        case 2:
                            distribucionLocalidad.Localidades = servicioLocalidades.GetLista(fila + 1);
                            break;
                        case 3:
                            distribucionLocalidad.Localidades = servicioLocalidades.GetLista(fila + 1);
                            break;
                        case 4:
                            distribucionLocalidad.Localidades = servicioLocalidades.GetLista(fila + 1);
                            break;
                        case 5:
                            distribucionLocalidad.Localidades = servicioLocalidades.GetLista(fila + 1);
                            break;
                        case 6:
                            distribucionLocalidad.Localidades = servicioLocalidades.GetLista(fila + 1);
                            break;
                        case 7:
                            distribucionLocalidad.Localidades = servicioLocalidades.GetLista(fila + 1);
                            break;
                        case 8:
                            distribucionLocalidad.Localidades = servicioLocalidades.GetLista(fila + 1);
                            break;
                        case 9:
                            distribucionLocalidad.Localidades = servicioLocalidades.GetLista(fila + 1);
                            break;
                        case 10:
                            distribucionLocalidad.Localidades = servicioLocalidades.GetLista(fila + 1);
                            break;
                        case 11:
                            distribucionLocalidad.Localidades = servicioLocalidades.GetLista(fila + 1);
                            break;
                        case 12:
                            distribucionLocalidad.Localidades = servicioLocalidades.GetLista(fila + 1);
                            break;
                        case 13:
                            distribucionLocalidad.Localidades = servicioLocalidades.GetLista(fila + 1);
                            break;
                        case 14:
                            distribucionLocalidad.Localidades = servicioLocalidades.GetLista(fila + 1);
                            break;
                        case 15:
                            distribucionLocalidad.Localidades = servicioLocalidades.GetLista(fila + 1);
                            break;
                        case 16:
                            distribucionLocalidad.Localidades = servicioLocalidades.GetLista(fila + 1);
                            break;
                        case 17:
                            distribucionLocalidad.Localidades = servicioLocalidades.GetLista(fila + 1);
                            break;
                        case 18:
                            distribucionLocalidad.Localidades = servicioLocalidades.GetLista(fila + 1);
                            break;
                        case 19:
                            distribucionLocalidad.Localidades = servicioLocalidades.GetLista(fila + 1);
                            break;
                        default:
                            break;
                    }
                    CargarGrilla(distribucionLocalidad); 
                }
            }
        }

        private bool ValidarFila()
        {
            errorProvider1.Clear();
            bool valido = true;
            foreach (var d in DistribucionLocalidades)
            {
                if (d.Localidades[0].Fila==cmbFila.SelectedIndex+1)
                {
                    valido = false;
                    errorProvider1.SetError(cmbFila,"Ya le han asignado precio a esta fila");
                }
            }
            return valido;
        }

        private bool ValidarPrecio()
        {
            errorProvider1.Clear();
            bool valido = true;
            decimal precio;
            if (!decimal.TryParse(txtButacas.Text,out precio))
            {
                valido = false;
                errorProvider1.SetError(txtButacas,"Debe ingresar un precio valido");
            }
            return valido;
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                DataGridViewRow r = dgvDatos.Rows[e.RowIndex];
                DistribucionLocalidad distribucionLocalidad = (DistribucionLocalidad)r.Tag;
                DistribucionLocalidades.Remove(distribucionLocalidad);
                dgvDatos.Rows.RemoveAt(e.RowIndex);
            }
        }
    }
}
