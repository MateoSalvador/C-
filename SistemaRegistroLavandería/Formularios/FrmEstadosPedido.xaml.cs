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
using System.Data;
namespace FormularioEntregas
{
    /// <summary>
    /// Lógica de interacción para FrmEstadosPedido.xaml
    /// </summary>
    public partial class FrmEstadosPedido : Window
    {
        Directorio directorio = new Directorio();
        ManejadorDB data = new ManejadorDB();
        public FrmEstadosPedido()
        {
            InitializeComponent();
        }
        private void cmbxEstado_Loaded(object sender, RoutedEventArgs e)
        {

            cmbxEstado.ItemsSource = directorio.MostrarEstados().DefaultView;
            cmbxEstado.DisplayMemberPath = "estadoPedido";
            cmbxEstado.SelectedValuePath = "idEstado";
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

            
            dgEstadosPedido.DataContext = data.MostrarPedidoXEstado((cmbxEstado.SelectedIndex) + 1).DefaultView;

            if(dgEstadosPedido.DataContext == null)
            {
                MessageBox.Show("Primero debe Seleccionar un Estado", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                btnDetalle.IsEnabled = IsEnabled = true; 
            }

        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            DataTable datas = new DataTable();
            if (dgEstadosPedido.SelectedItem != null)
            {

                datas = ((DataView)dgEstadosPedido.ItemsSource).ToTable();
                directorio.EditarEstadodePedido((Int32.Parse(datas.Rows[dgEstadosPedido.SelectedIndex]["Numero de Pedido"].ToString())), (cmbxEstado.SelectedIndex) + 1);
                MessageBox.Show("Estado de Pedido Cambiado con exito");
            }
            else
            {
                MessageBox.Show("Debe Seleccionar un pedido para actualizar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

           
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //Menu fmenu = new Menu();
            //fmenu.Show();
        }

        private void btnDetalle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable datas = new DataTable();
                datas = ((DataView)dgEstadosPedido.ItemsSource).ToTable();
                this.Close();
                fmrPedidoDetalle fdetalle = new fmrPedidoDetalle();
                fdetalle.Show();
                fdetalle.txtNumPedido.Text = datas.Rows[dgEstadosPedido.SelectedIndex]["Numero de Pedido"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Debe Seleccionar un pedido para Mostrar Detalles", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
