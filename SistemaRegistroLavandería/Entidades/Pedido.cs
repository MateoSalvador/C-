using System;
using System.Collections.Generic;
using System.Text;

namespace Directorio_Entidades
{
    public class Pedido
    {
        public void AddPedido(int p_IDPedido,Cliente p_Cliente)
        {
            IDpedido = p_IDPedido.ToString();
            cliente = p_Cliente;
        }
        public string IDpedido { get; set; }
            
        public Cliente cliente { get; set; }
        
    }
}
