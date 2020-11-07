using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;

namespace Teatro.ServiceLayer.Facades
{
    public interface IServicioClasificaciones
    {
        Clasificacion GetClasificacionPorId(int id);
        List<Clasificacion> GetLista();
        void Guardar(Clasificacion clasificacion);
        void Borrar(int id);
        bool Existe(Clasificacion clasificacion);
        bool EstaRelacionado(Clasificacion clasificacion);
        Clasificacion GetClasificacion(string nombreClasificacion);
        List<Clasificacion> BuscarClasificacion(string text);
    }
}
