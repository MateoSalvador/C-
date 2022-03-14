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
    /// Lógica de interacción para frmReclamosPendientes.xaml
    /// </summary>
    public partial class frmReclamosPendientes : Window
    {
        Directorio directorio = new Directorio();
        ManejadorDB manejador = new ManejadorDB();

        public frmReclamosPendientes()
        {


            InitializeComponent();
            dgReclamosPendientes.DataContext = manejador.MostrarReclamosPendientes().DefaultView;

        }
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
           // Menu menu = new Menu();
           // menu.Show();
        }
    }
}
