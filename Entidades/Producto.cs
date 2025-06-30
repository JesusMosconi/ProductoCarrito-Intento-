using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Producto
    {
        #region atributos
        private int IdProducto;
        private string NombreProducto;
        private string DescripcionProducto;
        private string PrecioProducto;
        private bool Habilitado;

        #endregion


        #region Constructores
        public Producto()
        {
            IdProducto = 0;
            NombreProducto = string.Empty;
            DescripcionProducto = string.Empty;
            PrecioProducto = string.Empty;
            Habilitado = false;
        }
        #endregion

        #region Propiedades
        public int Id
        {
            get { return IdProducto; }
            set { IdProducto = value; }
        }
        public string Nombre
        {
            get { return NombreProducto; }
            set { NombreProducto = value; }
        }

        public string Descripcion
        {
            get { return DescripcionProducto; }
            set { DescripcionProducto = value; }
        }
        public string Precio
        {
            get { return PrecioProducto; }
            set { PrecioProducto = value; }
        }

        public bool habilitado
            {
            get { return Habilitado; }
            set { Habilitado = value; }
        }

        #endregion
    }
}
