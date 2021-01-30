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
    public class RepositorioDistribucionesLocalidades : IRepositorioDistribucionesLocalidades
    {
        private readonly SqlConnection conexion;
        private IRepositorioDistribuciones repositorioDistribuciones;
        private IRepositorioUbicaciones repositorioUbicaciones;
        private IRepositorioLocalidades repositorioLocalidades;
        private readonly SqlTransaction transaction;

        public RepositorioDistribucionesLocalidades(SqlConnection conexion, IRepositorioDistribuciones repositorioDistribuciones,
             IRepositorioUbicaciones repositorioUbicaciones, SqlTransaction transaction)
        {
            this.conexion = conexion;
            this.repositorioUbicaciones = repositorioUbicaciones;
            this.repositorioDistribuciones = repositorioDistribuciones;
            this.transaction = transaction;
        }

        public RepositorioDistribucionesLocalidades(SqlConnection sqlConnection)
        {
            this.conexion = sqlConnection;
        }

        public RepositorioDistribucionesLocalidades(SqlConnection cn, SqlTransaction transaction)
        {
            conexion = cn;
            this.transaction = transaction;
        }

        public void Borrar(int id)
        {
            try
            {
                var cadenaDeComando = "DELETE DistribucionesLocalidades WHERE DistribucionId=@id";
                var comando = new SqlCommand(cadenaDeComando, conexion, transaction);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(DistribucionLocalidad distribucionLocalidad)
        {
            return false;
        }

        public bool Existe(DistribucionLocalidad distribucionLocalidad)
        {
            return false;
        }

        public List<DistribucionLocalidad> GetLista()
        {
            List<DistribucionLocalidad> lista = new List<DistribucionLocalidad>();
            try
            {
                var cadenaDeComando = "SELECT UbicacionId,DistribucionId,Precio " +
                    "FROM DistribucionesLocalidades";
                var comando = new SqlCommand(cadenaDeComando, conexion);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    DistribucionLocalidad distribucionLocalidad = ConstruirDistribucionLocalidad(reader);
                    lista.Add(distribucionLocalidad);
                }
                reader.Close();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public List<DistribucionLocalidad> GetLista(Distribucion distribucion)
        {
            bool fila = true;
            bool palcos = true;
            int filas = 0;
            List<DistribucionLocalidad> lista = new List<DistribucionLocalidad>();
            try
            {
                var cadenaDeComando = "SELECT LocalidadId,DistribucionId,Precio " +
                    "FROM DistribucionesLocalidades WHERE DistribucionId=@id";
                var comando = new SqlCommand(cadenaDeComando, conexion, transaction);
                comando.Parameters.AddWithValue("@id", distribucion.DistribucionId);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    repositorioLocalidades = new RepositorioLocalidades(conexion, transaction);
                    Localidad localidad = repositorioLocalidades.GetLocalidadPorId(reader.GetInt32(0));
                    if (localidad.Ubicacion.UbicacionId == 1)
                    {
                        if (filas != localidad.Fila)
                        {
                            if (fila)
                            {
                                DistribucionLocalidad distribucionLocalidad = ConstruirDistribucionLocalidad(reader);
                                lista.Add(distribucionLocalidad);
                                fila = false;
                            }
                     
                            filas = localidad.Fila;
                        }
                        else
                        {

                            fila = true;
                        }
                    }
                    else
                    {
                        if (palcos)
                        {
                            DistribucionLocalidad distribucionLocalidad = ConstruirDistribucionLocalidad(reader, 2);
                            lista.Add(distribucionLocalidad);
                            palcos = false;
                        }
                    }
                }
                reader.Close();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        private DistribucionLocalidad ConstruirDistribucionLocalidad(SqlDataReader reader, int v)
        {
            DistribucionLocalidad distribucionLocalidad = new DistribucionLocalidad();
            repositorioLocalidades = new RepositorioLocalidades(conexion, transaction);
            Ubicacion ubicacion = new Ubicacion() { UbicacionId = 2 };
            distribucionLocalidad.Localidades = repositorioLocalidades.GetLista(ubicacion);
            repositorioDistribuciones = new RepositorioDistribuciones(conexion, transaction);
            distribucionLocalidad.Distribucion = repositorioDistribuciones.GetDistribucionPorId(reader.GetInt32(1));
            distribucionLocalidad.Precio = reader.GetDecimal(2);
            return distribucionLocalidad;
        }

        private DistribucionLocalidad ConstruirDistribucionLocalidad(SqlDataReader reader)
        {
            DistribucionLocalidad distribucionLocalidad = new DistribucionLocalidad();
            repositorioLocalidades = new RepositorioLocalidades(conexion, transaction);
            Localidad localidad = repositorioLocalidades.GetLocalidadPorId(reader.GetInt32(0));
            distribucionLocalidad.Localidades = repositorioLocalidades.GetLista(localidad.Fila);
            repositorioDistribuciones = new RepositorioDistribuciones(conexion, transaction);
            distribucionLocalidad.Distribucion = repositorioDistribuciones.GetDistribucionPorId(reader.GetInt32(1));
            distribucionLocalidad.Precio = reader.GetDecimal(2);
            return distribucionLocalidad;
        }



        public void Guardar(DistribucionLocalidad distribucionLocalidad)
        {
            try
            {
                foreach (var l in distribucionLocalidad.Localidades)
                {
                    var cadenaDeComando = "INSERT INTO DistribucionesLocalidades ( DistribucionId,LocalidadId,Precio) " +
                                  "VALUES (@distribucionId,@localidadId,@precio)";
                    var comando = new SqlCommand(cadenaDeComando, conexion, transaction);
                    comando.Parameters.AddWithValue("@distribucionId", distribucionLocalidad.Distribucion.DistribucionId);
                    comando.Parameters.AddWithValue("@localidadId", l.LocalidadId);
                    comando.Parameters.AddWithValue("@precio", distribucionLocalidad.Precio);
                    comando.ExecuteNonQuery();

                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public List<DistribucionLocalidad> BuscarDistribucionLocalidad(string text)
        {
            List<DistribucionLocalidad> lista = new List<DistribucionLocalidad>();
            try
            {
                var cadenaDeComando = "SELECT DistribucionLocalidadId,DistribucionId,Numero,UbicacionId  " +
                    "FROM DistribucionesLocalidades WHERE DistribucionLocalidad like @text";
                var comando = new SqlCommand(cadenaDeComando, conexion);
                comando.Parameters.AddWithValue("@text", $"%{text}%");
                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    DistribucionLocalidad distribucionLocalidad = ConstruirDistribucionLocalidad(reader);
                    lista.Add(distribucionLocalidad);
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
