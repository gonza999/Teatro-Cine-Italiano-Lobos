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
    public class ServicioPlantas:IServicioPlantas
    {
        private ConexionBD _conexion;
        private IRepositorioPlantas _repositorio;
        public void Borrar(int id)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioPlantas(_conexion.AbrirConexion());
                _repositorio.Borrar(id);
                _conexion.CerrarConexion();

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<Planta> BuscarPlanta(string planta)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioPlantas(_conexion.AbrirConexion());
                var lista = _repositorio.BuscarPlanta(planta);
                _conexion.CerrarConexion();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Planta planta)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioPlantas(_conexion.AbrirConexion());
                var estaRelacionado = _repositorio.EstaRelacionado(planta);
                _conexion.CerrarConexion();
                return estaRelacionado;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Existe(Planta planta)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioPlantas(_conexion.AbrirConexion());
                var existe = _repositorio.Existe(planta);
                _conexion.CerrarConexion();
                return existe;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<Planta> GetLista()
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioPlantas(_conexion.AbrirConexion());
                var lista = _repositorio.GetLista();
                _conexion.CerrarConexion();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Planta GetPlanta(string nombrePlanta)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioPlantas(_conexion.AbrirConexion());
                var planta = _repositorio.GetPlanta(nombrePlanta);
                _conexion.CerrarConexion();
                return planta;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Planta GetPlantaPorId(int id)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioPlantas(_conexion.AbrirConexion());
                var planta = _repositorio.GetPlantaPorId(id);
                _conexion.CerrarConexion();
                return planta;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Guardar(Planta planta)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioPlantas(_conexion.AbrirConexion());
                _repositorio.Guardar(planta);
                _conexion.CerrarConexion();

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
