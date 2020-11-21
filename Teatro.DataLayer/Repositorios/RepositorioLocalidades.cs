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
    public class RepositorioLocalidades:IRepositorioLocalidades
    {
        private readonly SqlConnection conexion;
        private IRepositorioPlantas repositorioPlantas;
        private IRepositorioUbicaciones repositorioUbicaciones;
        private readonly SqlTransaction transaction;

        public RepositorioLocalidades(SqlConnection conexion, IRepositorioPlantas repositorioPlantas,
             IRepositorioUbicaciones repositorioUbicaciones)
        {
            this.conexion = conexion;
            this.repositorioUbicaciones = repositorioUbicaciones;
            this.repositorioPlantas = repositorioPlantas;
        }

        public RepositorioLocalidades(SqlConnection sqlConnection)
        {
            this.conexion = sqlConnection;
        }

        public RepositorioLocalidades(SqlConnection cn, SqlTransaction transaction)
        {
            conexion = cn;
            this.transaction = transaction;
        }

        public void Borrar(int id)
        {
            try
            {
                var cadenaDeComando = "DELETE Localidades WHERE LocalidadId=@id";
                var comando = new SqlCommand(cadenaDeComando, conexion);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Localidad localidad)
        {
            try
            {
                var cadenaDeComando = "SELECT LocalidadId FROM Tickets WHERE LocalidadId=@id";
                var comando = new SqlCommand(cadenaDeComando, conexion);
                comando.Parameters.AddWithValue("@id", localidad.LocalidadId);
                var reader = comando.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Existe(Localidad localidad)
        {
            try
            {
                SqlCommand comando;
                if (localidad.LocalidadId == 0)
                {
                    string cadenaComando = "SELECT LocalidadId FROM Localidades WHERE Numero=@num AND UbicacionId=@ubi";
                    comando = new SqlCommand(cadenaComando, conexion);
                    comando.Parameters.AddWithValue("@num", localidad.Numero);
                    comando.Parameters.AddWithValue("@ubi",localidad.Ubicacion.UbicacionId);

                }
                else
                {
                    string cadenaComando = "SELECT LocalidadId FROM Localidades WHERE Numero=@num AND UbicacionId=@ubi AND LocalidadId<>@id";
                    comando = new SqlCommand(cadenaComando, conexion);
                    comando.Parameters.AddWithValue("@num", localidad.Numero);
                    comando.Parameters.AddWithValue("@ubi", localidad.Ubicacion.UbicacionId);
                    comando.Parameters.AddWithValue("@id", localidad.LocalidadId);


                }
                SqlDataReader reader = comando.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Localidad> GetLista()
        {
            List<Localidad> lista = new List<Localidad>();
            try
            {
                var cadenaDeComando = "SELECT LocalidadId,PlantaId,Numero,UbicacionId " +
                    "FROM Localidades";
                var comando = new SqlCommand(cadenaDeComando, conexion);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Localidad localidad = ConstruirLocalidad(reader);
                    lista.Add(localidad);
                }
                reader.Close();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        private Localidad ConstruirLocalidad(SqlDataReader reader)
        {
            Localidad localidad = new Localidad();
            localidad.LocalidadId = reader.GetInt32(0);
            repositorioUbicaciones = new RepositorioUbicaciones(conexion);
            repositorioPlantas = new RepositorioPlantas(conexion);
            localidad.Planta = repositorioPlantas.GetPlantaPorId(reader.GetInt32(1));
            localidad.Numero = reader.GetInt32(2);
            localidad.Ubicacion = repositorioUbicaciones.GetUbicacionPorId(reader.GetInt32(3));
            return localidad;
        }

        public Localidad GetLocalidadPorId(int id)
        {
            Localidad localidad = null;
            try
            {
                var cadenaDeComando = "SELECT  FROM  LocalidadId,PlantaId,Numero,UbicacionId FROM Localidades WHERE LocalidadId=@id";
                var comando = new SqlCommand(cadenaDeComando, conexion);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    localidad = ConstruirLocalidad(reader);
                    reader.Close();
                }
                return localidad;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Guardar(Localidad localidad)
        {

            if (localidad.LocalidadId == 0)
            {
                try
                {
                    var cadenaDeComando = "INSERT INTO Localidades ( PlantaId,Numero,UbicacionId) " +
                        "VALUES (@plantaId,@numero,@ubicacionId)";
                    var comando = new SqlCommand(cadenaDeComando, conexion);
                    comando.Parameters.AddWithValue("@plantaId", localidad.Planta.PlantaId);
                    comando.Parameters.AddWithValue("@numero", localidad.Numero);
                    comando.Parameters.AddWithValue("@ubicacionId", localidad.Ubicacion.UbicacionId);
                    comando.ExecuteNonQuery();
                    cadenaDeComando = "SELECT @@Identity";
                    comando = new SqlCommand(cadenaDeComando, conexion);
                    localidad.LocalidadId = (int)(decimal)comando.ExecuteScalar();
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
                    string cadenaComando = "UPDATE Localidades SET PlantaId=@planta,Numero=@numero,UbicacionId=@ubicacion " +
                        " WHERE LocalidadId=@id";
                    SqlCommand comando = new SqlCommand(cadenaComando, conexion);
                    comando.Parameters.AddWithValue("@planta", localidad.Planta.PlantaId);
                    comando.Parameters.AddWithValue("@numero", localidad.Numero);
                    comando.Parameters.AddWithValue("@ubicacion", localidad.Ubicacion.UbicacionId);
                    comando.Parameters.AddWithValue("@id", localidad.LocalidadId);

                    comando.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }
        }
        public List<Localidad> BuscarLocalidad(string text)
        {
            List<Localidad> lista = new List<Localidad>();
            try
            {
                var cadenaDeComando = "SELECT LocalidadId,PlantaId,Numero,UbicacionId  " +
                    "FROM Localidades WHERE Localidad like @text";
                var comando = new SqlCommand(cadenaDeComando, conexion);
                comando.Parameters.AddWithValue("@text", $"%{text}%");
                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Localidad localidad = ConstruirLocalidad(reader);
                    lista.Add(localidad);
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
