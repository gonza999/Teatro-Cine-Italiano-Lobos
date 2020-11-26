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
    public class RepositorioVentas : IRepositorioVentas
    {
        private readonly SqlConnection cn;
        private SqlTransaction transaction;
        private IRepositorioEmpleados repositorioEmpleados;
        private IRepositorioTickets repositorioTickets;
        private IRepositorioVentasTickets repositorioVentasTickets;

        public RepositorioVentas(SqlConnection sqlConnection)
        {
            cn = sqlConnection;
        }

        public RepositorioVentas(SqlConnection cn1, SqlTransaction transaction1)
        {
            this.cn = cn1;
            this.transaction = transaction1;
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
            Venta venta = null;
            try
            {
                var cadenaDeComando = "SELECT  VentaId,Fecha,Total,Estado,EmpleadoId" +
                    " FROM Ventas WHERE VentaId=@id";
                var comando = new SqlCommand(cadenaDeComando, cn,transaction);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    venta = ConstruirVenta(reader);
                    reader.Close();
                }
                return venta;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        
        private Venta ConstruirVenta(SqlDataReader reader)
        {
            Venta venta = new Venta();
            venta.VentaId = reader.GetInt32(0);
            venta.Fecha = reader.GetDateTime(1);
            venta.Total = reader.GetDecimal(2);
            venta.Estado = reader.GetBoolean(3);
            repositorioEmpleados = new RepositorioEmpleados(cn,transaction);
            venta.Empleado = repositorioEmpleados.GetEmpleadoPorId(reader.GetInt32(4));
            repositorioVentasTickets = new RepositorioVentasTickets(cn, transaction);
            List<int> listaId = repositorioVentasTickets.GetLista(venta.VentaId);
            List<Ticket> listaTickets = new List<Ticket>();
            repositorioTickets = new RepositorioTickets(cn,transaction);
            foreach (var id in listaId)
            {
                listaTickets.Add(repositorioTickets.GetTicketPorId(id));
            }
            venta.Tickets = listaTickets;
            return venta;
        }

        public void Guardar(Venta venta)
        {
            //if (clasificacion.ClasificacionId == 0)
            //{
            //    //Nuevo registro
            try
            {
                string cadenaComando = "INSERT INTO Ventas VALUES(@fecha,@total,@estado,@empleado)";
                SqlCommand comando = new SqlCommand(cadenaComando, cn,transaction);
                comando.Parameters.AddWithValue("@fecha",venta.Fecha);
                comando.Parameters.AddWithValue("@total", venta.Total);
                comando.Parameters.AddWithValue("@estado", venta.Estado);
                comando.Parameters.AddWithValue("@empleado", venta.Empleado.EmpleadoId);

                comando.ExecuteNonQuery();
                cadenaComando = "SELECT @@IDENTITY";
                comando = new SqlCommand(cadenaComando, cn,transaction);
                venta.VentaId = (int)(decimal)comando.ExecuteScalar();

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
            //        string cadenaComando = "UPDATE Clasificaciones SET Clasificacion=@nombre WHERE ClasificacionId=@id";
            //        SqlCommand comando = new SqlCommand(cadenaComando, cn);
            //        comando.Parameters.AddWithValue("@nombre", clasificacion.NombreClasificacion);
            //        comando.Parameters.AddWithValue("@id", clasificacion.ClasificacionId);
            //        comando.ExecuteNonQuery();

            //    }
            //    catch (Exception e)
            //    {
            //        throw new Exception(e.Message);
            //    }

            //}
        }

        public void AnularVenta(int ventaId)
        {
            try
            {
                var cadenaDeComando = "UPDATE  Ventas SET Estado=1 WHERE VentaId=@id";
                var comando = new SqlCommand(cadenaDeComando,cn);
                comando.Parameters.AddWithValue("@id",ventaId);
                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
