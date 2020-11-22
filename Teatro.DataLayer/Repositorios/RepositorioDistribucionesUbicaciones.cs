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
    public class RepositorioDistribucionesUbicaciones:IRepositorioDistribucionesUbicaciones
    {
        private readonly SqlConnection conexion;
        private IRepositorioDistribuciones repositorioDistribuciones;
        private IRepositorioUbicaciones repositorioUbicaciones;
        private readonly SqlTransaction transaction;

        public RepositorioDistribucionesUbicaciones(SqlConnection conexion, IRepositorioDistribuciones repositorioDistribuciones,
             IRepositorioUbicaciones repositorioUbicaciones,SqlTransaction transaction)
        {
            this.conexion = conexion;
            this.repositorioUbicaciones = repositorioUbicaciones;
            this.repositorioDistribuciones = repositorioDistribuciones;
            this.transaction = transaction;
        }

        public RepositorioDistribucionesUbicaciones(SqlConnection sqlConnection)
        {
            this.conexion = sqlConnection;
        }

        public RepositorioDistribucionesUbicaciones(SqlConnection cn, SqlTransaction transaction)
        {
            conexion = cn;
            this.transaction = transaction;
        }

        public void Borrar(int id)
        {
            try
            {
                var cadenaDeComando = "DELETE DistribucionesUbicaciones WHERE DistribucionId=@disId";
                var comando = new SqlCommand(cadenaDeComando, conexion);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(DistribucionUbicacion distribucionUbicacion)
        {
            return false;
        }

        public bool Existe(DistribucionUbicacion distribucionUbicacion)
        {
            return false;
        }

        public List<DistribucionUbicacion> GetLista()
        {
            List<DistribucionUbicacion> lista = new List<DistribucionUbicacion>();
            try
            {
                var cadenaDeComando = "SELECT UbicacionId,DistribucionId,Precio " +
                    "FROM DistribucionesUbicaciones";
                var comando = new SqlCommand(cadenaDeComando, conexion);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    DistribucionUbicacion distribucionUbicacion = ConstruirDistribucionUbicacion(reader);
                    lista.Add(distribucionUbicacion);
                }
                reader.Close();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public List<DistribucionUbicacion> GetLista(Distribucion distribucion)
        {
            List<DistribucionUbicacion> lista = new List<DistribucionUbicacion>();
            try
            {
                var cadenaDeComando = "SELECT UbicacionId,DistribucionId,Precio " +
                    "FROM DistribucionesUbicaciones WHERE DistribucionId=@id";
                var comando = new SqlCommand(cadenaDeComando, conexion,transaction);
                comando.Parameters.AddWithValue("@id",distribucion.DistribucionId);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    DistribucionUbicacion distribucionUbicacion = ConstruirDistribucionUbicacion(reader);
                    lista.Add(distribucionUbicacion);
                }
                reader.Close();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        private DistribucionUbicacion ConstruirDistribucionUbicacion(SqlDataReader reader)
        {
            DistribucionUbicacion distribucionUbicacion = new DistribucionUbicacion();
            repositorioUbicaciones = new RepositorioUbicaciones(conexion,transaction);
            distribucionUbicacion.Ubicacion =repositorioUbicaciones.GetUbicacionPorId( reader.GetInt32(0));
            repositorioDistribuciones = new RepositorioDistribuciones(conexion,transaction);
            distribucionUbicacion.Distribucion = repositorioDistribuciones.GetDistribucionPorId(reader.GetInt32(1));
            distribucionUbicacion.Precio = reader.GetDecimal(2);
            return distribucionUbicacion;
        }



        public void Guardar(DistribucionUbicacion distribucionUbicacion,bool opcion)
        {

            if (opcion)
            {
                try
                {
                    var cadenaDeComando = "INSERT INTO DistribucionesUbicaciones ( DistribucionId,UbicacionId,Precio) " +
                        "VALUES (@distribucionId,@ubicacionId,@precio)";
                    var comando = new SqlCommand(cadenaDeComando, conexion,transaction);
                    comando.Parameters.AddWithValue("@distribucionId", distribucionUbicacion.Distribucion.DistribucionId);
                    comando.Parameters.AddWithValue("@ubicacionId", distribucionUbicacion.Ubicacion.UbicacionId);
                    comando.Parameters.AddWithValue("@precio", distribucionUbicacion.Precio);
                    comando.ExecuteNonQuery();

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
                    string cadenaComando = "UPDATE DistribucionesUbicaciones SET Precio=@precio " +
                        " WHERE DistribucionId=@id AND UbicacionId=@ubicacion";
                    SqlCommand comando = new SqlCommand(cadenaComando, conexion,transaction);
                    comando.Parameters.AddWithValue("@precio", distribucionUbicacion.Precio);
                    comando.Parameters.AddWithValue("@ubicacion", distribucionUbicacion.Ubicacion.UbicacionId);
                    comando.Parameters.AddWithValue("@id", distribucionUbicacion.Distribucion.DistribucionId);

                    comando.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }
        }
        public List<DistribucionUbicacion> BuscarDistribucionUbicacion(string text)
        {
            List<DistribucionUbicacion> lista = new List<DistribucionUbicacion>();
            try
            {
                var cadenaDeComando = "SELECT DistribucionUbicacionId,DistribucionId,Numero,UbicacionId  " +
                    "FROM DistribucionesUbicaciones WHERE DistribucionUbicacion like @text";
                var comando = new SqlCommand(cadenaDeComando, conexion);
                comando.Parameters.AddWithValue("@text", $"%{text}%");
                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    DistribucionUbicacion distribucionUbicacion = ConstruirDistribucionUbicacion(reader);
                    lista.Add(distribucionUbicacion);
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
