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
    public class RepositorioPlantas:IRepositorioPlantas
    {
        private readonly SqlConnection cn;
        private SqlTransaction transaction;

        public RepositorioPlantas(SqlConnection cn)
        {
            this.cn = cn;
        }

        public RepositorioPlantas(SqlConnection cn, SqlTransaction transaction) : this(cn)
        {
            this.transaction = transaction;
        }

        public void Borrar(int id)
        {
            try
            {
                string cadenaComando = "DELETE FROM Plantas WHERE PlantaId=@id";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Planta planta)
        {
            return false;
        }

        public bool Existe(Planta planta)
        {
            try
            {
                SqlCommand comando;
                if (planta.PlantaId == 0)
                {
                    string cadenaComando = "SELECT PlantaId, Planta FROM Plantas WHERE Planta=@nombre";
                    comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", planta.NombrePlanta);

                }
                else
                {
                    string cadenaComando = "SELECT PlantaId, Planta FROM Plantas WHERE Planta=@nombre AND PlantaId<>@id";
                    comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", planta.NombrePlanta);
                    comando.Parameters.AddWithValue("@id", planta.PlantaId);


                }
                SqlDataReader reader = comando.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Planta> GetLista()
        {
            List<Planta> lista = new List<Planta>();
            try
            {
                string cadenaComando = "SELECT PlantaId, Planta FROM Plantas";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Planta planta = ConstruirPlanta(reader);
                    lista.Add(planta);
                }
                reader.Close();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        private Planta ConstruirPlanta(SqlDataReader reader)
        {
            return new Planta()
            {
                PlantaId = reader.GetInt32(0),
                NombrePlanta = reader.GetString(1)
            };
        }

        public Planta GetPlanta(string nombrePlanta)
        {
            try
            {
                Planta planta = null;
                string cadenaComando = "SELECT PlantaId, Planta FROM Plantas WHERE Planta = @nombrePlanta";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@nombrePlanta", nombrePlanta);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    planta = ConstruirPlanta(reader);
                    reader.Close();
                }
                return planta;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Planta GetPlantaPorId(int id)
        {
            try
            {
                Planta planta = null;
                string cadenaComando = "SELECT PlantaId,Planta FROM Plantas WHERE PlantaId=@id";
                SqlCommand comando = new SqlCommand(cadenaComando, cn,transaction);
                comando.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    planta = ConstruirPlanta(reader);
                    reader.Close();
                }

                return planta;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Guardar(Planta planta)
        {
            if (planta.PlantaId == 0)
            {
                //Nuevo registro
                try
                {
                    string cadenaComando = "INSERT INTO Plantas VALUES(@nombre)";
                    SqlCommand comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", planta.NombrePlanta);
                    comando.ExecuteNonQuery();
                    cadenaComando = "SELECT @@IDENTITY";
                    comando = new SqlCommand(cadenaComando, cn);
                    planta.PlantaId = (int)(decimal)comando.ExecuteScalar();

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
                    string cadenaComando = "UPDATE Plantas SET Planta=@nombre WHERE PlantaId=@id";
                    SqlCommand comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", planta.NombrePlanta);
                    comando.Parameters.AddWithValue("@id", planta.PlantaId);
                    comando.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }
        }

        public List<Planta> BuscarPlanta(string buscar)
        {
            List<Planta> lista = new List<Planta>();
            try
            {
                string cadenaComando = "SELECT PlantaId, Planta FROM Plantas " +
                    "WHERE Planta like @buscar";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@buscar", $"%{buscar}%");
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Planta planta = ConstruirPlanta(reader);
                    lista.Add(planta);
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
