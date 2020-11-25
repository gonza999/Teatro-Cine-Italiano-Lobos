using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;
using Teatro.DataLayer.Facades;

namespace Teatro.DataLayer.Repositorios
{
    public class RepositorioVentasTickets : IRepositorioVentasTickets
    {
        private readonly SqlConnection cn;
        private SqlTransaction transaction;

        public RepositorioVentasTickets(SqlConnection cn1, SqlTransaction transaction1)
        {
            this.cn = cn1;
            this.transaction = transaction1;
        }

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
            throw new NotImplementedException();
        }

        public VentaTicket GetVentaTicket(Venta venta, Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public VentaTicket GetVentaTicketPorId(int ventaId, int ticketId)
        {
            throw new NotImplementedException();
        }

        public void Guardar(Venta venta,Ticket ticket)
        {
            try
            {
                var cadenaDeComando = "INSERT INTO VentasTicket (VentaId,TicketId) VALUES " +
                    "(@venta,@ticket)";
                var comando = new SqlCommand(cadenaDeComando, cn, transaction);
                comando.Parameters.AddWithValue("@venta",venta.VentaId);
                comando.Parameters.AddWithValue("@ticket",ticket.TicketId);
                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
