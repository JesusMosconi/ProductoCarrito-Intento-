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
    public class DatosProducto:DatosConexion
    {
        public int abmProducto(string accion, Producto objProducto)
        {
            int resultado = -1;
            string orden = string.Empty;

            if (accion == "Alta")
            {
                orden = "insert into Producto values (" + objProducto.Id + ",'" + objProducto.Nombre +
                    "','" + objProducto.Descripcion + "'," + objProducto.Precio + "," + objProducto.habilitado + ");";
            }

            if (accion == "Modificar")
                orden = "update Producto set Nombre='" + objProducto.Nombre + "', Descripcion='" + objProducto.Descripcion +
                    "', Precio=" + objProducto.Precio + ", Habilitado='" + objProducto.habilitado +
                    "' where IdProducto = '" + objProducto.Id + "';";
            
            if (accion == "Baja")
            {
                orden = "delete from Producto where IdProducto = '" + objProducto.Id + "';";
            }

            SqlCommand cmd = new SqlCommand(orden, conexion);

            try
            {
                AbrirConexion();
                resultado = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Error al realizar la operación en el producto", e);
            }
            finally
            {
                CerrarConexion();
                cmd.Dispose();
            }
            return resultado;
        }

        public DataSet ListadoProducto(string cual)
        {
            string orden = string.Empty;
            if (cual != "Todos")
            {
                orden = "select * from Producto where NombreProducto = '" + cual + "';";
            }
            else
            {
                orden = "select * from Producto;";
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
                throw new Exception("Error al listar los productos", e);
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
