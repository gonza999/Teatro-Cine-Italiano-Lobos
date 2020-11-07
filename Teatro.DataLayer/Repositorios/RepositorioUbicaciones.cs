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
    public class RepositorioUbicaciones:IRepositorioUbicaciones
    {
        private readonly SqlConnection cn;

        public RepositorioUbicaciones(SqlConnection cn)
        {
            this.cn = cn;
        }
        public void Borrar(int id)
        {
            try
            {
                string cadenaComando = "DELETE FROM Ubicaciones WHERE UbicacionId=@id";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Ubicacion ubicacion)
        {
            return false;
            //try
            //{
            //    var cadenaDeComando = "SELECT UbicacionId FROM Eventos WHERE UbicacionId=@id";
            //    var comando = new SqlCommand(cadenaDeComando, cn);
            //    comando.Parameters.AddWithValue("@id", ubicacion.UbicacionId);
            //    var reader = comando.ExecuteReader();
            //    return reader.HasRows;
            //}
            //catch (Exception e)
            //{

            //    throw new Exception(e.Message);
            //}
        }

        public bool Existe(Ubicacion ubicacion)
        {
            try
            {
                SqlCommand comando;
                if (ubicacion.UbicacionId == 0)
                {
                    string cadenaComando = "SELECT UbicacionId, Ubicacion FROM Ubicaciones WHERE Ubicacion=@nombre";
                    comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", ubicacion.NombreUbicacion);

                }
                else
                {
                    string cadenaComando = "SELECT UbicacionId, Ubicacion FROM Ubicaciones WHERE Ubicacion=@nombre AND UbicacionId<>@id";
                    comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", ubicacion.NombreUbicacion);
                    comando.Parameters.AddWithValue("@id", ubicacion.UbicacionId);


                }
                SqlDataReader reader = comando.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Ubicacion> GetLista()
        {
            List<Ubicacion> lista = new List<Ubicacion>();
            try
            {
                string cadenaComando = "SELECT UbicacionId, Ubicacion FROM Ubicaciones";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Ubicacion ubicacion = ConstruirUbicacion(reader);
                    lista.Add(ubicacion);
                }
                reader.Close();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        private Ubicacion ConstruirUbicacion(SqlDataReader reader)
        {
            return new Ubicacion()
            {
                UbicacionId = reader.GetInt32(0),
                NombreUbicacion = reader.GetString(1)
            };
        }

        public Ubicacion GetUbicacion(string nombreUbicacion)
        {
            try
            {
                Ubicacion ubicacion = null;
                string cadenaComando = "SELECT UbicacionId, Ubicacion FROM Ubicaciones WHERE Ubicacion = @nombreUbicacion";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@nombreUbicacion", nombreUbicacion);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    ubicacion = ConstruirUbicacion(reader);
                    reader.Close();
                }
                return ubicacion;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Ubicacion GetUbicacionPorId(int id)
        {
            try
            {
                Ubicacion ubicacion = null;
                string cadenaComando = "SELECT UbicacionId,Ubicacion FROM Ubicaciones WHERE UbicacionId=@id";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    ubicacion = ConstruirUbicacion(reader);
                    reader.Close();
                }

                return ubicacion;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Guardar(Ubicacion ubicacion)
        {
            if (ubicacion.UbicacionId == 0)
            {
                //Nuevo registro
                try
                {
                    string cadenaComando = "INSERT INTO Ubicaciones VALUES(@nombre)";
                    SqlCommand comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", ubicacion.NombreUbicacion);
                    comando.ExecuteNonQuery();
                    cadenaComando = "SELECT @@IDENTITY";
                    comando = new SqlCommand(cadenaComando, cn);
                    ubicacion.UbicacionId = (int)(decimal)comando.ExecuteScalar();

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
                    string cadenaComando = "UPDATE Ubicaciones SET Ubicacion=@nombre WHERE UbicacionId=@id";
                    SqlCommand comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", ubicacion.NombreUbicacion);
                    comando.Parameters.AddWithValue("@id", ubicacion.UbicacionId);
                    comando.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }
        }

        public List<Ubicacion> BuscarUbicacion(string buscar)
        {
            List<Ubicacion> lista = new List<Ubicacion>();
            try
            {
                string cadenaComando = "SELECT UbicacionId, Ubicacion FROM Ubicaciones " +
                    "WHERE Ubicacion like @buscar";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@buscar", $"%{buscar}%");
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Ubicacion ubicacion = ConstruirUbicacion(reader);
                    lista.Add(ubicacion);
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

