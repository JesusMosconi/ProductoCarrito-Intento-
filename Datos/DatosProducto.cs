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
        public DataSet CargarProductos() 
        {
            string orden = "SELECT * FROM Producto";
            SqlCommand cmd = new SqlCommand(orden, conexion);
            DataSet ds = new DataSet();
            SqlDataAdapter  da = new SqlDataAdapter(cmd);

            try
            {
                AbrirConexion();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {

                throw new Exception ("Error", e);
            }
            finally 
            { 
            CerrarConexion();
                cmd.Dispose();
            }

            return ds;

        }


        public int abmProducto(string accion, Producto objProducto)
        {
            int resultado = -1;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;


            try
            {
                AbrirConexion();

                if (accion == "Alta")
                {
                    cmd.CommandText = "INSERT INTO Producto (Nombre, Descripcion, Precio, Habilitado) VALUES (@Nombre, @Descripcion, @Precio, @Habilitado)";
                    cmd.Parameters.AddWithValue("@Nombre", objProducto.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", objProducto.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", objProducto.Precio);
                    cmd.Parameters.AddWithValue("@Habilitado", objProducto.habilitado);
                    resultado = cmd.ExecuteNonQuery();
                }
                else if (accion == "Modificar")
                {
                    cmd.CommandText = "UPDATE Producto SET Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio, Habilitado = @Habilitado WHERE IdProducto = @IdProducto";
                    cmd.Parameters.AddWithValue("@Nombre", objProducto.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", objProducto.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", objProducto.Precio);
                    cmd.Parameters.AddWithValue("@Habilitado", objProducto.habilitado);
                    cmd.Parameters.AddWithValue("@IdProducto", objProducto.Id);
                    resultado = cmd.ExecuteNonQuery();
                }
                else if (accion == "Baja")
                {
                    cmd.CommandText = "DELETE FROM Producto WHERE IdProducto = @IdProducto";
                    cmd.Parameters.AddWithValue("@IdProducto", objProducto.Id);
                    resultado = cmd.ExecuteNonQuery();
                }
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

        public DataSet ObtenerProductoPorId(int idProducto)
        {
            string orden = "SELECT * FROM Producto WHERE IdProducto = @IdProducto";
            SqlCommand cmd = new SqlCommand(orden, conexion);
            cmd.Parameters.AddWithValue("@IdProducto", idProducto);

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                AbrirConexion();
                da.Fill(ds);
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener el producto por Id", e);
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
