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
    public class ServicioDistribuciones:IServicioDistribuciones
    {
        private ConexionBD _conexion;
        private IRepositorioDistribuciones _repositorio;
        public void Borrar(int id)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioDistribuciones(_conexion.AbrirConexion());
                _repositorio.Borrar(id);
                _conexion.CerrarConexion();

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<Distribucion> BuscarDistribucion(string distribucion)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioDistribuciones(_conexion.AbrirConexion());
                var lista = _repositorio.BuscarDistribucion(distribucion);
                _conexion.CerrarConexion();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Distribucion distribucion)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioDistribuciones(_conexion.AbrirConexion());
                var estaRelacionado = _repositorio.EstaRelacionado(distribucion);
                _conexion.CerrarConexion();
                return estaRelacionado;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Existe(Distribucion distribucion)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioDistribuciones(_conexion.AbrirConexion());
                var existe = _repositorio.Existe(distribucion);
                _conexion.CerrarConexion();
                return existe;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<Distribucion> GetLista()
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioDistribuciones(_conexion.AbrirConexion());
                var lista = _repositorio.GetLista();
                _conexion.CerrarConexion();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Distribucion GetDistribucion(string nombreDistribucion)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioDistribuciones(_conexion.AbrirConexion());
                var distribucion = _repositorio.GetDistribucion(nombreDistribucion);
                _conexion.CerrarConexion();
                return distribucion;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Distribucion GetDistribucionPorId(int id)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioDistribuciones(_conexion.AbrirConexion());
                var distribucion = _repositorio.GetDistribucionPorId(id);
                _conexion.CerrarConexion();
                return distribucion;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Guardar(Distribucion distribucion)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioDistribuciones(_conexion.AbrirConexion());
                _repositorio.Guardar(distribucion);
                _conexion.CerrarConexion();

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
