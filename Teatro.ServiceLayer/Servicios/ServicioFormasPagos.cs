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
    public class ServicioFormasPagos:IServicioFormasPagos
    {

        private ConexionBD _conexion;
        private IRepositorioFormasPagos _repositorio;
        public void Borrar(int id)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioFormasPagos(_conexion.AbrirConexion());
                _repositorio.Borrar(id);
                _conexion.CerrarConexion();

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<FormaPago> BuscarFormaPago(string formaPago)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioFormasPagos(_conexion.AbrirConexion());
                var lista = _repositorio.BuscarFormaPago(formaPago);
                _conexion.CerrarConexion();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(FormaPago formaPago)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioFormasPagos(_conexion.AbrirConexion());
                var estaRelacionado = _repositorio.EstaRelacionado(formaPago);
                _conexion.CerrarConexion();
                return estaRelacionado;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Existe(FormaPago formaPago)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioFormasPagos(_conexion.AbrirConexion());
                var existe = _repositorio.Existe(formaPago);
                _conexion.CerrarConexion();
                return existe;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<FormaPago> GetLista()
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioFormasPagos(_conexion.AbrirConexion());
                var lista = _repositorio.GetLista();
                _conexion.CerrarConexion();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public FormaPago GetFormaPago(string nombreFormaPago)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioFormasPagos(_conexion.AbrirConexion());
                var formaPago = _repositorio.GetFormaPago(nombreFormaPago);
                _conexion.CerrarConexion();
                return formaPago;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public FormaPago GetFormaPagoPorId(int id)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioFormasPagos(_conexion.AbrirConexion());
                var formaPago = _repositorio.GetFormaPagoPorId(id);
                _conexion.CerrarConexion();
                return formaPago;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Guardar(FormaPago formaPago)
        {
            try
            {
                _conexion = new ConexionBD();
                _repositorio = new RepositorioFormasPagos(_conexion.AbrirConexion());
                _repositorio.Guardar(formaPago);
                _conexion.CerrarConexion();

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
