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
    public class RepositorioTipoEvento : IRepositorioTipoEventos
    {
        private readonly SqlConnection cn;

        public RepositorioTipoEvento(SqlConnection cn)
        {
            this.cn = cn;
        }
        public void Borrar(int id)
        {
            try
            {
                string cadenaComando = "DELETE FROM TiposEventos WHERE TipoEventoId=@id";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(TipoEvento tipoevento)
        {
            try
            {
                var cadenaDeComando = "SELECT TipoEventoId FROM Eventos WHERE TipoEventoId=@id";
                var comando = new SqlCommand(cadenaDeComando, cn);
                comando.Parameters.AddWithValue("@id", tipoevento.TipoEventoId);
                var reader = comando.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Existe(TipoEvento tipoevento)
        {
            try
            {
                SqlCommand comando;
                if (tipoevento.TipoEventoId == 0)
                {
                    string cadenaComando = "SELECT TipoEventoId, TipoEvento FROM TiposEventos WHERE TipoEvento=@nombre";
                    comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", tipoevento.NombreTipoEvento);

                }
                else
                {
                    string cadenaComando = "SELECT TipoEventoId, TipoEvento FROM TiposEventos WHERE TipoEvento=@nombre AND TipoEventoId<>@id";
                    comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", tipoevento.NombreTipoEvento);
                    comando.Parameters.AddWithValue("@id", tipoevento.TipoEventoId);


                }
                SqlDataReader reader = comando.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<TipoEvento> GetLista()
        {
            List<TipoEvento> lista = new List<TipoEvento>();
            try
            {
                string cadenaComando = "SELECT TipoEventoId, TipoEvento FROM TiposEventos";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    TipoEvento tipoevento = ConstruirTipoEvento(reader);
                    lista.Add(tipoevento);
                }
                reader.Close();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        private TipoEvento ConstruirTipoEvento(SqlDataReader reader)
        {
            return new TipoEvento()
            {
                TipoEventoId = reader.GetInt32(0),
                NombreTipoEvento = reader.GetString(1)
            };
        }

        public TipoEvento GetTipoEvento(string nombreTipoEvento)
        {
            try
            {
                TipoEvento tipoevento = null;
                string cadenaComando = "SELECT TipoEventoId, TipoEvento FROM TiposEventos WHERE TipoEvento = @nombreTipoEvento";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@nombreTipoEvento", nombreTipoEvento);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    tipoevento = ConstruirTipoEvento(reader);
                    reader.Close();
                }
                return tipoevento;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public TipoEvento GetTipoEventoPorId(int id)
        {
            try
            {
                TipoEvento tipoevento = null;
                string cadenaComando = "SELECT TipoEventoId,TipoEvento FROM TiposEventos WHERE TipoEventoId=@id";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    tipoevento = ConstruirTipoEvento(reader);
                    reader.Close();
                }

                return tipoevento;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Guardar(TipoEvento tipoevento)
        {
            if (tipoevento.TipoEventoId == 0)
            {
                //Nuevo registro
                try
                {
                    string cadenaComando = "INSERT INTO TiposEventos VALUES(@nombre)";
                    SqlCommand comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", tipoevento.NombreTipoEvento);
                    comando.ExecuteNonQuery();
                    cadenaComando = "SELECT @@IDENTITY";
                    comando = new SqlCommand(cadenaComando, cn);
                    tipoevento.TipoEventoId = (int)(decimal)comando.ExecuteScalar();

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
                    string cadenaComando = "UPDATE TiposEventos SET TipoEvento=@nombre WHERE TipoEventoId=@id";
                    SqlCommand comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nombre", tipoevento.NombreTipoEvento);
                    comando.Parameters.AddWithValue("@id", tipoevento.TipoEventoId);
                    comando.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }
        }

        public List<TipoEvento> BuscarTipoEvento(string buscar)
        {
            List<TipoEvento> lista = new List<TipoEvento>();
            try
            {
                string cadenaComando = "SELECT TipoEventoId, TipoEvento FROM TiposEventos " +
                    "WHERE TipoEvento like @buscar";
                SqlCommand comando = new SqlCommand(cadenaComando, cn);
                comando.Parameters.AddWithValue("@buscar", $"%{buscar}%");
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    TipoEvento tipoevento = ConstruirTipoEvento(reader);
                    lista.Add(tipoevento);
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
