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
using System.Data;
using Directorio_Datos;
using Directorio_Entidades;
using Directorio_Logica;
namespace FormularioEntregas
{
    /// <summary>
    /// Lógica de interacción para FrmCotizacion.xaml
    /// </summary>
    public partial class FrmCotizacion : Window
    {
        Directorio directorio = new Directorio();
        Cotizacion cot = new Cotizacion();
        ManejadorDB mane = new ManejadorDB();
        public FrmCotizacion()
        {
            InitializeComponent();
        }
        public void btnAgregar_Click(object sender, RoutedEventArgs e)
        {


            if (String.IsNullOrEmpty(txtNumPrendas.Text) || String.IsNullOrEmpty(cbxServicio.Text) || String.IsNullOrEmpty(cbxPrenda.Text))
            {
                MessageBox.Show("Campos incompletos", "ADVERTENCIA", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                try
                {

                    int cant = Int32.Parse(txtNumPrendas.Text);

                    int pren = cbxPrenda.SelectedIndex + 1;
                    int serv = cbxServicio.SelectedIndex + 1;


                    if (cant < 1)
                    {
                        MessageBox.Show("Valor no permitido", "ADVERTENCIA", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {

                        if (cant < 1)
                        {
                            MessageBox.Show("Valor no permitido", "ADVERTENCIA", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                        else
                        {

                            float preciosp = directorio.MostrarPrecioServicioPrenda(serv, pren);
                        float PrecioTotal = cant * preciosp;
                        Cotizacion cot1 = directorio.AgregarCotizacion(cbxServicio.Text, cbxPrenda.Text, cant, preciosp, PrecioTotal);
                        directorio.AgregarLista(cot1);
                        dgCotizar.DataContext = directorio.MostrarCotizacion().DefaultView;
                         }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Campo no válido, revise la entrada", "ADVERTENCIA", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void dgContizaciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (FrameworkElement)e.Source as DataGrid;
            if (dg.SelectedIndex != -1)
            {
                DataRowView drv = dg.SelectedItem as DataRowView;

                cbxServicio.Text = drv["Servicio"].ToString();
                cbxPrenda.Text = drv["Prenda"].ToString();
                txtNumPrendas.Text = drv["Cantidad"].ToString();

            }
        }

        private void btnNuevaC_Click(object sender, RoutedEventArgs e)
        {
            cbxServicio.SelectedItem = null;
            cbxPrenda.SelectedItem = null;
            dgCotizar.DataContext = null;
            txtNumPrendas.Text = null;
            txtDescuentoC.Text = null;
            directorio.LimpiarLista();
            txtSubtotalC.Text = null;
            txtIVAC.Text = null;
            txtTotalC.Text = null;

        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {

            if (String.IsNullOrEmpty(txtNumPrendas.Text) || String.IsNullOrEmpty(cbxServicio.Text) || String.IsNullOrEmpty(cbxPrenda.Text))
            {
                MessageBox.Show("Campos incompletos", "ADVERTENCIA", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                try
                {

                    int cant = Int32.Parse(txtNumPrendas.Text);

                    int pren = cbxPrenda.SelectedIndex + 1;
                    int serv = cbxServicio.SelectedIndex + 1;


                    if (cant < 1)
                    {
                        MessageBox.Show("Valor no permitido", "ADVERTENCIA", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {

                        float preciosp = directorio.MostrarPrecioServicioPrenda(serv, pren);
                        float PrecioTotal = cant * preciosp;
                        directorio.ActualizarCotizacion(dgCotizar.SelectedIndex, cbxServicio.Text, cbxPrenda.Text, cant, preciosp, PrecioTotal);
                        dgCotizar.DataContext = directorio.MostrarCotizacion().DefaultView;

                        cbxServicio.SelectedItem = null;
                        cbxPrenda.SelectedItem = null;
                        txtNumPrendas.Text = null;
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Campo no válido \n Revise la entrada o asegúrece de haber seleccionado una fila", "ADVERTENCIA", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Console.WriteLine(ex.Message);
                }
            }

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (dgCotizar.DataContext == null)
                {
                    MessageBox.Show("No hay campos por eliminar", "ADVERTENCIA", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    directorio.EliminarCotizacion(dgCotizar.SelectedIndex);
                    dgCotizar.DataContext = directorio.MostrarCotizacion().DefaultView;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Debe seleccionar un elemento", "ADVERTENCIA", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }
        private void btnCotizar_Click(object sender, RoutedEventArgs e)
        {

            if (dgCotizar.DataContext == null)
            {
                MessageBox.Show("Deben existir registros para cotizar", "ADVERTENCIA", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                float subtotal = 0;
                subtotal = (float)Math.Round(subtotal, 2);

                foreach (DataRowView fila in dgCotizar.ItemsSource)
                {
                    subtotal += (float)fila["Total"];
                }
                txtSubtotalC.Text = subtotal.ToString();
                txtDescuentoC.Text = "00,00";
                float IvaCotizacion = (float)(subtotal * 0.12);
                IvaCotizacion = (float)Math.Round(IvaCotizacion, 2);
                txtIVAC.Text = IvaCotizacion.ToString();
                txtTotalC.Text = (subtotal + IvaCotizacion).ToString();
            }

        }

        private void cbxPrenda_Loaded(object sender, RoutedEventArgs e)
        {
            cbxPrenda.ItemsSource = directorio.MostrarPrendas().DefaultView;
            cbxPrenda.DisplayMemberPath = "nombrePrenda";
            cbxPrenda.SelectedValuePath = "idPrenda";
        }

        private void cbxServicio_Loaded(object sender, RoutedEventArgs e)
        {
            cbxServicio.ItemsSource = directorio.MostrarServicios().DefaultView;
            cbxServicio.DisplayMemberPath = "nombreServicio";
            cbxServicio.SelectedValuePath = "idServicio";

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
           // Menu fmenu = new Menu();
           // fmenu.Show();
        }
    }
}
