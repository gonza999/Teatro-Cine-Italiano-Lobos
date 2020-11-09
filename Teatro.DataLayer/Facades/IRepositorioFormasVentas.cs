using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;

namespace Teatro.DataLayer.Facades
{
    public interface IRepositorioFormasVentas
    {
        FormaVenta GetFormaVentaPorId(int id);
        List<FormaVenta> GetLista();
        void Guardar(FormaVenta formaVenta);
        void Borrar(int id);
        bool Existe(FormaVenta formaVenta);
        bool EstaRelacionado(FormaVenta formaVenta);
        FormaVenta GetFormaVenta(string nombreFormaVenta);
        List<FormaVenta> BuscarFormaVenta(string formaVenta);
    }
}
