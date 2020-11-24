using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;

namespace Teatro.DataLayer.Facades
{
    public interface IRepositorioTickets
    {
        List<Ticket> GetLista();
        //List<Ticket> GetLista(Distribucion distribucion);

        void Borrar(int id);
        bool Existe(Ticket ticket);
        bool Existe(Localidad localidad, Horario horario);
        bool EstaRelacionado(Ticket ticket);
        void Guardar(Ticket ticket);
    }
}
