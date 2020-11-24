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
    public class RepositorioEventos : IRepositorioEventos
    {
        private readonly SqlConnection conexion;
        private  IRepositorioTipoEventos repositorioTipoEventos;
        private  IRepositorioClasificaciones repositorioClasificaciones;
        private IRepositorioDistribuciones repositorioDistribuciones;
        private readonly SqlTransaction transaction;

        public RepositorioEventos(SqlConnection conexion, IRepositorioTipoEventos repositorioTipoEventos,
             IRepositorioClasificaciones repositorioClasificaciones,IRepositorioDistribuciones repositorioDistribuciones)
        {
            this.conexion = conexion;
            this.repositorioClasificaciones = repositorioClasificaciones;
            this.repositorioTipoEventos = repositorioTipoEventos;
            this.repositorioDistribuciones = repositorioDistribuciones;
        }
        public RepositorioEventos(SqlConnection conexion, IRepositorioTipoEventos repositorioTipoEventos,
             IRepositorioClasificaciones repositorioClasificaciones, IRepositorioDistribuciones repositorioDistribuciones,
             SqlTransaction transaction)
        {
            this.conexion = conexion;
            this.repositorioClasificaciones = repositorioClasificaciones;
            this.repositorioTipoEventos = repositorioTipoEventos;
            this.repositorioDistribuciones = repositorioDistribuciones;
            this.transaction = transaction;
        }

        public RepositorioEventos(SqlConnection sqlConnection)
        {
            this.conexion = sqlConnection;
        }

        public RepositorioEventos(SqlConnection cn, SqlTransaction transaction)
        {
            conexion = cn;
            this.transaction = transaction;
        }

        public void Borrar(int id)
        {
            try
            {
                var cadenaDeComando = "DELETE Eventos WHERE EventoId=@id";
                var comando = new SqlCommand(cadenaDeComando, conexion);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Evento evento)
        {
            try
            {
                var cadenaDeComando = "SELECT EventoId FROM Horarios WHERE EventoId=@id";
                var comando = new SqlCommand(cadenaDeComando, conexion);
                comando.Parameters.AddWithValue("@id", evento.EventoId);
                var reader = comando.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Existe(Evento evento)
        {
            try
            {
                SqlCommand comando;
                if (evento.EventoId == 0)
                {
                    string cadenaComando = "SELECT EventoId FROM Eventos WHERE FechaEvento=@fecha";
                    comando = new SqlCommand(cadenaComando, conexion);
                    comando.Parameters.AddWithValue("@fecha", evento.FechaEvento);

                }
                else
                {
                    string cadenaComando = "SELECT EventoId FROM Eventos WHERE FechaEvento=@fecha AND EventoId<>@id";
                    comando = new SqlCommand(cadenaComando, conexion);
                    comando.Parameters.AddWithValue("@fecha", evento.FechaEvento);
                    comando.Parameters.AddWithValue("@id", evento.EventoId);


                }
                SqlDataReader reader = comando.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Evento> GetLista()
        {
            List<Evento> lista = new List<Evento>();
            try
            {
                var cadenaDeComando = "SELECT EventoId,Evento,Descripcion,TipoEventoId,ClasificacionId,FechaEvento,Suspendido,DistribucionId " +
                    "FROM Eventos";
                var comando = new SqlCommand(cadenaDeComando, conexion);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Evento evento = ConstruirEvento(reader);
                    lista.Add(evento);
                }
                reader.Close();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        private Evento ConstruirEvento(SqlDataReader reader)
        {
            Evento evento = new Evento();
            evento.EventoId = reader.GetInt32(0);
            evento.NombreEvento = reader.GetString(1);
            evento.Descripcion = reader.GetString(2);
            repositorioClasificaciones = new RepositorioClasificaciones(conexion,transaction);
            repositorioTipoEventos = new RepositorioTipoEvento(conexion,transaction);
            evento.TipoEvento = repositorioTipoEventos.GetTipoEventoPorId(reader.GetInt32(3));
            evento.Clasificacion = repositorioClasificaciones.GetClasificacionPorId(reader.GetInt32(4));
            evento.FechaEvento = reader.GetDateTime(5);
            evento.Suspendido = reader.GetBoolean(6);
            repositorioDistribuciones = new RepositorioDistribuciones(conexion,transaction);
            evento.Distribucion= repositorioDistribuciones.GetDistribucionPorId(reader.GetInt32(7));
            return evento;
        }

        public Evento GetEventoPorId(int id)
        {
            Evento evento = null;
            try
            {
                var cadenaDeComando = "SELECT EventoId,Evento,Descripcion,TipoEventoId," +
                    "ClasificacionId,FechaEvento,Suspendido,DistribucionId FROM Eventos WHERE EventoId=@id";
                var comando = new SqlCommand(cadenaDeComando, conexion,transaction);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    evento = ConstruirEvento(reader);
                    reader.Close();
                }
                return evento;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Guardar(Evento evento)
        {

            if (evento.EventoId == 0)
            {
                try
                {
                    var cadenaDeComando = "INSERT INTO Eventos (Evento,Descripcion,TipoEventoId," +
                               "ClasificacionId,FechaEvento,Suspendido,DistribucionId) VALUES (@evento,@descripcion,@tipoEventoId," +
                               "@clasificacionId,@fecha,@suspendido,@distribucion)";
                    var comando = new SqlCommand(cadenaDeComando, conexion,transaction);
                    comando.Parameters.AddWithValue("@evento", evento.NombreEvento);
                    comando.Parameters.AddWithValue("@descripcion", evento.Descripcion);
                    comando.Parameters.AddWithValue("@tipoEventoId", evento.TipoEvento.TipoEventoId);
                    comando.Parameters.AddWithValue("@clasificacionId", evento.Clasificacion.ClasificacionId);
                    comando.Parameters.AddWithValue("@fecha", evento.FechaEvento);
                    comando.Parameters.AddWithValue("@suspendido", evento.Suspendido);
                    comando.Parameters.AddWithValue("@distribucion", evento.Distribucion.DistribucionId);
                    comando.ExecuteNonQuery();
                    cadenaDeComando = "SELECT @@Identity";
                    comando = new SqlCommand(cadenaDeComando, conexion,transaction);
                    evento.EventoId = (int)(decimal)comando.ExecuteScalar();
                }
                catch (Exception e)
                {

                    throw new Exception(e.Message);
                }
            }
            else
            {
                //Edición
                try
                {
                    string cadenaComando = "UPDATE Eventos SET Evento=@nombre,Descripcion=@desc," +
                        "TipoEventoId=@tipo,ClasificacionId=@clasi,FechaEvento=@fecha,Suspendido=@sus,DistribucionId=@distribucion " +
                        " WHERE EventoId=@id";
                    SqlCommand comando = new SqlCommand(cadenaComando, conexion,transaction);
                    comando.Parameters.AddWithValue("@nombre", evento.NombreEvento);
                    comando.Parameters.AddWithValue("@desc", evento.Descripcion);
                    comando.Parameters.AddWithValue("@tipo", evento.TipoEvento.TipoEventoId);
                    comando.Parameters.AddWithValue("@clasi", evento.Clasificacion.ClasificacionId);
                    comando.Parameters.AddWithValue("@fecha", evento.FechaEvento);
                    comando.Parameters.AddWithValue("@sus", evento.Suspendido);
                    comando.Parameters.AddWithValue("@id", evento.EventoId);
                    comando.Parameters.AddWithValue("@distribucion", evento.Distribucion.DistribucionId);
                    comando.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }
        }
        public List<Evento> BuscarEvento(string text)
        {
            List<Evento> lista = new List<Evento>();
            try
            {
                var cadenaDeComando = "SELECT EventoId,Evento,Descripcion,TipoEventoId,ClasificacionId,FechaEvento,Suspendido,DistribucionId " +
                    "FROM Eventos WHERE Evento like @text";
                var comando = new SqlCommand(cadenaDeComando, conexion);
                comando.Parameters.AddWithValue("@text", $"%{text}%");
                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Evento evento = ConstruirEvento(reader);
                    lista.Add(evento);
                }
                reader.Close();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

     
    }
}


