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
    public class ServicioVentas : IServicioVentas
    {

        private ConexionBD conexion;
        private IRepositorioVentas repositorio;
        private IRepositorioTickets repositorioTickets;
        private IRepositorioVentasTickets repositorioVentasTickets;
        private SqlTransaction transaction;

        public void AnularVenta(int ventaId)
        {
            try
            {
                conexion = new ConexionBD();
                repositorio = new RepositorioVentas(conexion.AbrirConexion());
                repositorio.AnularVenta(ventaId);
                conexion.CerrarConexion();

            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }

        public void Borrar(int id)
        {
            throw new NotImplementedException();
        }

        public List<Venta> BuscarVenta(string text)
        {
            throw new NotImplementedException();
        }

        public bool EstaRelacionado(Venta venta)
        {
            throw new NotImplementedException();
        }

        public bool Existe(Venta venta)
        {
            throw new NotImplementedException();
        }

        public List<Venta> GetLista()
        {
            throw new NotImplementedException();
        }

        public Venta GetVenta(string nombreVenta)
        {
            throw new NotImplementedException();
        }

        public Venta GetVentaPorId(int id)
        {
            throw new NotImplementedException();
        }

        public void Guardar(Venta venta)
        {
            try
            {
                conexion = new ConexionBD();
                SqlConnection cn = conexion.AbrirConexion();
                transaction = cn.BeginTransaction();
                repositorioTickets = new RepositorioTickets(cn, transaction);
                repositorio = new RepositorioVentas(cn,transaction);
                repositorioVentasTickets = new RepositorioVentasTickets(cn,transaction);
                repositorio.Guardar(venta);
                foreach (var t in venta.Tickets)
                {
                    repositorioTickets.Guardar(t);
                    repositorioVentasTickets.Guardar(venta,t);
                }
                transaction.Commit();
                conexion.CerrarConexion();

            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new Exception(e.Message);
            }
        }
    }
}
