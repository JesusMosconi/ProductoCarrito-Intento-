using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;
using Entidades;

namespace Negocios
{

    public class NegocioCarrito
    {
        DatosCarrito objDatosCarrito = new DatosCarrito();


        public int abmCarrito(string accion, Carrito objCarrito)
        {
            return objDatosCarrito.abmCarrito(accion, objCarrito);
        }

        public DataSet CargarCarrito()
        {
            return objDatosCarrito.CargarCarrito();
        }

        public int VaciarCarrito() 
        {
           return objDatosCarrito.VaciarCarrito();
        }
    }
}
