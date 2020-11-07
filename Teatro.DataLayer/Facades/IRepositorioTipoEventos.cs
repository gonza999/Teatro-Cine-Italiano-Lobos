using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;

namespace Teatro.DataLayer.Facades
{
    public interface IRepositorioTipoEventos
    {
        TipoEvento GetTipoEventoPorId(int id);
        List<TipoEvento> GetLista();
        void Guardar(TipoEvento tipoevento);
        void Borrar(int id);
        bool Existe(TipoEvento tipoevento);
        bool EstaRelacionado(TipoEvento tipoevento);
        TipoEvento GetTipoEvento(string nombreTipoEvento);
        List<TipoEvento> BuscarTipoEvento(string tipoevento);
    }
}
