using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;

namespace Teatro.ServiceLayer.Facades
{
    public interface IServicioHorarios
    {
        List<Horario> GetLista();
        List<Horario> GetLista(Evento evento);

        void Guardar(Horario horario);
        void Borrar(Horario horario);
        bool Existe(Horario horario);
        bool EstaRelacionado(Horario horario);
        //List<Horario> BuscarHorario(string horario);
    }
}
