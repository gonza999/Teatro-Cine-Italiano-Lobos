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
    public class ServicioLocalidades:IServicioLocalidades
    {
        private IRepositorioLocalidades repositorio;
        private ConexionBD conexion;
        private IRepositorioPlantas repositorioPlantas;
        private IRepositorioUbicaciones repositorioUbicaciones;
        public ServicioLocalidades()
        {

        }
        public void Borrar(int id)
        {
            try
            {
                conexion = new ConexionBD();
                repositorioPlantas = new RepositorioPlantas(conexion.AbrirConexion());
                repositorioUbicaciones = new RepositorioUbicaciones(conexion.AbrirConexion());
                repositorio = new RepositorioLocalidades(conexion.AbrirConexion(), repositorioPlantas, repositorioUbicaciones);
                repositorio.Borrar(id);
                conexion.CerrarConexion();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Localidad localidad)
        {
            try
            {
                conexion = new ConexionBD();
                repositorioPlantas = new RepositorioPlantas(conexion.AbrirConexion());
                repositorioUbicaciones = new RepositorioUbicaciones(conexion.AbrirConexion());
                repositorio = new RepositorioLocalidades(conexion.AbrirConexion(), repositorioPlantas, repositorioUbicaciones);
                var relacionado = repositorio.EstaRelacionado(localidad);
                conexion.CerrarConexion();
                return relacionado;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Existe(Localidad localidad)
        {
            try
            {
                conexion = new ConexionBD();
                repositorioPlantas = new RepositorioPlantas(conexion.AbrirConexion());
                repositorioUbicaciones = new RepositorioUbicaciones(conexion.AbrirConexion());
                repositorio = new RepositorioLocalidades(conexion.AbrirConexion(), repositorioPlantas, repositorioUbicaciones);
                var existe = repositorio.Existe(localidad);
                conexion.CerrarConexion();
                return existe;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<Localidad> GetLista()
        {
            try
            {
                conexion = new ConexionBD();
                repositorioPlantas = new RepositorioPlantas(conexion.AbrirConexion());
                repositorioUbicaciones = new RepositorioUbicaciones(conexion.AbrirConexion());
                repositorio = new RepositorioLocalidades(conexion.AbrirConexion(), repositorioPlantas, repositorioUbicaciones);
                var lista = repositorio.GetLista();
                conexion.CerrarConexion();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        //public List<Localidad> GetLista(int plantaId)
        //{
        //    try
        //    {
        //        conexion = new ConexionBD();
        //        repositorioPlantas = new RepositorioPlantas(conexion.AbrirConexion());
        //        repositorioTipoLocalidades = new RepositorioTipoLocalidad(conexion.AbrirConexion());
        //        repositorio = new RepositorioLocalidades(conexion.AbrirConexion(),repositorioTipoLocalidades,repositorioPlantas);
        //        var lista = repositorio.GetLista(plantaId);
        //        conexion.CerrarConexion();
        //        return lista;
        //    }
        //    catch (Exception e)
        //    {

        //        throw new Exception(e.Message);
        //    }
        //}

        //public List<Localidad> GetLista(TipoLocalidad tipoLocalidad)
        //{
        //    try
        //    {
        //        conexion = new ConexionBd();
        //        repositorioPlantas = new RepositorioPlantas(conexion.AbrirConexion());
        //        repositorioTipoLocalidades = new RepositorioTipoLocalidades(conexion.AbrirConexion());
        //        repositorioMedidas = new RepositorioMedidas(conexion.AbrirConexion());
        //        repositorio = new RepositorioLocalidades(conexion.AbrirConexion(), repositorioPlantas, repositorioTipoLocalidades, repositorioMedidas);
        //        var lista = repositorio.GetLista(tipoLocalidad);
        //        conexion.CerrarConexion();
        //        return lista;
        //    }
        //    catch (Exception e)
        //    {

        //        throw new Exception(e.Message);
        //    }
        //}

        //public List<Localidad> GetLista(string descripcion)
        //{
        //    try
        //    {
        //        conexion = new ConexionBd();
        //        repositorioPlantas = new RepositorioPlantas(conexion.AbrirConexion());
        //        repositorioTipoLocalidades = new RepositorioTipoLocalidades(conexion.AbrirConexion());
        //        repositorioMedidas = new RepositorioMedidas(conexion.AbrirConexion());
        //        repositorio = new RepositorioLocalidades(conexion.AbrirConexion(), repositorioPlantas, repositorioTipoLocalidades, repositorioMedidas);
        //        var lista = repositorio.GetLista(descripcion);
        //        conexion.CerrarConexion();
        //        return lista;
        //    }
        //    catch (Exception e)
        //    {

        //        throw new Exception(e.Message);
        //    }
        //}

        //public Localidad GetLocalidadPorCodigoDeBarras(string codigo)
        //{
        //    try
        //    {
        //        conexion = new ConexionBd();
        //        repositorioPlantas = new RepositorioPlantas(conexion.AbrirConexion());
        //        repositorioTipoLocalidades = new RepositorioTipoLocalidades(conexion.AbrirConexion());
        //        repositorioMedidas = new RepositorioMedidas(conexion.AbrirConexion());
        //        repositorio = new RepositorioLocalidades(conexion.AbrirConexion(), repositorioPlantas, repositorioTipoLocalidades, repositorioMedidas);
        //        var localidad = repositorio.GetLocalidadPorCodigoDeBarras(codigo);
        //        conexion.CerrarConexion();
        //        return localidad;
        //    }
        //    catch (Exception e)
        //    {

        //        throw new Exception(e.Message);
        //    }
        //}

        public Localidad GetLocalidadPorId(int id)
        {
            try
            {
                conexion = new ConexionBD();
                repositorioPlantas = new RepositorioPlantas(conexion.AbrirConexion());
                repositorioUbicaciones = new RepositorioUbicaciones(conexion.AbrirConexion());
                repositorio = new RepositorioLocalidades(conexion.AbrirConexion(), repositorioPlantas, repositorioUbicaciones);
                var localidad = repositorio.GetLocalidadPorId(id);
                conexion.CerrarConexion();
                return localidad;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Guardar(Localidad localidad)
        {
            try
            {
                conexion = new ConexionBD();
                repositorio = new RepositorioLocalidades(conexion.AbrirConexion());
                repositorio.Guardar(localidad);
                conexion.CerrarConexion();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<Localidad> BuscarLocalidad(string text)
        {
            try
            {
                List<Localidad> lista = new List<Localidad>();
                conexion = new ConexionBD();
                repositorioPlantas = new RepositorioPlantas(conexion.AbrirConexion());
                repositorioUbicaciones = new RepositorioUbicaciones(conexion.AbrirConexion());
                repositorio = new RepositorioLocalidades(conexion.AbrirConexion(), repositorioPlantas, repositorioUbicaciones);
                lista = repositorio.BuscarLocalidad(text);
                conexion.CerrarConexion();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<Localidad> GetLista(Ubicacion ubicacion)
        {
            try
            {
                conexion = new ConexionBD();
                repositorioPlantas = new RepositorioPlantas(conexion.AbrirConexion());
                repositorioUbicaciones = new RepositorioUbicaciones(conexion.AbrirConexion());
                repositorio = new RepositorioLocalidades(conexion.AbrirConexion(), repositorioPlantas, repositorioUbicaciones);
                var lista = repositorio.GetLista(ubicacion);
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
