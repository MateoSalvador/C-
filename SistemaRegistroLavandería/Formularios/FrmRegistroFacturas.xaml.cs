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
    /// Lógica de interacción para FrmRegistroFacturas.xaml
    /// </summary>
    public partial class FrmRegistroFacturas : Window
    {
        public FrmRegistroFacturas()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Directorio directorio = new Directorio();
            ManejadorDB manejador = new ManejadorDB();

            Facturas Factura = new Facturas();

            try { 
            

                if (String.IsNullOrEmpty(txtFecha.Text) )
                {
                    MessageBox.Show("Recuerde ingresar una fecha. ", "ADVERTENCIA", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else 
                {
                    if (Factura != null)
                    {
                        dgvFacturas.DataContext = manejador.MostrarFacturas(txtFecha.SelectedDate.Value.ToShortDateString()).DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("Noy facturas desde esa fecha o ingrese la fecha en el formato correcto (dd/MM/aaaa)", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }

            
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //Menu menu = new Menu();
            //menu.Show();
        }
    }

    
}
