using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;
using Teatro.DataLayer;
using Teatro.DataLayer.Facades;
using Teatro.DataLayer.Repositorios;
using Teatro.ServiceLayer.Facades;

namespace Teatro.ServiceLayer.Servicios
{
    public class ServicioEmpleados:IServicioEmpleados
    {
        private IRepositorioEmpleados repositorio;
        private ConexionBD conexion;
        private IRepositorioTiposDocumentos repositorioTiposDocumentos;
        public ServicioEmpleados()
        {

        }
        public void Borrar(int id)
        {
            try
            {
                conexion = new ConexionBD();
                repositorioTiposDocumentos = new RepositorioTiposDocumentos(conexion.AbrirConexion());
                repositorio = new RepositorioEmpleados(conexion.AbrirConexion(), repositorioTiposDocumentos);
                repositorio.Borrar(id);
                conexion.CerrarConexion();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Empleado empleado)
        {
            try
            {
                conexion = new ConexionBD();
                repositorioTiposDocumentos = new RepositorioTiposDocumentos(conexion.AbrirConexion());
                repositorio = new RepositorioEmpleados(conexion.AbrirConexion(), repositorioTiposDocumentos);
                var relacionado = repositorio.EstaRelacionado(empleado);
                conexion.CerrarConexion();
                return relacionado;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Existe(Empleado empleado)
        {
            try
            {
                conexion = new ConexionBD();
                repositorioTiposDocumentos = new RepositorioTiposDocumentos(conexion.AbrirConexion());
                repositorio = new RepositorioEmpleados(conexion.AbrirConexion(), repositorioTiposDocumentos);
                var existe = repositorio.Existe(empleado);
                conexion.CerrarConexion();
                return existe;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<Empleado> GetLista()
        {
            try
            {
                conexion = new ConexionBD();
                repositorioTiposDocumentos = new RepositorioTiposDocumentos(conexion.AbrirConexion());
                repositorio = new RepositorioEmpleados(conexion.AbrirConexion(), repositorioTiposDocumentos);
                var lista = repositorio.GetLista();
                conexion.CerrarConexion();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        //public List<Empleado> GetLista(int tiposDocumentoId)
        //{
        //    try
        //    {
        //        conexion = new ConexionBD();
        //        repositorioTiposDocumentos = new RepositorioTiposDocumentos(conexion.AbrirConexion());
        //        repositorioTipoEmpleados = new RepositorioTipoEmpleado(conexion.AbrirConexion());
        //        repositorio = new RepositorioEmpleados(conexion.AbrirConexion(),repositorioTipoEmpleados,repositorioTiposDocumentos);
        //        var lista = repositorio.GetLista(tiposDocumentoId);
        //        conexion.CerrarConexion();
        //        return lista;
        //    }
        //    catch (Exception e)
        //    {

        //        throw new Exception(e.Message);
        //    }
        //}

        //public List<Empleado> GetLista(TipoEmpleado tipoEmpleado)
        //{
        //    try
        //    {
        //        conexion = new ConexionBd();
        //        repositorioTiposDocumentos = new RepositorioTiposDocumentos(conexion.AbrirConexion());
        //        repositorioTipoEmpleados = new RepositorioTipoEmpleados(conexion.AbrirConexion());
        //        repositorioMedidas = new RepositorioMedidas(conexion.AbrirConexion());
        //        repositorio = new RepositorioEmpleados(conexion.AbrirConexion(), repositorioTiposDocumentos, repositorioTipoEmpleados, repositorioMedidas);
        //        var lista = repositorio.GetLista(tipoEmpleado);
        //        conexion.CerrarConexion();
        //        return lista;
        //    }
        //    catch (Exception e)
        //    {

        //        throw new Exception(e.Message);
        //    }
        //}

        //public List<Empleado> GetLista(string descripcion)
        //{
        //    try
        //    {
        //        conexion = new ConexionBd();
        //        repositorioTiposDocumentos = new RepositorioTiposDocumentos(conexion.AbrirConexion());
        //        repositorioTipoEmpleados = new RepositorioTipoEmpleados(conexion.AbrirConexion());
        //        repositorioMedidas = new RepositorioMedidas(conexion.AbrirConexion());
        //        repositorio = new RepositorioEmpleados(conexion.AbrirConexion(), repositorioTiposDocumentos, repositorioTipoEmpleados, repositorioMedidas);
        //        var lista = repositorio.GetLista(descripcion);
        //        conexion.CerrarConexion();
        //        return lista;
        //    }
        //    catch (Exception e)
        //    {

        //        throw new Exception(e.Message);
        //    }
        //}

        //public Empleado GetEmpleadoPorCodigoDeBarras(string codigo)
        //{
        //    try
        //    {
        //        conexion = new ConexionBd();
        //        repositorioTiposDocumentos = new RepositorioTiposDocumentos(conexion.AbrirConexion());
        //        repositorioTipoEmpleados = new RepositorioTipoEmpleados(conexion.AbrirConexion());
        //        repositorioMedidas = new RepositorioMedidas(conexion.AbrirConexion());
        //        repositorio = new RepositorioEmpleados(conexion.AbrirConexion(), repositorioTiposDocumentos, repositorioTipoEmpleados, repositorioMedidas);
        //        var empleado = repositorio.GetEmpleadoPorCodigoDeBarras(codigo);
        //        conexion.CerrarConexion();
        //        return empleado;
        //    }
        //    catch (Exception e)
        //    {

        //        throw new Exception(e.Message);
        //    }
        //}

        public Empleado GetEmpleadoPorId(int id)
        {
            try
            {
                conexion = new ConexionBD();
                repositorioTiposDocumentos = new RepositorioTiposDocumentos(conexion.AbrirConexion());
                repositorio = new RepositorioEmpleados(conexion.AbrirConexion(), repositorioTiposDocumentos);
                var empleado = repositorio.GetEmpleadoPorId(id);
                conexion.CerrarConexion();
                return empleado;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Guardar(Empleado empleado)
        {
            try
            {
                conexion = new ConexionBD();
                repositorio = new RepositorioEmpleados(conexion.AbrirConexion());
                repositorio.Guardar(empleado);
                conexion.CerrarConexion();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<Empleado> BuscarEmpleado(string text)
        {
            try
            {
                List<Empleado> lista = new List<Empleado>();
                conexion = new ConexionBD();
                repositorio = new RepositorioEmpleados(conexion.AbrirConexion(), repositorioTiposDocumentos);
                lista = repositorio.BuscarEmpleado(text);
                conexion.CerrarConexion();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
