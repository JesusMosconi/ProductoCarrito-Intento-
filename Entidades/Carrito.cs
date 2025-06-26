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
        private string NombreProducto;
        private int PrecioProducto;
        private int CantidadProducto;
        private int TotalProducto;

        #endregion


        #region Constructores
        public Carrito() 
        { 
            NombreProducto = string.Empty;
            PrecioProducto = 0;
            CantidadProducto = 0;
            TotalProducto = 0;
        }
        #endregion

        #region Propiedades
        public string Nombre
        {
            get { return NombreProducto; }
            set { NombreProducto = value; }
        }

        public int Precio
        {
            get { return PrecioProducto; }
            set { PrecioProducto = value; }
        }
        public int Cantidad
        {
            get { return CantidadProducto; }
            set { CantidadProducto = value; }
        }
        public int Total
        {
            get { return TotalProducto; }
            set { TotalProducto = value; }
        }
        #endregion
    }
}
