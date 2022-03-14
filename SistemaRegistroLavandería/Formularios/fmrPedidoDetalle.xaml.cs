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
    /// Lógica de interacción para fmrPedidoDetalle.xaml
    /// </summary>
    public partial class fmrPedidoDetalle : Window
    {
        Directorio directorio = new Directorio();
        ManejadorDB manejador = new ManejadorDB();
        public fmrPedidoDetalle()
        {
            InitializeComponent();
        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            

            Pedido pedido = new Pedido();

            if (pedido != null && directorio.ValidarCampo(txtNumPedido.Text))
            {
                txtID.Text = manejador.BuscaridCliente(Convert.ToInt32(txtNumPedido.Text));
                txtNameCliente.Text = manejador.BuscarNombreCliente(Convert.ToInt32(txtNumPedido.Text));
                txtTelefono.Text = manejador.BuscarTelefonoCliente(Convert.ToInt32(txtNumPedido.Text));
                txtMail.Text = manejador.BuscarEmailCliente(Convert.ToInt32(txtNumPedido.Text));
                txtFechaPedido.Text = manejador.BuscarFechaPedido(Convert.ToInt32(txtNumPedido.Text));
                dgvPrendas.DataContext = manejador.MostrarPrendasyPedido(Convert.ToInt32(txtNumPedido.Text)).DefaultView;
            }
            else
            {
                MessageBox.Show(directorio.StrRetorno, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                txtID.Text = "";
                txtNameCliente.Text = "";
                txtTelefono.Text = "";
                txtMail.Text = "";
                txtFechaPedido.Text = "";
            }

            

        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            FrmEstadosPedido festados = new FrmEstadosPedido();
            festados.Show();
        }
        private void btnObservacion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String Obvservacion = manejador.MostrarObservacion(Convert.ToInt32(txtNumPedido.Text));

                if (Obvservacion != "Null")
                {
                    MessageBox.Show(Obvservacion, "OBSERVACION", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No se registro una Observacion", "OBSERVACION", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                {

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Debe ingresar un pedido valido");
            }

        }

        //
    }
}
   
    