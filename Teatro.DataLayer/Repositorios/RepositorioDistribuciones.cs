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
    public class RepositorioDistribuciones:IRepositorioDistribuciones
    {
        private readonly SqlConnection cn;
        private SqlTransaction transaction;

        public RepositorioDistribuciones(SqlConnection cn)
        {
            this.cn = cn;
        }
        public RepositorioDistribuciones(SqlConnection cn,SqlTransaction transaction)
        {
            this.cn = cn;
            this.transaction = transaction;
        }
        public void Borrar(int id)
        {
            try
            {
                string cadenaComando = "DELETE FROM Distribuciones WHERE DistribucionId=@id";
                SqlCommand comando = new SqlCommand(cadenaComando, cn,transaction);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Distribucion distribucion)
        {
            try
            {
                var cadenaDeComando = "SELECT DistribucionId FROM Eventos WHERE DistribucionId=@id";
                var comando = new SqlCommand(cadenaDeComando, cn);
                comando.Parameters.AddWithValue("@id", distribucion.DistribucionId);
                var reader = comando.ExecuteReader();
                //if (!reader.HasRows)
                //{
                //     cadenaDeComando = "SELECT DistribucionId FROM DistribucionesUbicaciones WHERE DistribucionId=@id";
                //     comando = new SqlCommand(cadenaDeComando, cn);
                //    comando.Parameters.AddWithValue("@id", distribucion.DistribucionId);
                //     reader = comando.ExecuteReader();
                //}
                return reader.HasRows;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Existe(Distribucion distribucion)
        {
            try
            {
                SqlCommand comando;
                if (distribucion.DistribucionId == 0)
                {
                    string cadenaComando = "SELECT DistribucionId, Distribucion FROM Distribuciones WHERE Distribucion=@nombre";
                    comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", distribucion.NombreDistribucion);

                }
                else
                {
                    string cadenaComando = "SELECT DistribucionId, Distribucion FROM Distribuciones WHERE Distribucion=@nombre AND DistribucionId<>@id";
                    comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", distribucion.NombreDistribucion);
                    comando.Parameters.AddWithValue("@id", distribucion.DistribucionId);


                }
                SqlDataReader reader = comando.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Distribucion> GetLista()
        {
            List<Distribucion> lista = new List<Distribucion>();
            try
            {
                string cadenaComando = "SELECT DistribucionId, Distribucion FROM Distribuciones";
                SqlCommand comando = new SqlCommand(cadenaComando, cn,transaction);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Distribucion distribucion = ConstruirDistribucion(reader);
                    lista.Add(distribucion);
                }
                reader.Close();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        private Distribucion ConstruirDistribucion(SqlDataReader reader)
        {
            return new Distribucion()
            {
                DistribucionId = reader.GetInt32(0),
                NombreDistribucion = reader.GetString(1)
            };
        }

        public Distribucion GetDistribucion(string nombreDistribucion)
        {
            try
            {
                Distribucion distribucion = null;
                string cadenaComando = "SELECT DistribucionId, Distribucion FROM Distribuciones WHERE Distribucion = @nombreDistribucion";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@nombreDistribucion", nombreDistribucion);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    distribucion = ConstruirDistribucion(reader);
                    reader.Close();
                }
                return distribucion;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Distribucion GetDistribucionPorId(int id)
        {
            try
            {
                Distribucion distribucion = null;
                string cadenaComando = "SELECT DistribucionId,Distribucion FROM Distribuciones WHERE DistribucionId=@id";
                SqlCommand comando = new SqlCommand(cadenaComando, cn,transaction);
                comando.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    distribucion = ConstruirDistribucion(reader);
                    reader.Close();
                }

                return distribucion;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Guardar(Distribucion distribucion)
        {
            if (distribucion.DistribucionId == 0)
            {
                //Nuevo registro
                try
                {
                    string cadenaComando = "INSERT INTO Distribuciones VALUES(@nombre)";
                    SqlCommand comando = new SqlCommand(cadenaComando, cn,transaction);
                    comando.Parameters.AddWithValue("@nombre", distribucion.NombreDistribucion);
                    comando.ExecuteNonQuery();
                    cadenaComando = "SELECT @@IDENTITY";
                    comando = new SqlCommand(cadenaComando, cn,transaction);
                    distribucion.DistribucionId = (int)(decimal)comando.ExecuteScalar();

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
                    string cadenaComando = "UPDATE Distribuciones SET Distribucion=@nombre WHERE DistribucionId=@id";
                    SqlCommand comando = new SqlCommand(cadenaComando, cn,transaction);
                    comando.Parameters.AddWithValue("@nombre", distribucion.NombreDistribucion);
                    comando.Parameters.AddWithValue("@id", distribucion.DistribucionId);
                    comando.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }
        }

        public List<Distribucion> BuscarDistribucion(string buscar)
        {
            List<Distribucion> lista = new List<Distribucion>();
            try
            {
                string cadenaComando = "SELECT DistribucionId, Distribucion FROM Distribuciones " +
                    "WHERE Distribucion like @buscar";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@buscar", $"%{buscar}%");
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Distribucion distribucion = ConstruirDistribucion(reader);
                    lista.Add(distribucion);
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
