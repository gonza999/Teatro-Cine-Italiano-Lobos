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
    public class RepositorioFormasVentas:IRepositorioFormasVentas
    {
        private readonly SqlConnection cn;
        private SqlTransaction transaction;

        public RepositorioFormasVentas(SqlConnection cn)
        {
            this.cn = cn;
        }

        public RepositorioFormasVentas(SqlConnection cn, SqlTransaction transaction) : this(cn)
        {
            this.transaction = transaction;
        }

        public void Borrar(int id)
        {
            try
            {
                string cadenaComando = "DELETE FROM FormasVentas WHERE FormaVentaId=@id";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(FormaVenta formaVenta)
        {
            try
            {
                var cadenaDeComando = "SELECT FormaVentaId FROM Tickets WHERE FormaVentaId=@id";
                var comando = new SqlCommand(cadenaDeComando, cn);
                comando.Parameters.AddWithValue("@id", formaVenta.FormaVentaId);
                var reader = comando.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Existe(FormaVenta formaVenta)
        {
            try
            {
                SqlCommand comando;
                if (formaVenta.FormaVentaId == 0)
                {
                    string cadenaComando = "SELECT FormaVentaId, FormaVenta FROM FormasVentas WHERE FormaVenta=@nombre";
                    comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", formaVenta.NombreFormaVenta);

                }
                else
                {
                    string cadenaComando = "SELECT FormaVentaId, FormaVenta FROM FormasVentas WHERE FormaVenta=@nombre AND FormaVentaId<>@id";
                    comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", formaVenta.NombreFormaVenta);
                    comando.Parameters.AddWithValue("@id", formaVenta.FormaVentaId);


                }
                SqlDataReader reader = comando.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<FormaVenta> GetLista()
        {
            List<FormaVenta> lista = new List<FormaVenta>();
            try
            {
                string cadenaComando = "SELECT FormaVentaId, FormaVenta FROM FormasVentas";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    FormaVenta formaVenta = ConstruirFormaVenta(reader);
                    lista.Add(formaVenta);
                }
                reader.Close();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        private FormaVenta ConstruirFormaVenta(SqlDataReader reader)
        {
            return new FormaVenta()
            {
                FormaVentaId = reader.GetInt32(0),
                NombreFormaVenta = reader.GetString(1)
            };
        }

        public FormaVenta GetFormaVenta(string nombreFormaVenta)
        {
            try
            {
                FormaVenta formaVenta = null;
                string cadenaComando = "SELECT FormaVentaId, FormaVenta FROM FormasVentas WHERE FormaVenta = @nombreFormaVenta";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@nombreFormaVenta", nombreFormaVenta);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    formaVenta = ConstruirFormaVenta(reader);
                    reader.Close();
                }
                return formaVenta;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public FormaVenta GetFormaVentaPorId(int id)
        {
            try
            {
                FormaVenta formaVenta = null;
                string cadenaComando = "SELECT FormaVentaId,FormaVenta FROM FormasVentas WHERE FormaVentaId=@id";
                SqlCommand comando = new SqlCommand(cadenaComando, cn,transaction);
                comando.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    formaVenta = ConstruirFormaVenta(reader);
                    reader.Close();
                }

                return formaVenta;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Guardar(FormaVenta formaVenta)
        {
            if (formaVenta.FormaVentaId == 0)
            {
                //Nuevo registro
                try
                {
                    string cadenaComando = "INSERT INTO FormasVentas VALUES(@nombre)";
                    SqlCommand comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", formaVenta.NombreFormaVenta);
                    comando.ExecuteNonQuery();
                    cadenaComando = "SELECT @@IDENTITY";
                    comando = new SqlCommand(cadenaComando, cn);
                    formaVenta.FormaVentaId = (int)(decimal)comando.ExecuteScalar();

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
                    string cadenaComando = "UPDATE FormasVentas SET FormaVenta=@nombre WHERE FormaVentaId=@id";
                    SqlCommand comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", formaVenta.NombreFormaVenta);
                    comando.Parameters.AddWithValue("@id", formaVenta.FormaVentaId);
                    comando.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }
        }

        public List<FormaVenta> BuscarFormaVenta(string buscar)
        {
            List<FormaVenta> lista = new List<FormaVenta>();
            try
            {
                string cadenaComando = "SELECT FormaVentaId, FormaVenta FROM FormasVentas " +
                    "WHERE FormaVenta like @buscar";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@buscar", $"%{buscar}%");
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    FormaVenta formaVenta = ConstruirFormaVenta(reader);
                    lista.Add(formaVenta);
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

