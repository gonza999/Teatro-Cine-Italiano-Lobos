using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;

namespace Teatro.ServiceLayer.Facades
{
    public interface IServicioEventos
    {
        Evento GetEventoPorId(int id);
        List<Evento> GetLista();
        //List<Evento> GetLista(int marcaId);

        //List<Evento> GetLista(Categoria categoria);
        void Guardar(Evento evento);
        void Borrar(int id);
        bool Existe(Evento evento);
        bool EstaRelacionado(Evento evento);
        List<Evento> BuscarEvento(string text);
        void AnularEvento(int eventoId);
    }
}
