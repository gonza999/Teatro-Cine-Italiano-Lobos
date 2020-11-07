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
    public class ServicioUbicaciones:IServicioUbicaciones
    {
        private ConexionBD _conexion;
        private IRepositorioUbicaciones _repositorio;
        public void Borrar(int id)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioUbicaciones(_conexion.AbrirConexion());
                _repositorio.Borrar(id);
                _conexion.CerrarConexion();

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<Ubicacion> BuscarUbicacion(string ubicacion)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioUbicaciones(_conexion.AbrirConexion());
                var lista = _repositorio.BuscarUbicacion(ubicacion);
                _conexion.CerrarConexion();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Ubicacion ubicacion)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioUbicaciones(_conexion.AbrirConexion());
                var estaRelacionado = _repositorio.EstaRelacionado(ubicacion);
                _conexion.CerrarConexion();
                return estaRelacionado;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Existe(Ubicacion ubicacion)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioUbicaciones(_conexion.AbrirConexion());
                var existe = _repositorio.Existe(ubicacion);
                _conexion.CerrarConexion();
                return existe;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<Ubicacion> GetLista()
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioUbicaciones(_conexion.AbrirConexion());
                var lista = _repositorio.GetLista();
                _conexion.CerrarConexion();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Ubicacion GetUbicacion(string nombreUbicacion)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioUbicaciones(_conexion.AbrirConexion());
                var ubicacion = _repositorio.GetUbicacion(nombreUbicacion);
                _conexion.CerrarConexion();
                return ubicacion;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Ubicacion GetUbicacionPorId(int id)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioUbicaciones(_conexion.AbrirConexion());
                var ubicacion = _repositorio.GetUbicacionPorId(id);
                _conexion.CerrarConexion();
                return ubicacion;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Guardar(Ubicacion ubicacion)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioUbicaciones(_conexion.AbrirConexion());
                _repositorio.Guardar(ubicacion);
                _conexion.CerrarConexion();

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
