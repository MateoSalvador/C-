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
    /// Lógica de interacción para RECLAMOS.xaml
    /// </summary>
    public partial class RECLAMOS : Window
    {
        Directorio directorio = new Directorio();
        ManejadorDB manejador = new ManejadorDB();
        public RECLAMOS()
        {
            InitializeComponent();
        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {


            try {

            
            //dpFecha.DisplayDate("");
            txtID.Text = "";
            txtNameCliente.Text = "";
            txtTelefono.Text = "";
            txtMail.Text = "";
            txtFechaPedido.Text = "";

            Pedido pedido = new Pedido();
            DataTable data = new DataTable();

            if (pedido != null && directorio.ValidarCampo(txtNumPedido.Text))
            {
                txtID.Text = manejador.BuscaridCliente(Convert.ToInt32(txtNumPedido.Text));
                if (txtID.Text == "Null")
                {
                    txtID.Text = "";
                    //MessageBox.Show("")
                    MessageBox.Show(" Ingrese un pedido válido.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    txtNameCliente.Text = manejador.BuscarNombreCliente(Convert.ToInt32(txtNumPedido.Text));
                    txtTelefono.Text = manejador.BuscarTelefonoCliente(Convert.ToInt32(txtNumPedido.Text));
                    txtMail.Text = manejador.BuscarEmailCliente(Convert.ToInt32(txtNumPedido.Text));
                    txtFechaPedido.Text = manejador.BuscarFechaPedido(Convert.ToInt32(txtNumPedido.Text));
                    dgvPrendas.DataContext = manejador.MostrarPrendasXPedido(Convert.ToInt32(txtNumPedido.Text)).DefaultView;
                    data = ((DataView)dgvPrendas.ItemsSource).ToTable();
                    if (data.Rows.Count == 0)
                    {

                        txtID.Text = "";
                        txtNameCliente.Text = "";
                        txtTelefono.Text = "";
                        txtMail.Text = "";
                        txtFechaPedido.Text = "";
                        MessageBox.Show(" El pedido ingresado no posee prendas entregadas.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                        txtNumPedido.Text = "";
                    }
                }

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
            catch (Exception)
            {
                MessageBox.Show("Ingrese un valor válido.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
            Menu menu = new Menu();
            menu.Show();
        }

        public void btnGenerarReclamo_Click(object sender, RoutedEventArgs e)
        {
            
            DataTable data = new DataTable();
            if (dpFecha.SelectedDate.Value.ToString("yyyy-MM-dd") != "null")
            {
                if (dgvPrendas.SelectedItem != null)
                {

                    data = ((DataView)dgvPrendas.ItemsSource).ToTable();


                    vtnGenerarReclamo ventana = new vtnGenerarReclamo();
                    ventana.txtServicio.Text = data.Rows[dgvPrendas.SelectedIndex]["nombreservicio"].ToString();
                    ventana.txtPrenda.Text = data.Rows[dgvPrendas.SelectedIndex]["nombreprenda"].ToString();
                    // ventana.txtidDetalle.Text=data.Rows[dgvPrendas.SelectedIndex]
                    ventana.txtFechaReclamovnt.Text = dpFecha.SelectedDate.Value.ToString("yyyy-MM-dd");
                    ventana.txtNumPedido.Text = txtNumPedido.Text;
                    ventana.txtidDetalle.Text = data.Rows[dgvPrendas.SelectedIndex]["idDetalle"].ToString();
                    ventana.Show();
                }
                else
                {
                    MessageBox.Show("Debe Seleccionar una preda para generar un reclamo", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            else
                MessageBox.Show("Por favor seleccione una fecha.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);


        }
        //
        public void dpFecha_CalendarClosed(object sender, RoutedEventArgs e)
        {
            try {
                txtfecha.Text = dpFecha.SelectedDate.Value.ToShortDateString();
            }
            catch (Exception)
            {
                MessageBox.Show("Seleccione una fecha", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
          
        }
    }
}
