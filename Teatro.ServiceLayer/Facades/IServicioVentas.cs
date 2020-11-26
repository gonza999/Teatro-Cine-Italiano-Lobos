using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;

namespace Teatro.ServiceLayer.Facades
{
    public interface IServicioVentas
    {
        Venta GetVentaPorId(int id);
        List<Venta> GetLista();
        void Guardar(Venta venta);
        void Borrar(int id);
        bool Existe(Venta venta);
        bool EstaRelacionado(Venta venta);
        Venta GetVenta(string nombreVenta);
        List<Venta> BuscarVenta(string text);
        void AnularVenta(int ventaId);
    }
}
