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
    /// Lógica de interacción para frmConsultarReclamo.xaml
    /// </summary>
    public partial class frmConsultarReclamo : Window
    {
        Directorio directorio = new Directorio();
        ManejadorDB manejador = new ManejadorDB();
        public frmConsultarReclamo()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Pedido pedido = new Pedido();
            DataTable data = new DataTable();


            if (pedido != null && directorio.ValidarCampo(txtNumReclamo.Text))
            {
                txtID.Text = manejador.BuscaridClienteR(Convert.ToInt32(txtNumReclamo.Text));
                if (txtID.Text == "Null")
                {
                    txtID.Text = "";
                    //MessageBox.Show("")
                    MessageBox.Show(" Ingrese un número de Reclamo válido.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    txtNameCliente.Text = manejador.BuscarNombreClienteR(Convert.ToInt32(txtNumReclamo.Text));
                    txtTelefono.Text = manejador.BuscarTelefonoClienteR(Convert.ToInt32(txtNumReclamo.Text));
                    txtMail.Text = manejador.BuscarEmailClienteR(Convert.ToInt32(txtNumReclamo.Text));
                    txtFechaReclamo.Text = manejador.BuscarFechaR(Convert.ToInt32(txtNumReclamo.Text));
                    dgPrendas.DataContext = manejador.MostrarReclamosXid(Convert.ToInt32(txtNumReclamo.Text)).DefaultView;
                    data = ((DataView)dgPrendas.ItemsSource).ToTable();
                    if (data.Rows.Count == 0)
                    {
                        txtID.Text = "";
                        txtNameCliente.Text = "";
                        txtTelefono.Text = "";
                        txtMail.Text = "";
                        txtFechaReclamo.Text = "";
                        MessageBox.Show(" Ingrese un número de Reclamo válido.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
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
                txtFechaReclamo.Text = "";
            }
        }
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Menu menu = new Menu();
            menu.Show();
        }
    }
}