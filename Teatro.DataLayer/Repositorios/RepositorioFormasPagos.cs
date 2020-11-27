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
    public class RepositorioFormasPagos:IRepositorioFormasPagos
    {
        private readonly SqlConnection cn;
        private SqlTransaction transaction;

        public RepositorioFormasPagos(SqlConnection cn)
        {
            this.cn = cn;
        }

        public RepositorioFormasPagos(SqlConnection cn, SqlTransaction transaction) : this(cn)
        {
            this.transaction = transaction;
        }

        public void Borrar(int id)
        {
            try
            {
                string cadenaComando = "DELETE FROM FormasPagos WHERE FormaPagoId=@id";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(FormaPago formaPago)
        {
            try
            {
                var cadenaDeComando = "SELECT FormaPagoId FROM Tickets WHERE FormaPagoId=@id";
                var comando = new SqlCommand(cadenaDeComando, cn);
                comando.Parameters.AddWithValue("@id", formaPago.FormaPagoId);
                var reader = comando.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Existe(FormaPago formaPago)
        {
            try
            {
                SqlCommand comando;
                if (formaPago.FormaPagoId == 0)
                {
                    string cadenaComando = "SELECT FormaPagoId, FormaPago FROM FormasPagos WHERE FormaPago=@nombre";
                    comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", formaPago.NombreFormaPago);

                }
                else
                {
                    string cadenaComando = "SELECT FormaPagoId, FormaPago FROM FormasPagos WHERE FormaPago=@nombre AND FormaPagoId<>@id";
                    comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", formaPago.NombreFormaPago);
                    comando.Parameters.AddWithValue("@id", formaPago.FormaPagoId);


                }
                SqlDataReader reader = comando.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<FormaPago> GetLista()
        {
            List<FormaPago> lista = new List<FormaPago>();
            try
            {
                string cadenaComando = "SELECT FormaPagoId, FormaPago FROM FormasPagos";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    FormaPago formaPago = ConstruirFormaPago(reader);
                    lista.Add(formaPago);
                }
                reader.Close();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        private FormaPago ConstruirFormaPago(SqlDataReader reader)
        {
            return new FormaPago()
            {
                FormaPagoId = reader.GetInt32(0),
                NombreFormaPago = reader.GetString(1)
            };
        }

        public FormaPago GetFormaPago(string nombreFormaPago)
        {
            try
            {
                FormaPago formaPago = null;
                string cadenaComando = "SELECT FormaPagoId, FormaPago FROM FormasPagos WHERE FormaPago = @nombreFormaPago";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@nombreFormaPago", nombreFormaPago);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    formaPago = ConstruirFormaPago(reader);
                    reader.Close();
                }
                return formaPago;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public FormaPago GetFormaPagoPorId(int id)
        {
            try
            {
                FormaPago formaPago = null;
                string cadenaComando = "SELECT FormaPagoId,FormaPago FROM FormasPagos WHERE FormaPagoId=@id";
                SqlCommand comando = new SqlCommand(cadenaComando, cn,transaction);
                comando.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    formaPago = ConstruirFormaPago(reader);
                    reader.Close();
                }

                return formaPago;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Guardar(FormaPago formaPago)
        {
            if (formaPago.FormaPagoId == 0)
            {
                //Nuevo registro
                try
                {
                    string cadenaComando = "INSERT INTO FormasPagos VALUES(@nombre)";
                    SqlCommand comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", formaPago.NombreFormaPago);
                    comando.ExecuteNonQuery();
                    cadenaComando = "SELECT @@IDENTITY";
                    comando = new SqlCommand(cadenaComando, cn);
                    formaPago.FormaPagoId = (int)(decimal)comando.ExecuteScalar();

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
                    string cadenaComando = "UPDATE FormasPagos SET FormaPago=@nombre WHERE FormaPagoId=@id";
                    SqlCommand comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", formaPago.NombreFormaPago);
                    comando.Parameters.AddWithValue("@id", formaPago.FormaPagoId);
                    comando.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }
        }

        public List<FormaPago> BuscarFormaPago(string buscar)
        {
            List<FormaPago> lista = new List<FormaPago>();
            try
            {
                string cadenaComando = "SELECT FormaPagoId, FormaPago FROM FormasPagos " +
                    "WHERE FormaPago like @buscar";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@buscar", $"%{buscar}%");
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    FormaPago formaPago = ConstruirFormaPago(reader);
                    lista.Add(formaPago);
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
