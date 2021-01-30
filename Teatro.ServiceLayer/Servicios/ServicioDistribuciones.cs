using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    public class ServicioDistribuciones : IServicioDistribuciones
    {
        private ConexionBD _conexion;
        private IRepositorioDistribuciones _repositorio;
        private IRepositorioDistribucionesLocalidades repositorioDistribucionesLocalidades;
        private IRepositorioUbicaciones repositorioUbicaciones;
        private SqlTransaction transaction;
        public void Borrar(Distribucion distribucion)
        {
            try
            {
                _conexion = new ConexionBD();
                SqlConnection cn = _conexion.AbrirConexion();
                transaction = cn.BeginTransaction();
                repositorioDistribucionesLocalidades = new RepositorioDistribucionesLocalidades(cn, transaction);
                _repositorio = new RepositorioDistribuciones(cn, transaction);
                repositorioDistribucionesLocalidades.Borrar(distribucion.DistribucionId);
                _repositorio.Borrar(distribucion.DistribucionId);
                transaction.Commit();
                _conexion.CerrarConexion();

            }
            catch (Exception e)
            {
                transaction.Rollback();
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
            _conexion = new ConexionBD();
            SqlTransaction transaction = null;
            try
            {
                SqlConnection cn = _conexion.AbrirConexion();
                transaction = cn.BeginTransaction();
                repositorioUbicaciones = new RepositorioUbicaciones(cn, transaction);
                repositorioDistribucionesLocalidades = new RepositorioDistribucionesLocalidades(cn, _repositorio, repositorioUbicaciones, transaction);
                _repositorio = new RepositorioDistribuciones(_conexion.AbrirConexion(), transaction);
                var lista = _repositorio.GetLista();
                foreach (var d in lista)
                {
                    d.DistribucionLocalidad = repositorioDistribucionesLocalidades.GetLista(d);
                }
                transaction.Commit();
                _conexion.CerrarConexion();

                return lista;
            }
            catch (Exception e)
            {
                transaction.Rollback();
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
            _conexion = new ConexionBD();
            SqlTransaction transaction = null;
            try
            {
                SqlConnection cn = _conexion.AbrirConexion();
                transaction = cn.BeginTransaction();
                repositorioUbicaciones = new RepositorioUbicaciones(cn, transaction);
                repositorioDistribucionesLocalidades = new RepositorioDistribucionesLocalidades(cn, _repositorio, repositorioUbicaciones, transaction);
                _repositorio = new RepositorioDistribuciones(_conexion.AbrirConexion(), transaction);
                var d = _repositorio.GetDistribucionPorId(id);
                d.DistribucionLocalidad = repositorioDistribucionesLocalidades.GetLista(d);
                transaction.Commit();
                _conexion.CerrarConexion();
                return d;
            }
            catch (Exception e)
            {

                transaction.Rollback();
                throw new Exception(e.Message);
            }
        }

        public void Guardar(Distribucion distribucion)
        {
            _conexion = new ConexionBD();
            SqlTransaction transaction = null;
            try
            {
                SqlConnection cn = _conexion.AbrirConexion();
                transaction = cn.BeginTransaction();
                repositorioUbicaciones = new RepositorioUbicaciones(cn, transaction);
                repositorioDistribucionesLocalidades = new RepositorioDistribucionesLocalidades(cn, _repositorio, repositorioUbicaciones, transaction);
                _repositorio = new RepositorioDistribuciones(cn, transaction);
                _repositorio.Guardar(distribucion);
                foreach (var d in distribucion.DistribucionLocalidad)
                {
                    d.Distribucion = distribucion;
                    repositorioDistribucionesLocalidades.Guardar(d);
                }
                transaction.Commit();
                _conexion.CerrarConexion();

            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new Exception(e.Message);
            }
        }
    }
}
