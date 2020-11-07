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

        //internal static void CargarTipoEventoComboBox(ref ComboBox cmbTipoEvento)
        //{
        //    IServicioTipoEventos servicioTipoEventos = new ServicioTipoEventos();
        //    List<TipoEvento> listaMedidas = servicioTipoEventos.GetLista();
        //    var defaultCategorias = new TipoEvento() { TipoEventoId = 0, NombreTipoEvento = "-Seleccione TipoEvento-" };
        //    listaMedidas.Insert(0, defaultCategorias);
        //    cmbTipoEvento.DataSource = listaMedidas;
        //    cmbTipoEvento.DisplayMember = "NombreTipoEvento";
        //    cmbTipoEvento.ValueMember = "TipoEventoId";
        //    cmbTipoEvento.SelectedIndex = 0;
        //}
    }
}
