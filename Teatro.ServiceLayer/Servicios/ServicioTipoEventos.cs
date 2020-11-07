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
    public class ServicioTipoEventos : IServicioTipoEventos
    {
        private ConexionBD _conexion;
        private IRepositorioTipoEventos _repositorio;
        public void Borrar(int id)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioTipoEvento(_conexion.AbrirConexion());
                _repositorio.Borrar(id);
                _conexion.CerrarConexion();

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<TipoEvento> BuscarTipoEvento(string tipoevento)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioTipoEvento(_conexion.AbrirConexion());
                var lista = _repositorio.BuscarTipoEvento(tipoevento);
                _conexion.CerrarConexion();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(TipoEvento tipoevento)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioTipoEvento(_conexion.AbrirConexion());
                var estaRelacionado = _repositorio.EstaRelacionado(tipoevento);
                _conexion.CerrarConexion();
                return estaRelacionado;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Existe(TipoEvento tipoevento)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioTipoEvento(_conexion.AbrirConexion());
                var existe = _repositorio.Existe(tipoevento);
                _conexion.CerrarConexion();
                return existe;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<TipoEvento> GetLista()
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioTipoEvento(_conexion.AbrirConexion());
                var lista = _repositorio.GetLista();
                _conexion.CerrarConexion();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public TipoEvento GetTipoEvento(string nombreTipoEvento)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioTipoEvento(_conexion.AbrirConexion());
                var tipoevento = _repositorio.GetTipoEvento(nombreTipoEvento);
                _conexion.CerrarConexion();
                return tipoevento;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public TipoEvento GetTipoEventoPorId(int id)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioTipoEvento(_conexion.AbrirConexion());
                var tipoevento = _repositorio.GetTipoEventoPorId(id);
                _conexion.CerrarConexion();
                return tipoevento;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Guardar(TipoEvento tipoevento)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioTipoEvento(_conexion.AbrirConexion());
                _repositorio.Guardar(tipoevento);
                _conexion.CerrarConexion();

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
