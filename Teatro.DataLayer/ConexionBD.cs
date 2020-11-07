using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teatro.DataLayer
{
    public class ConexionBD
    {
        private readonly SqlConnection connection;
        public ConexionBD()
        {
            var cadenaDeConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
            connection = new SqlConnection(cadenaDeConexion);
        }

        public SqlConnection AbrirConexion()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            return connection;
        }

        public void CerrarConexion()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
