using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;

namespace Teatro.DataLayer.Facades
{
    public interface IRepositorioEventos
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
        //void ActualizarStock(Evento evento, decimal cantidad);
        //Evento GetEventoPorCodigoDeBarras(string codigo);
        //List<Evento> GetLista(string descripcion);
    }
}
