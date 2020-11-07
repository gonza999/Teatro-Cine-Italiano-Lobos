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
    public class RepositorioClasificaciones : IRepositorioClasificaciones
    {
        private readonly SqlConnection cn;

        public RepositorioClasificaciones(SqlConnection cn)
        {
            this.cn = cn;
        }
        public void Borrar(int id)
        {
            try
            {
                string cadenaComando = "DELETE FROM Clasificaciones WHERE ClasificacionId=@id";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Clasificacion clasificacion)
        {
            try
            {
                var cadenaDeComando = "SELECT ClasificacionId FROM Eventos WHERE ClasificacionId=@id";
                var comando = new SqlCommand(cadenaDeComando, cn);
                comando.Parameters.AddWithValue("@id", clasificacion.ClasificacionId);
                var reader = comando.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Existe(Clasificacion clasificacion)
        {
            try
            {
                SqlCommand comando;
                if (clasificacion.ClasificacionId == 0)
                {
                    string cadenaComando = "SELECT ClasificacionId, Clasificacion FROM Clasificaciones WHERE Clasificacion=@nombre";
                    comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", clasificacion.NombreClasificacion);

                }
                else
                {
                    string cadenaComando = "SELECT ClasificacionId, Clasificacion FROM Clasificaciones WHERE Clasificacion=@nombre AND ClasificacionId<>@id";
                    comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", clasificacion.NombreClasificacion);
                    comando.Parameters.AddWithValue("@id", clasificacion.ClasificacionId);


                }
                SqlDataReader reader = comando.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Clasificacion> GetLista()
        {
            List<Clasificacion> lista = new List<Clasificacion>();
            try
            {
                string cadenaComando = "SELECT ClasificacionId, Clasificacion FROM Clasificaciones";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Clasificacion clasificacion = ConstruirClasificacion(reader);
                    lista.Add(clasificacion);
                }
                reader.Close();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        private Clasificacion ConstruirClasificacion(SqlDataReader reader)
        {
            return new Clasificacion()
            {
                ClasificacionId = reader.GetInt32(0),
                NombreClasificacion = reader.GetString(1)
            };
        }

        public Clasificacion GetClasificacion(string nombreClasificacion)
        {
            try
            {
                Clasificacion clasificacion = null;
                string cadenaComando = "SELECT ClasificacionId, Clasificacion FROM Clasificaciones WHERE Clasificacion = @nombreClasificacion";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@nombreClasificacion", nombreClasificacion);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    clasificacion = ConstruirClasificacion(reader);
                    reader.Close();
                }
                return clasificacion;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Clasificacion GetClasificacionPorId(int id)
        {
            try
            {
                Clasificacion clasificacion = null;
                string cadenaComando = "SELECT ClasificacionId,Clasificacion FROM Clasificaciones WHERE ClasificacionId=@id";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    clasificacion = ConstruirClasificacion(reader);
                    reader.Close();
                }

                return clasificacion;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Guardar(Clasificacion clasificacion)
        {
            if (clasificacion.ClasificacionId == 0)
            {
                //Nuevo registro
                try
                {
                    string cadenaComando = "INSERT INTO Clasificaciones VALUES(@nombre)";
                    SqlCommand comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", clasificacion.NombreClasificacion);
                    comando.ExecuteNonQuery();
                    cadenaComando = "SELECT @@IDENTITY";
                    comando = new SqlCommand(cadenaComando, cn);
                    clasificacion.ClasificacionId = (int)(decimal)comando.ExecuteScalar();

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
                    string cadenaComando = "UPDATE Clasificaciones SET Clasificacion=@nombre WHERE ClasificacionId=@id";
                    SqlCommand comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", clasificacion.NombreClasificacion);
                    comando.Parameters.AddWithValue("@id", clasificacion.ClasificacionId);
                    comando.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }
        }

        public List<Clasificacion> BuscarClasificacion(string buscar)
        {
            List<Clasificacion> lista = new List<Clasificacion>();
            try
            {
                string cadenaComando = "SELECT ClasificacionId, Clasificacion FROM Clasificaciones " +
                    "WHERE Clasificacion like @buscar";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@buscar", $"%{buscar}%");
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Clasificacion clasificacion = ConstruirClasificacion(reader);
                    lista.Add(clasificacion);
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