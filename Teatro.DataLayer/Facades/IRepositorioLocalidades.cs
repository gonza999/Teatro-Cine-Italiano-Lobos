using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;

namespace Teatro.DataLayer.Facades
{
    public interface IRepositorioLocalidades
    {
        Localidad GetLocalidadPorId(int id);
        List<Localidad> GetLista();
        void Guardar(Localidad localidad);
        void Borrar(int id);
        bool Existe(Localidad localidad);
        bool EstaRelacionado(Localidad localidad);
        List<Localidad> BuscarLocalidad(string text);
        List<Localidad> GetLista(Ubicacion ubicacion);

        List<Localidad> GetLista(int fila);
    }
}
