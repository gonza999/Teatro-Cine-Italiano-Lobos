using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;

namespace Teatro.DataLayer.Facades
{
    public interface IRepositorioEmpleados
    {
        Empleado GetEmpleadoPorId(int id);
        List<Empleado> GetLista();
        //List<Empleado> GetLista(int marcaId);

        //List<Empleado> GetLista(Categoria categoria);
        void Guardar(Empleado empleado);
        void Borrar(int id);
        bool Existe(Empleado empleado);
        bool EstaRelacionado(Empleado empleado);
        List<Empleado> BuscarEmpleado(string text);
        //void ActualizarStock(Empleado empleado, decimal cantidad);
        //Empleado GetEmpleadoPorCodigoDeBarras(string codigo);
        //List<Empleado> GetLista(string descripcion);
    }
}
