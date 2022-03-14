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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppEjemplo_Delegados
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void MensajeDelegado(string mensaje);
        MensajeDelegado msjDelegado;
        public MainWindow()
        {
          
            InitializeComponent();
            msjDelegado += ImprimirEnConsola;
            msjDelegado += MostrarMessageBox;
        }

  
        private void btnDelegados_Click(object sendr, RoutedEventArgs e)
        {
            msjDelegado(txtNombre.Text);
        }

        void ImprimirEnConsola(string mensaje)
        {
            Console.WriteLine(mensaje + "\n");
        }
        void MostrarMessageBox(string mensaje)
        {
            MessageBox.Show("Hola " + mensaje);
        }

        private void btnSaludar_Click(object sender, RoutedEventArgs e)
        {
            MostrarMessageBox(txtNombre.Text);
            this.Title = "Saludando a " + txtNombre.Text;

            Button btnSource = e.Source as Button;
            btnSource.Width = 50;
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                MostrarMessageBox("Usted presionó la tecla Up " + txtNombre.Text);
            }
        }
    }
}
