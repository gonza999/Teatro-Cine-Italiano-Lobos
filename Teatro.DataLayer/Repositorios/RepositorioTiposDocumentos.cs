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
    public class RepositorioTiposDocumentos:IRepositorioTiposDocumentos
    {
        private readonly SqlConnection cn;
        private SqlTransaction transaction;

        public RepositorioTiposDocumentos(SqlConnection cn)
        {
            this.cn = cn;
        }

        public RepositorioTiposDocumentos(SqlConnection cn, SqlTransaction transaction) : this(cn)
        {
            this.transaction = transaction;
        }

        public void Borrar(int id)
        {
            try
            {
                string cadenaComando = "DELETE FROM TiposDocumentos WHERE TipoDocumentoId=@id";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(TipoDocumento tipoDocumento)
        {
            try
            {
                var cadenaDeComando = "SELECT TipoDocumentoId FROM Empleados WHERE TipoDocumentoId=@id";
                var comando = new SqlCommand(cadenaDeComando, cn);
                comando.Parameters.AddWithValue("@id", tipoDocumento.TipoDocumentoId);
                var reader = comando.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Existe(TipoDocumento tipoDocumento)
        {
            try
            {
                SqlCommand comando;
                if (tipoDocumento.TipoDocumentoId == 0)
                {
                    string cadenaComando = "SELECT TipoDocumentoId, TipoDocumento FROM TiposDocumentos WHERE TipoDocumento=@nombre";
                    comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", tipoDocumento.NombreTipoDocumento);

                }
                else
                {
                    string cadenaComando = "SELECT TipoDocumentoId, TipoDocumento FROM TiposDocumentos WHERE TipoDocumento=@nombre AND TipoDocumentoId<>@id";
                    comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", tipoDocumento.NombreTipoDocumento);
                    comando.Parameters.AddWithValue("@id", tipoDocumento.TipoDocumentoId);


                }
                SqlDataReader reader = comando.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<TipoDocumento> GetLista()
        {
            List<TipoDocumento> lista = new List<TipoDocumento>();
            try
            {
                string cadenaComando = "SELECT TipoDocumentoId, TipoDocumento FROM TiposDocumentos";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    TipoDocumento tipoDocumento = ConstruirTipoDocumento(reader);
                    lista.Add(tipoDocumento);
                }
                reader.Close();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        private TipoDocumento ConstruirTipoDocumento(SqlDataReader reader)
        {
            return new TipoDocumento()
            {
                TipoDocumentoId = reader.GetInt32(0),
                NombreTipoDocumento = reader.GetString(1)
            };
        }

        public TipoDocumento GetTipoDocumento(string nombreTipoDocumento)
        {
            try
            {
                TipoDocumento tipoDocumento = null;
                string cadenaComando = "SELECT TipoDocumentoId, TipoDocumento FROM TiposDocumentos WHERE TipoDocumento = @nombreTipoDocumento";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@nombreTipoDocumento", nombreTipoDocumento);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    tipoDocumento = ConstruirTipoDocumento(reader);
                    reader.Close();
                }
                return tipoDocumento;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public TipoDocumento GetTipoDocumentoPorId(int id)
        {
            try
            {
                TipoDocumento tipoDocumento = null;
                string cadenaComando = "SELECT TipoDocumentoId,TipoDocumento FROM TiposDocumentos WHERE TipoDocumentoId=@id";
                SqlCommand comando = new SqlCommand(cadenaComando, cn,transaction);
                comando.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    tipoDocumento = ConstruirTipoDocumento(reader);
                    reader.Close();
                }

                return tipoDocumento;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Guardar(TipoDocumento tipoDocumento)
        {
            if (tipoDocumento.TipoDocumentoId == 0)
            {
                //Nuevo registro
                try
                {
                    string cadenaComando = "INSERT INTO TiposDocumentos VALUES(@nombre)";
                    SqlCommand comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", tipoDocumento.NombreTipoDocumento);
                    comando.ExecuteNonQuery();
                    cadenaComando = "SELECT @@IDENTITY";
                    comando = new SqlCommand(cadenaComando, cn);
                    tipoDocumento.TipoDocumentoId = (int)(decimal)comando.ExecuteScalar();

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
                    string cadenaComando = "UPDATE TiposDocumentos SET TipoDocumento=@nombre WHERE TipoDocumentoId=@id";
                    SqlCommand comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", tipoDocumento.NombreTipoDocumento);
                    comando.Parameters.AddWithValue("@id", tipoDocumento.TipoDocumentoId);
                    comando.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }
        }

        public List<TipoDocumento> BuscarTipoDocumento(string buscar)
        {
            List<TipoDocumento> lista = new List<TipoDocumento>();
            try
            {
                string cadenaComando = "SELECT TipoDocumentoId, TipoDocumento FROM TiposDocumentos " +
                    "WHERE TipoDocumento like @buscar";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@buscar", $"%{buscar}%");
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    TipoDocumento tipoDocumento = ConstruirTipoDocumento(reader);
                    lista.Add(tipoDocumento);
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
