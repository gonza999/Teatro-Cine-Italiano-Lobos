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

        internal static void CargarEmpleadoComboBox(ref ComboBox cmbEmpleado)
        {
            IServicioEmpleados servicioEmpleados = new ServicioEmpleados();
            List<Empleado> listaEmpleado = servicioEmpleados.GetLista();
            var defaultEmpleado = new Empleado() { EmpleadoId = 0, Nombre = "-Seleccione Empleado-" };
            listaEmpleado.Insert(0, defaultEmpleado);
            cmbEmpleado.DataSource = listaEmpleado;
            cmbEmpleado.DisplayMember = "Nombre";
            cmbEmpleado.ValueMember = "EmpleadoId";
            cmbEmpleado.SelectedIndex = 0;
        }

        internal static void CargarHorarioComboBox(ref ComboBox cmbHorario, Evento evento)
        {
            IServicioHorarios servicioHorarios = new ServicioHorarios();
            List<Horario> listaHorario = servicioHorarios.GetLista(evento);
            //var defaultHorario = new Horario() { HorarioId = 0, Fecha = "-Seleccione Horario-" };
            //listaHorario.Insert(0, defaultHorario);
            cmbHorario.DataSource = listaHorario;
            cmbHorario.DisplayMember="Fecha";
            cmbHorario.ValueMember = "HorarioId";
            //cmbHorario.SelectedIndex = 0;
        }

        internal static void CargarLocalidadComboBox(ref ComboBox cmbLocalidad, Ubicacion ubicacion)
        {
            IServicioLocalidades servicioLocalidades = new ServicioLocalidades();
            List<Localidad> listaLocalidad = servicioLocalidades.GetLista(ubicacion);
            //var defaultLocalidad = new Localidad() { LocalidadId = 0, Numero = "-Seleccione Localidad-" };
            //listaLocalidad.Insert(0, defaultLocalidad);
            cmbLocalidad.DataSource = listaLocalidad;
            cmbLocalidad.DisplayMember = "Numero";
            cmbLocalidad.ValueMember = "LocalidadId";
            //cmbLocalidad.SelectedIndex = 0;
        }

        internal static void CargarFormaPagoComboBox(ref ComboBox cmbFormaPago)
        {
            IServicioFormasPagos servicioFormaPago = new ServicioFormasPagos();
            List<FormaPago> listaFormaPago = servicioFormaPago.GetLista();
            var defaultFormaPago = new FormaPago() { FormaPagoId = 0, NombreFormaPago = "-Seleccione Forma de Pago-" };
            listaFormaPago.Insert(0, defaultFormaPago);
            cmbFormaPago.DataSource = listaFormaPago;
            cmbFormaPago.DisplayMember = "NombreFormaPago";
            cmbFormaPago.ValueMember = "FormaPagoId";
            cmbFormaPago.SelectedIndex = 0;
        }
        internal static void CargarFormaVentaComboBox(ref ComboBox cmbFormaVenta)
        {
            IServicioFormasVentas servicioFormaVenta = new ServicioFormasVentas();
            List<FormaVenta> listaFormaVenta = servicioFormaVenta.GetLista();
            var defaultFormaVenta = new FormaVenta() { FormaVentaId = 0, NombreFormaVenta = "-Seleccione Forma de Venta-" };
            listaFormaVenta.Insert(0, defaultFormaVenta);
            cmbFormaVenta.DataSource = listaFormaVenta;
            cmbFormaVenta.DisplayMember = "NombreFormaVenta";
            cmbFormaVenta.ValueMember = "FormaVentaId";
            cmbFormaVenta.SelectedIndex = 0;
        }
        internal static void CargarEventoComboBox(ref ComboBox cmbEvento)
        {
            IServicioEventos servicioEvento = new ServicioEventos();
            List<Evento> listaEvento = servicioEvento.GetLista();
            var defaultEvento = new Evento() { EventoId = 0, NombreEvento = "-Seleccione Evento-" };
            listaEvento.Insert(0, defaultEvento);
            cmbEvento.DataSource = listaEvento;
            cmbEvento.DisplayMember = "NombreEvento";
            cmbEvento.ValueMember = "EventoId";
            cmbEvento.SelectedIndex = 0;
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
        internal static void CargarPlantaComboBox(ref ComboBox cmbPlanta)
        {
            IServicioPlantas servicioPlanta = new ServicioPlantas();
            List<Planta> listaPlanta = servicioPlanta.GetLista();
            var defaultPlanta = new Planta() { PlantaId = 0, NombrePlanta = "-Seleccione Planta-" };
            listaPlanta.Insert(0, defaultPlanta);
            cmbPlanta.DataSource = listaPlanta;
            cmbPlanta.DisplayMember = "NombrePlanta";
            cmbPlanta.ValueMember = "PlantaId";
            cmbPlanta.SelectedIndex = 0;
        }

        internal static void CargarUbicacionComboBox(ref ComboBox cmbUbicacion)
        {
            IServicioUbicaciones servicioPlanta = new ServicioUbicaciones();
            List<Ubicacion> listaUbicacion = servicioPlanta.GetLista();
            var defaultUbicacion = new Ubicacion() { UbicacionId = 0, NombreUbicacion = "-Seleccione Ubicacion-" };
            listaUbicacion.Insert(0, defaultUbicacion);
            cmbUbicacion.DataSource = listaUbicacion;
            cmbUbicacion.DisplayMember = "NombreUbicacion";
            cmbUbicacion.ValueMember = "UbicacionId";
            cmbUbicacion.SelectedIndex = 0;
        }
    }
}
