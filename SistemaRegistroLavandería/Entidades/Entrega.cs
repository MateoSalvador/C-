using System;
using System.Collections.Generic;
using System.Text;

namespace Directorio_Entidades
{
    public class Entrega
    {
        public string IDEntrega { get; set; }
        public string idPedido { get; set; }
        public DateTime @fechaEntregado { get; set; }
        public string Observaciones { get; set; }
    }
}
