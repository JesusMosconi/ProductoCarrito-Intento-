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
    public class NegocioProducto
    {
        DatosProducto datosProducto = new DatosProducto();
        public int abmProducto(string accion, Producto objProducto)
        {
            return datosProducto.abmProducto(accion, objProducto);
        }
        public DataSet ListadoProducto(string cual)
        {
            return datosProducto.ListadoProducto(cual);

        }

    }
}


