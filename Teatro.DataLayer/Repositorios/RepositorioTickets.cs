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
    public class RepositorioTickets:IRepositorioTickets
    {
        private readonly SqlConnection conexion;
        private IRepositorioLocalidades repositorioLocalidades;
        private SqlTransaction transaction;
        private IRepositorioHorarios repositorioHorarios;
        private IRepositorioFormasPagos repositorioFormasPagos;
        private IRepositorioFormasVentas repositorioFormasVentas;
        private IRepositorioEventos repositorioEventos;
        private IRepositorioTipoEventos repositorioTipoEventos;
        private IRepositorioClasificaciones repositorioClasificaciones;
        private IRepositorioDistribuciones repositorioDistribuciones;
        private IRepositorioPlantas repositorioPlantas;
        private IRepositorioUbicaciones repositorioUbicaciones;

        public RepositorioTickets(SqlConnection sqlConnection)
        {
            this.conexion = sqlConnection;
        }

        public RepositorioTickets(SqlConnection cn, IRepositorioLocalidades repositorioLocalidades,
            IRepositorioHorarios repositorioHorarios,IRepositorioFormasPagos repositorioFormasPagos,
            IRepositorioFormasVentas repositorioFormasVentas,SqlTransaction transaction)
        {
            conexion = cn;
            this.transaction = transaction;
            this.repositorioLocalidades = repositorioLocalidades;
            this.repositorioHorarios = repositorioHorarios;
            this.repositorioFormasVentas = repositorioFormasVentas;
            this.repositorioFormasPagos = repositorioFormasPagos;
        }

        public RepositorioTickets(SqlConnection cn, SqlTransaction transaction)
        {
            this.conexion = cn;
            this.transaction = transaction;
        }

        public void Borrar(int id)
        {
            try
            {
                var cadenaDeComando = "DELETE Tickets WHERE TicketId=@id";
                var comando = new SqlCommand(cadenaDeComando, conexion);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Ticket ticket)
        {
            try
            {
                var cadenaDeComando = "SELECT TicketId FROM VentasTickets WHERE TicketId=@id";
                var comando = new SqlCommand(cadenaDeComando, conexion);
                comando.Parameters.AddWithValue("@id", ticket.TicketId);
                var reader = comando.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Existe(Ticket ticket)
        {
            //try
            //{
            //    string fecha = $"{ticket.FechaVenta.Year}{ticket.FechaVenta.Month}{ticket.FechaVenta.Day}";
            //    SqlCommand comando;
            //    if (ticket.TicketId == 0)
            //    {
            //        string cadenaComando = "SELECT TicketId FROM Tickets WHERE  convert(date,Fecha)=@fecha ";
            //        comando = new SqlCommand(cadenaComando, conexion, transaction);
            //        comando.Parameters.AddWithValue("@id", ticket.Localidad.LocalidadId);
            //        comando.Parameters.AddWithValue("@fecha", fecha);

            //    }
            //    else
            //    {
            //        string cadenaComando = "SELECT TicketId FROM Tickets WHERE LocalidadId<>@localidad AND convert(date,Fecha)=@fecha AND TicketId<>@id";
            //        comando = new SqlCommand(cadenaComando, conexion, transaction);
            //        comando.Parameters.AddWithValue("@localidad", ticket.Localidad.LocalidadId);
            //        comando.Parameters.AddWithValue("@fecha", fecha);
            //        comando.Parameters.AddWithValue("@id", ticket.TicketId);


            //    }
            //    SqlDataReader reader = comando.ExecuteReader();
            //    return reader.HasRows;
            //}
            //catch (Exception e)
            //{
            //    throw new Exception(e.Message);
            //}
            return false;
        }

        public List<Ticket> GetLista()
        {
            List<Ticket> lista = new List<Ticket>();
            try
            {
                var cadenaDeComando = "SELECT TicketId,HorarioId,Importe,LocalidadId, " +
                    "FechaVenta,FormaPagoId,FormaVentaId,Anulada FROM Tickets";
                var comando = new SqlCommand(cadenaDeComando, conexion, transaction);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Ticket ticket = ConstruirTicket(reader);
                    lista.Add(ticket);
                }
                reader.Close();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        private Ticket ConstruirTicket(SqlDataReader reader)
        {
            Ticket ticket = new Ticket();
            ticket.TicketId = reader.GetInt32(0);
            repositorioClasificaciones = new RepositorioClasificaciones(conexion,transaction);
            repositorioTipoEventos = new RepositorioTipoEvento(conexion,transaction);
            repositorioDistribuciones = new RepositorioDistribuciones(conexion,transaction);
            repositorioEventos = new RepositorioEventos(conexion,repositorioTipoEventos,repositorioClasificaciones,repositorioDistribuciones,transaction);
            repositorioHorarios = new RepositorioHorarios(conexion,repositorioEventos,transaction);
            ticket.Horario =repositorioHorarios.GetHorarioPorId(reader.GetInt32(1));
            ticket.Importe = reader.GetDecimal(2);
            repositorioPlantas = new RepositorioPlantas(conexion,transaction);
            repositorioUbicaciones = new RepositorioUbicaciones(conexion,transaction);
            repositorioLocalidades = new RepositorioLocalidades(conexion,repositorioPlantas,repositorioUbicaciones, transaction);
            ticket.Localidad = repositorioLocalidades.GetLocalidadPorId(reader.GetInt32(3));
            ticket.FechaVenta = reader.GetDateTime(4);
            repositorioFormasPagos = new RepositorioFormasPagos(conexion, transaction);
            ticket.FormaPago = repositorioFormasPagos.GetFormaPagoPorId(reader.GetInt32(5));
            repositorioFormasVentas = new RepositorioFormasVentas(conexion, transaction);
            ticket.FormaVenta = repositorioFormasVentas.GetFormaVentaPorId(reader.GetInt32(6));
            ticket.Anulada = reader.GetBoolean(7);
            return ticket;
        }

        public Ticket GetTicketPorId(int id)
        {
            Ticket ticket = null;
            try
            {
                var cadenaDeComando = "SELECT TicketId,HorarioId,Importe,LocalidadId, " +
                    "FechaVenta,FormaPagoId,FormaVentaId,Anulada FROM Tickets WHERE TicketId=@id";
                var comando = new SqlCommand(cadenaDeComando, conexion,transaction);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    ticket = ConstruirTicket(reader);
                    reader.Close();
                }
                return ticket;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Guardar(Ticket ticket)
        {

            //if (ticket.TicketId == 0)
            //{
                try
                {
                    var cadenaDeComando = "INSERT INTO Tickets (HorarioId,Importe,LocalidadId,FechaVenta," +
                        "FormaPagoId,FormaVentaId,Anulada) " +
                        "VALUES (@horario,@importe,@localidad,@fecha,@pago,@venta,@anulada)";
                    var comando = new SqlCommand(cadenaDeComando, conexion, transaction);
                    comando.Parameters.AddWithValue("@horario", ticket.Horario.HorarioId);
                    comando.Parameters.AddWithValue("@importe", ticket.Importe);
                    comando.Parameters.AddWithValue("@localidad", ticket.Localidad.LocalidadId);
                    comando.Parameters.AddWithValue("@fecha", ticket.FechaVenta);
                    comando.Parameters.AddWithValue("@pago", ticket.FormaPago.FormaPagoId);
                    comando.Parameters.AddWithValue("@venta", ticket.FormaVenta.FormaVentaId);
                    comando.Parameters.AddWithValue("@anulada", ticket.Anulada);
                    comando.ExecuteNonQuery();
                    cadenaDeComando = "SELECT @@Identity";
                    comando = new SqlCommand(cadenaDeComando, conexion, transaction);
                    ticket.TicketId = (int)(decimal)comando.ExecuteScalar();
                }
                catch (Exception e)
                {

                    throw new Exception(e.Message);
                }
            //}
            //else
            //{
            //    //Edición
            //    try
            //    {
            //        string cadenaComando = "UPDATE Tickets SET HorarioId=@horario,Importe=@importe," +
            //            "LocalidadId=@localidad,FechaVenta=@fecha,FormaPagoId=@pago,FormaVenta=@venta," +
            //            "Anulada=@anulada WHERE TicketId=@id";
            //        SqlCommand comando = new SqlCommand(cadenaComando, conexion, transaction);
            //        comando.Parameters.AddWithValue("@horario", ticket.Horario.HorarioId);
            //        comando.Parameters.AddWithValue("@importe", ticket.Importe);
            //        comando.Parameters.AddWithValue("@localidad", ticket.Localidad.LocalidadId);
            //        comando.Parameters.AddWithValue("@fecha", ticket.FechaVenta);
            //        comando.Parameters.AddWithValue("@pago", ticket.FormaPago.FormaPagoId);
            //        comando.Parameters.AddWithValue("@venta", ticket.FormaVenta.FormaVentaId);
            //        comando.Parameters.AddWithValue("@anulada", ticket.Anulada);
            //        comando.Parameters.AddWithValue("@id", ticket.TicketId);
            //        comando.ExecuteNonQuery();

            //    }
            //    catch (Exception e)
            //    {
            //        throw new Exception(e.Message);
            //    }

            //}
        }

        public bool Existe(Localidad localidad,Horario horario)
        {
            try
            {
                SqlCommand comando;
                string cadenaComando = "SELECT TicketId FROM Tickets WHERE LocalidadId=@id AND HorarioId=@horario AND Anulada=0";
                comando = new SqlCommand(cadenaComando, conexion, transaction);
                comando.Parameters.AddWithValue("@id", localidad.LocalidadId);
                comando.Parameters.AddWithValue("@horario",horario.HorarioId);
                SqlDataReader reader = comando.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void AnularTicket(int ticketId)
        {
            try
            {
                var cadenaDeComando = "UPDATE  Tickets SET Anulada=1 WHERE TicketId=@id";
                var comando = new SqlCommand(cadenaDeComando, conexion);
                comando.Parameters.AddWithValue("@id", ticketId);
                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<Ticket> GetLista(List<Horario> horarios)
        {
            List<Ticket> lista = new List<Ticket>();
            try
            {
                foreach (var h in horarios)
                {
                    var cadenaDeComando = "SELECT TicketId,HorarioId,Importe,LocalidadId, " +
                               "FechaVenta,FormaPagoId,FormaVentaId,Anulada FROM Tickets WHERE HorarioId IN (" +
                               "SELECT HorarioId FROM Horarios WHERE HorarioId=@id)";
                    var comando = new SqlCommand(cadenaDeComando, conexion, transaction);
                    comando.Parameters.AddWithValue("@id",h.HorarioId);
                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        Ticket ticket = ConstruirTicket(reader);
                        lista.Add(ticket);
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
