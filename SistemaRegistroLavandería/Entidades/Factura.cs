using System;
using System.Collections.Generic;
using System.Text;

namespace Directorio_Entidades
{
    public class Facturas
    {
        public int idPedido { get; set; }
        public float subtotal { get; set; }
        public float descuento { get; set; }
        public float iva { get; set; }
        public float total { get; set; }
    }
}

