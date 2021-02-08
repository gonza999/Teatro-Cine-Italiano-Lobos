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
        private IRepositorioVentas repositorioVentas;
        //private IRepositorioTickets repositorioTickets;

        public RepositorioVentasTickets(SqlConnection cn1, SqlTransaction transaction1)
        {
            this.cn = cn1;
            this.transaction = transaction1;
        }

        public void Borrar(int id)
        {
            try
            {
                var cadenaDeComando = "DELETE VentasTicket WHERE TicketId=@id";
                var comando = new SqlCommand(cadenaDeComando,cn,transaction);
                comando.Parameters.AddWithValue("@id",id);
                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
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
            List<VentaTicket> lista = new List<VentaTicket>();
            try
            {
                var cadenaDeComando = "SELECT VentaId " +
                    " FROM VentasTicket GROUP BY VentaId";
                var comando = new SqlCommand(cadenaDeComando, cn, transaction);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    VentaTicket ventaTicket = ConstruirVentaTicket(reader);
                    lista.Add(ventaTicket);
                }
                reader.Close();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        private VentaTicket ConstruirVentaTicket(SqlDataReader reader)
        {
            VentaTicket ventaTicket = new VentaTicket();
            repositorioVentas = new RepositorioVentas(cn,transaction);
            ventaTicket.Venta = repositorioVentas.GetVentaPorId(reader.GetInt32(0));
            return ventaTicket;
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

        public List<int> GetLista(int ventaId)
        {
            List<int> lista = new List<int>();
            try
            {
                var cadenaDeComando = "SELECT TicketId " +
                    " FROM VentasTicket WHERE VentaId=@id";
                var comando = new SqlCommand(cadenaDeComando, cn, transaction);
                comando.Parameters.AddWithValue("@id",ventaId);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(reader.GetInt32(0));
                }
                reader.Close();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<int> GetListaVentas(List<Ticket> listaTickets)
        {
            List<int> lista = new List<int>();
            try
            {
                foreach (var t in listaTickets)
                {
                    var cadenaDeComando = "SELECT VentaId " +
                               " FROM VentasTicket WHERE TicketId=@id";
                    var comando = new SqlCommand(cadenaDeComando, cn, transaction);
                    comando.Parameters.AddWithValue("@id", t.TicketId);
                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(reader.GetInt32(0));
                    }
                    reader.Close(); 
                }
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
