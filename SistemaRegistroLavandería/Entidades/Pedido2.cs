using System;
using System.Collections.Generic;
using System.Text;

namespace Directorio_Entidades
{
   public class Pedido2
    {
        public int idPedido { get; set; }
        public int estadoPedido { get; set; }
        public string fecha { get; set; }
        public Cliente2 cliente { get; set; }
        public string fechaEntrega { get; set; }
    }
}
