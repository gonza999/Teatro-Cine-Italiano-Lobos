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
    public class RepositorioEmpleados:IRepositorioEmpleados
    {
        private readonly SqlConnection conexion;
        private IRepositorioTiposDocumentos repositorioTiposDocumentos;
        private readonly SqlTransaction transaction;

        public RepositorioEmpleados(SqlConnection conexion,
             IRepositorioTiposDocumentos repositorioTiposDocumentos)
        {
            this.conexion = conexion;
            this.repositorioTiposDocumentos = repositorioTiposDocumentos;
        }

        public RepositorioEmpleados(SqlConnection sqlConnection)
        {
            this.conexion = sqlConnection;
        }

        public RepositorioEmpleados(SqlConnection cn, SqlTransaction transaction)
        {
            conexion = cn;
            this.transaction = transaction;
        }

        public void Borrar(int id)
        {
            try
            {
                var cadenaDeComando = "DELETE Empleados WHERE EmpleadoId=@id";
                var comando = new SqlCommand(cadenaDeComando, conexion);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Empleado empleado)
        {
            try
            {
                var cadenaDeComando = "SELECT EmpleadoId FROM Ventas WHERE EmpleadoId=@id";
                var comando = new SqlCommand(cadenaDeComando, conexion);
                comando.Parameters.AddWithValue("@id", empleado.EmpleadoId);
                var reader = comando.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Existe(Empleado empleado)
        {
            try
            {
                SqlCommand comando;
                if (empleado.EmpleadoId == 0)
                {
                    string cadenaComando = "SELECT EmpleadoId FROM Empleados WHERE TipoDocumentoId=@id AND NroDocumento=@doc";
                    comando = new SqlCommand(cadenaComando, conexion);
                    comando.Parameters.AddWithValue("@id", empleado.TipoDocumento.TipoDocumentoId);
                    comando.Parameters.AddWithValue("@doc",empleado.NumeroDocumento);

                }
                else
                {
                    string cadenaComando = "SELECT EmpleadoId FROM Empleados WHERE TipoDocumentoId=@idTipo AND NroDocumento=@doc AND EmpleadoId<>@id";
                    comando = new SqlCommand(cadenaComando, conexion);
                    comando.Parameters.AddWithValue("@idTipo", empleado.TipoDocumento.TipoDocumentoId);
                    comando.Parameters.AddWithValue("@doc", empleado.NumeroDocumento);
                    comando.Parameters.AddWithValue("@id", empleado.EmpleadoId);


                }
                SqlDataReader reader = comando.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Empleado> GetLista()
        {
            List<Empleado> lista = new List<Empleado>();
            try
            {
                var cadenaDeComando = "SELECT EmpleadoId,Nombre,Apellido,TipoDocumentoId,NroDocumento,TelefonoFijo, " +
                    "TelefonoMovil, Mail FROM Empleados";
                var comando = new SqlCommand(cadenaDeComando, conexion);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Empleado empleado = ConstruirEmpleado(reader);
                    lista.Add(empleado);
                }
                reader.Close();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        private Empleado ConstruirEmpleado(SqlDataReader reader)
        {
            Empleado empleado = new Empleado();
            empleado.EmpleadoId = reader.GetInt32(0);
            empleado.Nombre = reader.GetString(1);
            empleado.Apellido = reader.GetString(2);
            repositorioTiposDocumentos = new RepositorioTiposDocumentos(conexion,transaction);
            empleado.TipoDocumento = repositorioTiposDocumentos.GetTipoDocumentoPorId(reader.GetInt32(3));
            empleado.NumeroDocumento = reader.GetString(4);
            empleado.TelefonoFijo = reader.GetString(5);
            empleado.TelefonoMovil = reader.GetString(6);
            empleado.Mail = reader.GetString(7);
            return empleado;
        }

        public Empleado GetEmpleadoPorId(int id)
        {
            Empleado empleado = null;
            try
            {
                var cadenaDeComando = "SELECT  EmpleadoId,Nombre,Apellido,TipoDocumentoId,NroDocumento,TelefonoFijo, " +
                    "TelefonoMovil, Mail  FROM Empleados WHERE EmpleadoId=@id";
                var comando = new SqlCommand(cadenaDeComando, conexion,transaction);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    empleado = ConstruirEmpleado(reader);
                    reader.Close();
                }
                return empleado;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Guardar(Empleado empleado)
        {

            if (empleado.EmpleadoId == 0)
            {
                try
                {
                    var cadenaDeComando = "INSERT INTO Empleados (Nombre,Apellido,TipoDocumentoId,NroDocumento,TelefonoFijo, " +
                    "TelefonoMovil, Mail ) VALUES (@nombre,@apellido,@tipo,@nro,@telF,@telM,@mail)";
                    var comando = new SqlCommand(cadenaDeComando, conexion);
                    comando.Parameters.AddWithValue("@nombre", empleado.Nombre);
                    comando.Parameters.AddWithValue("@apellido", empleado.Apellido);
                    comando.Parameters.AddWithValue("@tipo", empleado.TipoDocumento.TipoDocumentoId);
                    comando.Parameters.AddWithValue("@nro", empleado.NumeroDocumento);
                    comando.Parameters.AddWithValue("@telF", empleado.TelefonoFijo);
                    comando.Parameters.AddWithValue("@telM", empleado.TelefonoMovil);
                    comando.Parameters.AddWithValue("@mail", empleado.Mail);
                    comando.ExecuteNonQuery();
                    cadenaDeComando = "SELECT @@Identity";
                    comando = new SqlCommand(cadenaDeComando, conexion);
                    empleado.EmpleadoId = (int)(decimal)comando.ExecuteScalar();
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
                    string cadenaComando = "UPDATE Empleados SET Nombre=@nombre,Apellido=@apellido," +
                        "TipoDocumentoId=@tipo,NroDocumento=@nro,TelefonoFijo=@telF,TelefonoMovil=@telM," +
                        "Mail=@mail WHERE EmpleadoId=@id";
                    SqlCommand comando = new SqlCommand(cadenaComando, conexion);
                    comando.Parameters.AddWithValue("@nombre", empleado.Nombre);
                    comando.Parameters.AddWithValue("@apellido", empleado.Apellido);
                    comando.Parameters.AddWithValue("@tipo", empleado.TipoDocumento.TipoDocumentoId);
                    comando.Parameters.AddWithValue("@nro", empleado.NumeroDocumento);
                    comando.Parameters.AddWithValue("@telF", empleado.TelefonoFijo);
                    comando.Parameters.AddWithValue("@telM", empleado.TelefonoMovil);
                    comando.Parameters.AddWithValue("@mail", empleado.Mail);
                    comando.Parameters.AddWithValue("@id", empleado.EmpleadoId);
                    comando.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }
        }
        public List<Empleado> BuscarEmpleado(string text)
        {
            List<Empleado> lista = new List<Empleado>();
            try
            {
                var cadenaDeComando = "SELECT  EmpleadoId,Nombre,Apellido,TipoDocumentoId,NroDocumento,TelefonoFijo, " +
                    "TelefonoMovil, Mail  FROM Empleados WHERE Nombre LIKE @text";
                var comando = new SqlCommand(cadenaDeComando, conexion);
                comando.Parameters.AddWithValue("@text", $"%{text}%");
                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Empleado empleado = ConstruirEmpleado(reader);
                    lista.Add(empleado);
                }
                reader.Close();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        //public void ActualizarStock(Empleado empleado, decimal cantidad)
        //{
        //    try
        //    {
        //        string cadenaComando = "UPDATE Empleados SET Stock=Stock+@cant WHERE EmpleadoId=@id";
        //        var comando = new SqlCommand(cadenaComando, conexion, transaction);
        //        comando.Parameters.AddWithValue("@cant", cantidad);
        //        comando.Parameters.AddWithValue("@id", empleado.EmpleadoId);
        //        comando.ExecuteNonQuery();
        //    }
        //    catch (Exception e)
        //    {

        //        throw new Exception(e.Message);
        //    }
        //}

        //public Empleado GetEmpleadoPorCodigoDeBarras(string codigo)
        //{
        //    Empleado empleado = null;
        //    try
        //    {
        //        var cadenaDeComando = "SELECT EmpleadoId,Descripcion,TipoEmpleadoId,TipoDocumentoId," +
        //            "PrecioUnitario,Stock,CodigoBarra,MedidaId,Imagen,Suspendido FROM " +
        //            " Empleados WHERE CodigoBarra=@codigo";
        //        var comando = new SqlCommand(cadenaDeComando, conexion);
        //        comando.Parameters.AddWithValue("@codigo", codigo);
        //        var reader = comando.ExecuteReader();
        //        if (reader.HasRows)
        //        {
        //            reader.Read();
        //            empleado = ConstruirEmpleado(reader);
        //            reader.Close();
        //        }
        //        return empleado;
        //    }
        //    catch (Exception e)
        //    {

        //        throw new Exception(e.Message);
        //    }
        //}

        //public List<Empleado> GetLista(int tipoEmpleadoId)
        //{
        //    List<Empleado> lista = new List<Empleado>();
        //    try
        //    {
        //        var cadenaDeComando = "SELECT EmpleadoId,Descripcion,TipoEmpleadoId,TipoDocumentoId,PrecioUnitario,Stock,Suspendido " +
        //            "FROM Empleados WHERE TipoEmpleadoId=@id";
        //        var comando = new SqlCommand(cadenaDeComando, conexion);
        //        comando.Parameters.AddWithValue("@id", tipoEmpleadoId);
        //        var reader = comando.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            Empleado empleado = ConstruirEmpleado(reader);
        //            lista.Add(empleado);
        //        }
        //        reader.Close();
        //        return lista;
        //    }
        //    catch (Exception e)
        //    {

        //        throw new Exception(e.Message);
        //    }
        //}

        //public List<Empleado> GetLista(TipoDocumento tipoDocumento)
        //{
        //    List<Empleado> lista = new List<Empleado>();
        //    try
        //    {
        //        var cadenaDeComando = "SELECT EmpleadoId,Descripcion,TipoEmpleadoId,TipoDocumentoId,PrecioUnitario,Stock,Suspendido " +
        //            "FROM Empleados WHERE TipoDocumentoId=@id";
        //        var comando = new SqlCommand(cadenaDeComando, conexion);
        //        comando.Parameters.AddWithValue("@id", tipoDocumento.TipoDocumentoId);
        //        var reader = comando.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            Empleado empleado = ConstruirEmpleado(reader);
        //            lista.Add(empleado);
        //        }
        //        reader.Close();
        //        return lista;
        //    }
        //    catch (Exception e)
        //    {

        //        throw new Exception(e.Message);
        //    }
        //}

        //public List<Empleado> GetLista(string descripcion)
        //{
        //    List<Empleado> lista = new List<Empleado>();
        //    try
        //    {
        //        var cadenaDeComando = "SELECT EmpleadoId,Descripcion,TipoEmpleadoId,TipoDocumentoId,PrecioUnitario,Stock,Suspendido " +
        //            "FROM Empleados WHERE Descripcion LIKE @descripcion";
        //        var comando = new SqlCommand(cadenaDeComando, conexion);
        //        comando.Parameters.AddWithValue("@descripcion", $"%{descripcion}%");
        //        var reader = comando.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            Empleado empleado = ConstruirEmpleado(reader);
        //            lista.Add(empleado);
        //        }
        //        reader.Close();
        //        return lista;
        //    }
        //    catch (Exception e)
        //    {

        //        throw new Exception(e.Message);
        //    }

    }
}
