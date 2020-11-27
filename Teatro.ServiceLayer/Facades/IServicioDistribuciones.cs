using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;

namespace Teatro.ServiceLayer.Facades
{
    public interface IServicioDistribuciones
    {
        Distribucion GetDistribucionPorId(int id);
        List<Distribucion> GetLista();
        void Guardar(Distribucion distribucion);
        void Borrar(Distribucion distribucion);
        bool Existe(Distribucion distribucion);
        bool EstaRelacionado(Distribucion distribucion);
        Distribucion GetDistribucion(string nombreDistribucion);
        List<Distribucion> BuscarDistribucion(string text);
    }
}
