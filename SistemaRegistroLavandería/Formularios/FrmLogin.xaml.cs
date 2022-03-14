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
using Directorio_Datos;
namespace FormularioEntregas
{
    /// <summary>
    /// Lógica de interacción para FrmLogin.xaml
    /// </summary>
    public partial class FrmLogin : Window
    {
        ManejadorDB manejador = new ManejadorDB();
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            if (manejador.validarUsuario(txtUsuario.Text.ToString(), txtPassword.Password) && txtUsuario.Text=="admin")
            {
                Menu fmenu = new Menu();
                this.Close();
                fmenu.Show();

            }

            else if (manejador.validarUsuario(txtUsuario.Text.ToString(), txtPassword.Password) && txtUsuario.Text == "cajero")
            {
                Menu fmenu = new Menu();
                this.Close();
                fmenu.btnAdministrar.IsEnabled = IsEnabled = false;
                fmenu.btnFacturas.IsEnabled = IsEnabled = false;
                fmenu.btnAdminReclamosPendientes.IsEnabled = IsEnabled = false;
                fmenu.Show();

            }
            
            else if (manejador.validarUsuario(txtUsuario.Text.ToString(), txtPassword.Password) && txtUsuario.Text == "servicios")
            {
                Menu fmenu = new Menu();
                this.Close();
                fmenu.btnAdministrar.IsEnabled = IsEnabled = false;
                fmenu.btnConsultarReclamos.IsEnabled = IsEnabled = false;
                fmenu.btnReclamos.IsEnabled = IsEnabled = false;
                fmenu.btnRegistro.IsEnabled = IsEnabled = false;
                fmenu.btnCotizacion.IsEnabled = IsEnabled = false;
                fmenu.btnEntregas.IsEnabled = IsEnabled = false;
                fmenu.btnFacturas.IsEnabled = IsEnabled = false;
                fmenu.btnAdminReclamosPendientes.IsEnabled = IsEnabled = false;
                fmenu.Show();

            }
            else
            {
                MessageBox.Show("Usuario o Contraseña no valida", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
            private void btnSalir_Click(object sender, RoutedEventArgs e)
            {
                App.Current.Shutdown();
            }
        }

    
}
