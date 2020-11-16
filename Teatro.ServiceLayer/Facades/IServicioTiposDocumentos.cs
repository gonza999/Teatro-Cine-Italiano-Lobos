using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;

namespace Teatro.ServiceLayer.Facades
{
   public interface IServicioTiposDocumentos
    {
        TipoDocumento GetTipoDocumentoPorId(int id);
        List<TipoDocumento> GetLista();
        void Guardar(TipoDocumento tipoDocumento);
        void Borrar(int id);
        bool Existe(TipoDocumento tipoDocumento);
        bool EstaRelacionado(TipoDocumento tipoDocumento);
        TipoDocumento GetTipoDocumento(string nombreTipoDocumento);
        List<TipoDocumento> BuscarTipoDocumento(string text);
    }
}
