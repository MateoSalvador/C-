using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Directorio_Entidades;
using Directorio_Logica;
using Directorio_Datos;

namespace FormularioEntregas
{
    /// <summary>
    /// Lógica de interacción para FrmEntregas.xaml
    /// </summary>
    public partial class FrmEntregas : Window
    {
        Directorio directorio = new Directorio();
        Entrega _entrega = new Entrega();
        Cliente _cliente = new Cliente();
        Pedido _pedido = new Pedido();
        ManejadorDB manejador = new ManejadorDB();
        List<Pedido> lstPedido = new List<Pedido>();
        List<Cliente> lstCliente = new List<Cliente>();

        public FrmEntregas()
        {
            InitializeComponent();
        }

        private void txtIdCliente_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                if (!(String.IsNullOrEmpty(txtIdCliente.Text)))
                {
                    List<String> lsprueba = new List<String>();
                    _cliente.idCliente = txtIdCliente.Text;
                    Cliente m_cliente = new Cliente();
                    m_cliente = directorio.MostrarDatosClienteEntrega(txtIdCliente.Text.ToString());
                    txtNombreCLiente.Text = m_cliente.Nombre;
                    txtTelefonoCLiente.Text = m_cliente.telfono;
                    txtDireccionCLiente.Text = m_cliente.direccion;

                    List<Pedido> m_pedido = directorio.MostrarPedidosPorClienteEntrega(txtIdCliente.Text.ToString());
                    cbxPedido.Items.Clear();
                    for (int i = 0; i < m_pedido.Count; i++)
                    {
                        cbxPedido.Items.Add(m_pedido[i].IDpedido);
                    }
                    if (String.IsNullOrEmpty(m_cliente.Nombre))
                    {
                        MessageBox.Show("No se encontro al cliente , revise la entrada", "ADVERTENCIA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else if (m_pedido.Count == 0)
                    {
                        MessageBox.Show("El cliente no tiene pedidos listos para entrega", "ADVERTENCIA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Campo no válido, revise la entrada", "ADVERTENCIA", MessageBoxButton.OK, MessageBoxImage.Warning);
                Console.WriteLine(ex.Message);

            }
        }

        private void BtnFinalizarEntrega_Click(object sender, RoutedEventArgs e)
        {
            Directorio directorio1 = new Directorio();
            if (String.IsNullOrEmpty(txtIdCliente.Text))
            {
                MessageBox.Show("Faltan campos por llenar", "ADVERTENCIA", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                _entrega.idPedido = (cbxPedido.Text).ToString();
                _entrega.fechaEntregado = DateTime.Now;
                _entrega.Observaciones = txtObservacionesCLiente.Text;

                int Resultado = manejador.AgregarEntrega(_entrega);
                if (Resultado > 0)
                {
                    manejador.EditarPedidoEntregado((cbxPedido.Text).ToString());
                    MessageBox.Show("Se guardo la entrega  de manera correcta", "Importante", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    MessageBox.Show("Existio un error al guardar la entrega", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
           // Menu fmenu = new Menu();
           // fmenu.Show();
        }

    }
}
