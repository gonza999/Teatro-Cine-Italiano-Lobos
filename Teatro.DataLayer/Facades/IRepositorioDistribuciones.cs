using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;

namespace Teatro.DataLayer.Facades
{
   public interface IRepositorioDistribuciones
    {
        Distribucion GetDistribucionPorId(int id);
        List<Distribucion> GetLista();
        void Guardar(Distribucion distribucion);
        void Borrar(int id);
        bool Existe(Distribucion distribucion);
        bool EstaRelacionado(Distribucion distribucion);
        Distribucion GetDistribucion(string nombreDistribucion);
        List<Distribucion> BuscarDistribucion(string distribucion);
    }
}
