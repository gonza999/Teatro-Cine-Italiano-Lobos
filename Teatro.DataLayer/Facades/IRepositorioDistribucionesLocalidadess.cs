using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;

namespace Teatro.DataLayer.Facades
{
    public interface IRepositorioDistribucionesLocalidades
    {
        List<DistribucionLocalidad> GetLista();
        List<DistribucionLocalidad> GetLista(Distribucion distribucion);

        void Guardar(DistribucionLocalidad distribucionLocalidad);
        void Borrar(int id);
        bool Existe(DistribucionLocalidad distribucionLocalidad);
        bool EstaRelacionado(DistribucionLocalidad distribucionLocalidad);
        List<DistribucionLocalidad> BuscarDistribucionLocalidad(string distribucionLocalidad);
    }
}
