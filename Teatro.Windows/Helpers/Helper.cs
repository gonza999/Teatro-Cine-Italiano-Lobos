using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Teatro.BussinesLayer.Entidades;
using Teatro.ServiceLayer.Facades;
using Teatro.ServiceLayer.Servicios;
using Teatro.Windows.Helpers.Enum;

namespace Teatro.Windows.Helpers
{
    public class Helper
    {
        public static void MensajeBox(string mensaje, Tipo tipo)
        {
            switch (tipo)
            {
                case Tipo.Success:
                    MessageBox.Show(mensaje, "Operación Exitosa", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    break;
                case Tipo.Error:
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    break;
                    //case Tipo.Warning:
                    //    break;
                    //case Tipo.Question:
                    //    break;
                    //default:
                    //    break;
            }
        }

        internal static void CargarTipoEventoComboBox(ref ComboBox cmbTipoEvento)
        {
            IServicioTipoEventos servicioTipoEventos = new ServicioTipoEventos();
            List<TipoEvento> listaTipoEvento = servicioTipoEventos.GetLista();
            var defaultTipoEvento = new TipoEvento() { TipoEventoId = 0, NombreTipoEvento = "-Seleccione Tipo de Evento-" };
            listaTipoEvento.Insert(0, defaultTipoEvento);
            cmbTipoEvento.DataSource = listaTipoEvento;
            cmbTipoEvento.DisplayMember = "NombreTipoEvento";
            cmbTipoEvento.ValueMember = "TipoEventoId";
            cmbTipoEvento.SelectedIndex = 0;
        }
        internal static void CargarTipoDocumentoComboBox(ref ComboBox cmbTipoDocumento) { 
            IServicioTiposDocumentos servicioTiposDocumentos = new ServicioTiposDocumentos();
            List<TipoDocumento> listaTipoDocumento = servicioTiposDocumentos.GetLista();
            var defaultTipoDocumento = new TipoDocumento() { TipoDocumentoId = 0, NombreTipoDocumento = "-Seleccione Tipo de Documento-" };
            listaTipoDocumento.Insert(0, defaultTipoDocumento);
            cmbTipoDocumento.DataSource = listaTipoDocumento;
            cmbTipoDocumento.DisplayMember = "NombreTipoDocumento";
            cmbTipoDocumento.ValueMember = "TipoDocumentoId";
            cmbTipoDocumento.SelectedIndex = 0;
        }
        internal static void CargarClasificacionComboBox(ref ComboBox cmbClasificacion)
        {
            IServicioClasificaciones servicioClasificacion = new ServicioClasificaciones();
            List<Clasificacion> listaClasificacion = servicioClasificacion.GetLista();
            var defaultClasificacion = new Clasificacion() { ClasificacionId = 0, NombreClasificacion = "-Seleccione Clasificacion-" };
            listaClasificacion.Insert(0, defaultClasificacion);
            cmbClasificacion.DataSource = listaClasificacion;
            cmbClasificacion.DisplayMember = "NombreClasificacion";
            cmbClasificacion.ValueMember = "ClasificacionId";
            cmbClasificacion.SelectedIndex = 0;
        }
        internal static void CargarDistribucionComboBox(ref ComboBox cmbDistribucion)
        { 
            IServicioDistribuciones servicioDistribucion = new ServicioDistribuciones();
            List<Distribucion> listaDistribucion = servicioDistribucion.GetLista();
            var defaultDistribucion = new Distribucion() { DistribucionId = 0, NombreDistribucion = "-Seleccione Distribucion-" };
            listaDistribucion.Insert(0, defaultDistribucion);
            cmbDistribucion.DataSource = listaDistribucion;
            cmbDistribucion.DisplayMember = "NombreDistribucion";
            cmbDistribucion.ValueMember = "DistribucionId";
            cmbDistribucion.SelectedIndex = 0;
        }
    }
}
