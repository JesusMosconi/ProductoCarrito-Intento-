using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Entidades;

namespace Datos
{
    public class DatosCarrito : DatosConexion
    {
        public int abmCarrito(string accion, Carrito objCarrito) 
        {
            int resultado = -1;
            string orden = string.Empty;

            if (accion == "Alta")
            {
                orden = "insert into Carrito values ('"  + objCarrito.Nombre +
"'," + objCarrito.Precio + "," + objCarrito.Cantidad  + "," + objCarrito.Total + ");";
            }

            if (accion == "Modificar") 
            {
                orden = "update Carrito set CantidadProducto = '" + objCarrito.Cantidad +
                    ", TotalProducto = " + objCarrito.Total +
                    " where NombreProducto = '" + objCarrito.Nombre + "';";
            }

            if (accion == "Baja")
            {
                orden = "delete from Carrito where NombreProducto = '" + objCarrito.Nombre + "';";
            }

            SqlCommand cmd = new SqlCommand(orden, conexion);

            try
            {
                AbrirConexion();
                resultado = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Error al realizar la operación en el carrito", e);
            }
            finally
            {
                CerrarConexion();
                cmd.Dispose();
            }
            return resultado;
        }


        public DataSet ListadoCarrito(string cual) 
        { 
            string orden = string.Empty;
            if (cual != "Todos")
            {
                orden = "select * from Carrito where NombreProducto = '" + cual + "';";
            }
            else
            {
                orden = "select * from Carrito;";
            }
            SqlCommand cmd = new SqlCommand(orden, conexion);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();

            try
            {
                AbrirConexion();
                cmd.ExecuteNonQuery();

                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {

                throw new Exception("Error al Listar Carrito", e);
            }
            finally
            {
                CerrarConexion();
                cmd.Dispose();
            }
            return ds;
        }
    }
}
