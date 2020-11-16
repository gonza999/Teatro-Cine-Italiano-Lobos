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
    public class ServicioTiposDocumentos:IServicioTiposDocumentos
    {
        private ConexionBD _conexion;
        private IRepositorioTiposDocumentos _repositorio;
        public void Borrar(int id)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioTiposDocumentos(_conexion.AbrirConexion());
                _repositorio.Borrar(id);
                _conexion.CerrarConexion();

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<TipoDocumento> BuscarTipoDocumento(string tipoDocumento)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioTiposDocumentos(_conexion.AbrirConexion());
                var lista = _repositorio.BuscarTipoDocumento(tipoDocumento);
                _conexion.CerrarConexion();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(TipoDocumento tipoDocumento)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioTiposDocumentos(_conexion.AbrirConexion());
                var estaRelacionado = _repositorio.EstaRelacionado(tipoDocumento);
                _conexion.CerrarConexion();
                return estaRelacionado;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Existe(TipoDocumento tipoDocumento)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioTiposDocumentos(_conexion.AbrirConexion());
                var existe = _repositorio.Existe(tipoDocumento);
                _conexion.CerrarConexion();
                return existe;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<TipoDocumento> GetLista()
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioTiposDocumentos(_conexion.AbrirConexion());
                var lista = _repositorio.GetLista();
                _conexion.CerrarConexion();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public TipoDocumento GetTipoDocumento(string nombreTipoDocumento)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioTiposDocumentos(_conexion.AbrirConexion());
                var tipoDocumento = _repositorio.GetTipoDocumento(nombreTipoDocumento);
                _conexion.CerrarConexion();
                return tipoDocumento;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public TipoDocumento GetTipoDocumentoPorId(int id)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioTiposDocumentos(_conexion.AbrirConexion());
                var tipoDocumento = _repositorio.GetTipoDocumentoPorId(id);
                _conexion.CerrarConexion();
                return tipoDocumento;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Guardar(TipoDocumento tipoDocumento)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioTiposDocumentos(_conexion.AbrirConexion());
                _repositorio.Guardar(tipoDocumento);
                _conexion.CerrarConexion();

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
