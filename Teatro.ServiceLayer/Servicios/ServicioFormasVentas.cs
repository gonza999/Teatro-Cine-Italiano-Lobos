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
    public class ServicioFormasVentas:IServicioFormasVentas
    {
        private ConexionBD _conexion;
        private IRepositorioFormasVentas _repositorio;
        public void Borrar(int id)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioFormasVentas(_conexion.AbrirConexion());
                _repositorio.Borrar(id);
                _conexion.CerrarConexion();

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<FormaVenta> BuscarFormaVenta(string formaVenta)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioFormasVentas(_conexion.AbrirConexion());
                var lista = _repositorio.BuscarFormaVenta(formaVenta);
                _conexion.CerrarConexion();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(FormaVenta formaVenta)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioFormasVentas(_conexion.AbrirConexion());
                var estaRelacionado = _repositorio.EstaRelacionado(formaVenta);
                _conexion.CerrarConexion();
                return estaRelacionado;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Existe(FormaVenta formaVenta)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioFormasVentas(_conexion.AbrirConexion());
                var existe = _repositorio.Existe(formaVenta);
                _conexion.CerrarConexion();
                return existe;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<FormaVenta> GetLista()
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioFormasVentas(_conexion.AbrirConexion());
                var lista = _repositorio.GetLista();
                _conexion.CerrarConexion();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public FormaVenta GetFormaVenta(string nombreFormaVenta)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioFormasVentas(_conexion.AbrirConexion());
                var formaVenta = _repositorio.GetFormaVenta(nombreFormaVenta);
                _conexion.CerrarConexion();
                return formaVenta;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public FormaVenta GetFormaVentaPorId(int id)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioFormasVentas(_conexion.AbrirConexion());
                var formaVenta = _repositorio.GetFormaVentaPorId(id);
                _conexion.CerrarConexion();
                return formaVenta;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Guardar(FormaVenta formaVenta)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioFormasVentas(_conexion.AbrirConexion());
                _repositorio.Guardar(formaVenta);
                _conexion.CerrarConexion();

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}

