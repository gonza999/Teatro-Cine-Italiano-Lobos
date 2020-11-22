using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;

namespace Teatro.DataLayer.Facades
{
    public interface IRepositorioDistribucionesUbicaciones
    {
        List<DistribucionUbicacion> GetLista();
        List<DistribucionUbicacion> GetLista(Distribucion distribucion);

        void Guardar(DistribucionUbicacion distribucionUbicacion,bool opcion);
        void Borrar(int id);
        bool Existe(DistribucionUbicacion distribucionUbicacion);
        bool EstaRelacionado(DistribucionUbicacion distribucionUbicacion);
        List<DistribucionUbicacion> BuscarDistribucionUbicacion(string distribucionUbicacion);
    }
}
