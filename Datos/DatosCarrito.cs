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

        public DataSet CargarCarrito()
        {
            string orden = @"
                SELECT 
                    c.IdCarrito,
                    p.Nombre,
                    p.Descripcion,
                    p.Precio,
                    c.Cantidad,
                    (p.Precio * c.Cantidad) AS Total
                FROM 
                    Carrito c
                INNER JOIN 
                    Producto p ON c.IdProducto = p.IdProducto";

            SqlCommand cmd = new SqlCommand(orden, conexion);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                AbrirConexion();
                da.Fill(ds);
            }
            catch (Exception e)
            {
                throw new Exception("Error al cargar el carrito", e);
            }
            finally
            {
                CerrarConexion();
                cmd.Dispose();
            }

            return ds;
        }

        public int abmCarrito(string accion, Carrito objCarrito) 
        {
           int resultado = -1;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;
            try
            {
                AbrirConexion();
                if (accion == "Alta")
                {
                    cmd.CommandText = @"
                        SELECT Cantidad
                        FROM Carrito 
                        WHERE IdProducto = @IdProducto";

                    cmd.Parameters.AddWithValue("@IdProducto", objCarrito.Id);

                    object cantidadExistente = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();

                    if (cantidadExistente != null)
                    {
                        // Si existe, actualizar cantidad acumulada
                        cmd.CommandText = @"
                            UPDATE Carrito
                            SET Cantidad = Cantidad + @Cantidad
                            WHERE IdProducto = @IdProducto";
                    }
                    else
                    {
                        // Si no existe, insertar nuevo registro
                        cmd.CommandText = @"
                            INSERT INTO Carrito (IdProducto, Cantidad)
                            VALUES (@IdProducto, @Cantidad)";
                    }

                    cmd.Parameters.AddWithValue("@IdProducto", objCarrito.Id);
                    cmd.Parameters.AddWithValue("@Cantidad", objCarrito.Cantidad);

                    resultado = cmd.ExecuteNonQuery();
                }
                else if (accion == "Baja")
                {
                    cmd.CommandText = "DELETE FROM Carrito WHERE IdCarrito = @IdCarrito";
                    cmd.Parameters.AddWithValue("@IdCarrito", objCarrito.Id);
                    resultado = cmd.ExecuteNonQuery();
                }
            
                
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


        public int VaciarCarrito()         
        {
            int resultado = -1;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;

            try
            {
                AbrirConexion();
                cmd.CommandText = "Delete from Carrito";
                resultado = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw new Exception("ERROR", e);
            }
            finally 
            {
                CerrarConexion();
                cmd.Dispose();
            
            }

            return resultado;
        }

    }
}
