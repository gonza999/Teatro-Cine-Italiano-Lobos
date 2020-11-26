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
    public class ServicioVentasTickets : IServicioVentasTickets
    {
        private ConexionBD _conexion;
        private IRepositorioVentasTickets _repositorio;
        private SqlTransaction transaction;
        public void Borrar(int id)
        {
            throw new NotImplementedException();
        }

        public bool EstaRelacionado(Venta venta, Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public bool Existe(Venta venta, Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public List<VentaTicket> GetLista()
        {
            try
            {
                _conexion = new ConexionBD();
                SqlConnection cn = _conexion.AbrirConexion();
                transaction = cn.BeginTransaction();
                _repositorio = new RepositorioVentasTickets(cn,transaction);
                var lista = _repositorio.GetLista();
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

        public VentaTicket GetVentaTicket(Venta venta, Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public VentaTicket GetVentaTicketPorId(int ventaId, int ticketId)
        {
            throw new NotImplementedException();
        }

        public void Guardar(Venta venta, Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
