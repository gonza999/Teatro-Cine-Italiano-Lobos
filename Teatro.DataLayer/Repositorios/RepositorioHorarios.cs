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
    public class RepositorioHorarios:IRepositorioHorarios
    {
        private readonly SqlConnection conexion;
        private IRepositorioEventos repositorioEventos;
        private readonly SqlTransaction transaction;

        public RepositorioHorarios(SqlConnection conexion,
             IRepositorioEventos repositorioEventos)
        {
            this.conexion = conexion;
            this.repositorioEventos = repositorioEventos;
        }

        public RepositorioHorarios(SqlConnection sqlConnection)
        {
            this.conexion = sqlConnection;
        }

        public RepositorioHorarios(SqlConnection cn,IRepositorioEventos repositorioEventos, SqlTransaction transaction)
        {
            conexion = cn;
            this.transaction = transaction;
            this.repositorioEventos = repositorioEventos;
        }

        public RepositorioHorarios(SqlConnection cn, SqlTransaction transaction)
        {
            this.conexion = cn;
            this.transaction = transaction;
        }

        public void Borrar(int id)
        {
            try
            {
                var cadenaDeComando = "DELETE Horarios WHERE HorarioId=@id";
                var comando = new SqlCommand(cadenaDeComando, conexion,transaction);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Horario horario)
        {
            try
            {
                var cadenaDeComando = "SELECT HorarioId FROM Tickets WHERE HorarioId=@id";
                var comando = new SqlCommand(cadenaDeComando, conexion,transaction);
                comando.Parameters.AddWithValue("@id", horario.HorarioId);
                var reader = comando.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Existe(Horario horario)
        {
            try
            {
                string fecha = $"{horario.Fecha.Year}{horario.Fecha.Month}{horario.Fecha.Day}";
                SqlCommand comando;
                if (horario.HorarioId == 0)
                {
                    string cadenaComando = "SELECT HorarioId FROM Horarios WHERE  convert(date,Fecha)=@fecha ";
                    comando = new SqlCommand(cadenaComando, conexion,transaction);
                    comando.Parameters.AddWithValue("@id", horario.Evento.EventoId);
                    comando.Parameters.AddWithValue("@fecha",fecha);

                }
                else
                {
                    string cadenaComando = "SELECT HorarioId FROM Horarios WHERE EventoId<>@evento AND convert(date,Fecha)=@fecha AND HorarioId<>@id";
                    comando = new SqlCommand(cadenaComando, conexion,transaction);
                    comando.Parameters.AddWithValue("@evento", horario.Evento.EventoId);
                    comando.Parameters.AddWithValue("@fecha", fecha);
                    comando.Parameters.AddWithValue("@id", horario.HorarioId);


                }
                SqlDataReader reader = comando.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Horario> GetLista()
        {
            List<Horario> lista = new List<Horario>();
            try
            {
                var cadenaDeComando = "SELECT HorarioId,Fecha,Hora,EventoId " +
                    " FROM Horarios";
                var comando = new SqlCommand(cadenaDeComando, conexion,transaction);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Horario horario = ConstruirHorario(reader);
                    lista.Add(horario);
                }
                reader.Close();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        private Horario ConstruirHorario(SqlDataReader reader)
        {
            Horario horario = new Horario();
            horario.HorarioId = reader.GetInt32(0);
            horario.Fecha = reader.GetDateTime(1);
            horario.Hora = reader.GetDateTime(2);
            repositorioEventos = new RepositorioEventos(conexion,transaction);
            horario.Evento = repositorioEventos.GetEventoPorId(reader.GetInt32(3));
            return horario;
        }

        public Horario GetHorarioPorId(int id)
        {
            Horario horario = null;
            try
            {
                var cadenaDeComando = "SELECT  HorarioId,Fecha,Hora,EventoId " +
                    "  FROM Horarios WHERE HorarioId=@id";
                var comando = new SqlCommand(cadenaDeComando, conexion);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    horario = ConstruirHorario(reader);
                    reader.Close();
                }
                return horario;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Guardar(Horario horario)
        {

            if (horario.HorarioId == 0)
            {
                try
                {
                    var cadenaDeComando = "INSERT INTO Horarios (Fecha,Hora,EventoId) " +
                        "VALUES (@fecha,@hora,@evento)";
                    var comando = new SqlCommand(cadenaDeComando, conexion,transaction);
                    comando.Parameters.AddWithValue("@fecha", horario.Fecha);
                    comando.Parameters.AddWithValue("@hora", horario.Hora);
                    comando.Parameters.AddWithValue("@evento", horario.Evento.EventoId);
                    comando.ExecuteNonQuery();
                    cadenaDeComando = "SELECT @@Identity";
                    comando = new SqlCommand(cadenaDeComando, conexion,transaction);
                    horario.HorarioId = (int)(decimal)comando.ExecuteScalar();
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
                    string cadenaComando = "UPDATE Horarios SET Fecha=@fecha,Hora=@hora," +
                        "EventoId=@evento WHERE HorarioId=@id";
                    SqlCommand comando = new SqlCommand(cadenaComando, conexion,transaction);
                    comando.Parameters.AddWithValue("@fecha", horario.Fecha);
                    comando.Parameters.AddWithValue("@hora", horario.Hora);
                    comando.Parameters.AddWithValue("@evento", horario.Evento.EventoId);
                    comando.Parameters.AddWithValue("@id", horario.HorarioId);
                    comando.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }
        }

        public List<Horario> GetLista(Evento evento)
        {
            List<Horario> lista = new List<Horario>();
            try
            {
                var cadenaDeComando = "SELECT HorarioId,Fecha,Hora,EventoId " +
                    " FROM Horarios WHERE EventoId=@id";
                var comando = new SqlCommand(cadenaDeComando, conexion,transaction);
                comando.Parameters.AddWithValue("@id",evento.EventoId);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Horario horario = ConstruirHorario(reader);
                    lista.Add(horario);
                }
                reader.Close();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        //public List<Horario> BuscarHorario(string text)
        //{
        //    List<Horario> lista = new List<Horario>();
        //    try
        //    {
        //        var cadenaDeComando = "SELECT  HorarioId,Fecha,Hora,EventoId FROM Horarios WHERE Nombre LIKE @text";
        //        var comando = new SqlCommand(cadenaDeComando, conexion);
        //        comando.Parameters.AddWithValue("@text", $"%{text}%");
        //        var reader = comando.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            Horario horario = ConstruirHorario(reader);
        //            lista.Add(horario);
        //        }
        //        reader.Close();
        //        return lista;
        //    }
        //    catch (Exception e)
        //    {

        //        throw new Exception(e.Message);
        //    }
        //}

    }
}
