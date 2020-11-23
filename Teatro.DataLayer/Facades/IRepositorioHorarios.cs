using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;

namespace Teatro.DataLayer.Facades
{
    public interface IRepositorioHorarios
    {
        List<Horario> GetLista();
        //List<Horario> GetLista(Distribucion distribucion);

        void Borrar(int id);
        bool Existe(Horario horario);
        bool EstaRelacionado(Horario horario);
        List<Horario> GetLista(Evento evento);
        void Guardar(Horario horario);
        //List<Horario> BuscarHorario(string horario);
    }
}
