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

namespace FormularioEntregas
{
    /// <summary>
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btnCotizacion_Click(object sender, RoutedEventArgs e)
        {
            FrmCotizacion fcotizacion = new FrmCotizacion();
          //  this.Hide();
            fcotizacion.Show();
        }

        private void btnEntregas_Click(object sender, RoutedEventArgs e)
        {
            FrmEntregas fentrega = new FrmEntregas();
          //  this.Hide();
            fentrega.Show();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnEstadoPedido_Click(object sender, RoutedEventArgs e)
        {
            FrmEstadosPedido festadospedido = new FrmEstadosPedido();
          //  this.Hide();
            festadospedido.Show();
        }

        private void btnAdministrar_Click(object sender, RoutedEventArgs e)
        {
            FrmAdminValores fadminvalores = new FrmAdminValores();
        //    this.Hide();
            fadminvalores.Show();
        }

        private void btnReclamos_Click(object sender, RoutedEventArgs e)
        {
            btnNewReclamo.Visibility = Visibility.Visible;
            btnConsultarReclamos.Visibility = Visibility.Visible;
            btnReclamosPendientes.Visibility = Visibility.Visible;
            btnAdminReclamosPendientes.Visibility = Visibility.Visible;
        }



        private void btnConsultarReclamos_Click(object sender, RoutedEventArgs e)
        {
            btnNewReclamo.Visibility = Visibility.Visible;
            btnConsultarReclamos.Visibility = Visibility.Visible;
            btnReclamosPendientes.Visibility = Visibility.Visible;
            btnAdminReclamosPendientes.Visibility = Visibility.Visible;
        }

        private void btnReclamos_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void btnNewReclamo_Click(object sender, RoutedEventArgs e)
        {
            RECLAMOS frmNewReclamo = new RECLAMOS();
         //   this.Hide();
            frmNewReclamo.Show();
        }

        private void btnConsultarReclamos_Click_1(object sender, RoutedEventArgs e)
        {
            frmConsultarReclamo frmConsultar = new frmConsultarReclamo();
        //    this.Hide();
            frmConsultar.Show();
        }

        private void btnReclamosPendientes_Click(object sender, RoutedEventArgs e)
        {
            frmReclamosPendientes frmReclamos = new frmReclamosPendientes();
        //    this.Hide();
            frmReclamos.Show();
        }
        //
        private void btnNewReclamo_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btnRegistro_Click(object sender, RoutedEventArgs e)
        {
            FrmRegistroPedido frmReclamos = new FrmRegistroPedido();
        //    this.Hide();
            frmReclamos.Show();
        }

        private void btnFacturas_Click(object sender, RoutedEventArgs e)
        {
            FrmRegistroFacturas frmfacturas = new FrmRegistroFacturas();
        //    this.Hide();
            frmfacturas.Show();
        }

        private void btnAdminReclamosPendientes_Click(object sender, RoutedEventArgs e)
        {
            frmReclamosAdmin frmAdminRec = new frmReclamosAdmin();
        //    this.Hide();
            frmAdminRec.Show();
        }
    }
}
