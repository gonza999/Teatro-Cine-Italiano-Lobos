using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;

namespace Teatro.ServiceLayer.Facades
{
    public interface IServicioLocalidades
    {
        Localidad GetLocalidadPorId(int id);
        List<Localidad> GetLista();
        void Guardar(Localidad localidad);
        void Borrar(int id);
        bool Existe(Localidad localidad);
        bool EstaRelacionado(Localidad localidad);
        //Localidad GetLocalidad(string nombreLocalidad);
        List<Localidad> BuscarLocalidad(string text);
    }
}
