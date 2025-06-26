using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Datos
{
    public class DatosConexion
    {
        public SqlConnection conexion;
        public string CadenaConexion = @"Server=DESKTOP-24PF9TS\SQLEXPRESS;Database=BdCarrito;Trusted_Connection=True;";



        public DatosConexion()
        {
            conexion = new SqlConnection(CadenaConexion);
        }

        public void AbrirConexion()
        {
            try
            {

                if (conexion.State == ConnectionState.Closed || conexion.State == ConnectionState.Broken)
                {
                    conexion.Open();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al abrir la conexion", e);
            }
        }

        public void CerrarConexion()
        {
            try
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al cerrar la conexion", e);
            }
        }
    }


   
}
