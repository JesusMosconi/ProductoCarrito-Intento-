using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Carrito
    {
        #region atributos
        private int IdCarrito;
        public Producto Producto { get; set; }
        private int CantidadProducto;


        #endregion


        #region Constructores
        public Carrito() 
        { 
            IdCarrito = 0;
            CantidadProducto = 0;
        }
        #endregion

        #region Propiedades

        public int Id
        {
            get { return IdCarrito; }
            set { IdCarrito = value; }
        }


        public int Cantidad
        {
            get { return CantidadProducto; }
            set { CantidadProducto = value; }
        }
        public decimal Total
        {
            get { return Convert.ToInt32(Producto.Precio) * Cantidad; }
        }
        #endregion
    }
}
