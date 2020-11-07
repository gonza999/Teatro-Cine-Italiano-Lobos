using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;

namespace Teatro.ServiceLayer.Facades
{
    public interface IServicioUbicaciones
    {
        Ubicacion GetUbicacionPorId(int id);
        List<Ubicacion> GetLista();
        void Guardar(Ubicacion ubicacion);
        void Borrar(int id);
        bool Existe(Ubicacion ubicacion);
        bool EstaRelacionado(Ubicacion ubicacion);
        Ubicacion GetUbicacion(string nombreUbicacion);
        List<Ubicacion> BuscarUbicacion(string text);
    }
}
