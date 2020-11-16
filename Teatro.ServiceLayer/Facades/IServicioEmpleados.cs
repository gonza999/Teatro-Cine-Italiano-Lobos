using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;

namespace Teatro.ServiceLayer.Facades
{
    public interface IServicioEmpleados
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
    }
}
