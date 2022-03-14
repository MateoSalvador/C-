using System;
using System.Collections.Generic;
using System.Text;

namespace Directorio_Entidades
{
    public class Cotizacion
    {
        public string Servicio { get; set; }
        public string Prenda { get; set; }
        public int Cantidad { get; set; }

        public float PrecioPrendaServicio { get; set; }

        public float PrecioTotal { get; set; }
    }
}
