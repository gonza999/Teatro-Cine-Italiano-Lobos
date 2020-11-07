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
    public class ServicioClasificiones:IServicioClasificaciones
    {
        private ConexionBD _conexion;
        private IRepositorioClasificaciones _repositorio;
        public void Borrar(int id)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioClasificaciones(_conexion.AbrirConexion());
                _repositorio.Borrar(id);
                _conexion.CerrarConexion();

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<Clasificacion> BuscarClasificacion(string clasificacion)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioClasificaciones(_conexion.AbrirConexion());
                var lista = _repositorio.BuscarClasificacion(clasificacion);
                _conexion.CerrarConexion();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Clasificacion clasificacion)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioClasificaciones(_conexion.AbrirConexion());
                var estaRelacionado = _repositorio.EstaRelacionado(clasificacion);
                _conexion.CerrarConexion();
                return estaRelacionado;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Existe(Clasificacion clasificacion)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioClasificaciones(_conexion.AbrirConexion());
                var existe = _repositorio.Existe(clasificacion);
                _conexion.CerrarConexion();
                return existe;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<Clasificacion> GetLista()
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioClasificaciones(_conexion.AbrirConexion());
                var lista = _repositorio.GetLista();
                _conexion.CerrarConexion();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Clasificacion GetClasificacion(string nombreClasificacion)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioClasificaciones(_conexion.AbrirConexion());
                var clasificacion = _repositorio.GetClasificacion(nombreClasificacion);
                _conexion.CerrarConexion();
                return clasificacion;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Clasificacion GetClasificacionPorId(int id)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioClasificaciones(_conexion.AbrirConexion());
                var clasificacion = _repositorio.GetClasificacionPorId(id);
                _conexion.CerrarConexion();
                return clasificacion;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Guardar(Clasificacion clasificacion)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioClasificaciones(_conexion.AbrirConexion());
                _repositorio.Guardar(clasificacion);
                _conexion.CerrarConexion();

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}

