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
using System.IO;
using Microsoft.Win32;
using System.ComponentModel;


namespace FormularioEntregas
{
    /// <summary>
    /// Lógica de interacción para frmRevisarReclamo.xaml
    /// </summary>
    public partial class frmRevisarReclamo : Window
    {
        Directorio directorio = new Directorio();
        ManejadorDB manejador = new ManejadorDB();
        Configuracion config = new Configuracion()
        {

        };
        BitmapImage bitmapImage;
        byte[] imagen;
        BitmapDecoder bitCoder;

        public frmRevisarReclamo()
        {
            InitializeComponent();



            //now, the Position of the StreamSource is not in the begin of the stream.

        }
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            frmReclamosAdmin frmReclamosAdmin = new frmReclamosAdmin();
            config.DescripcionReclamo = txtDescripcionDefecto.Text;

            if (String.IsNullOrEmpty(txtDescripcionDefecto.Text))
            {
                MessageBox.Show("ERROR--Reclamo mal generado, no puede existir un reclamo sin descripción del defecto de la prenda", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //Guardar
            RECLAMOS frmReclamos = new RECLAMOS();

            MessageBox.Show("Registro Guardado");
            if (radAprobar.IsChecked == true)
            {
                manejador.EditarEstadoReclamo(Convert.ToInt32(txtidReclamo.Text), "Aprobado");
            }
            else
            {
                if (radRechazar.IsChecked == true)
                {
                    manejador.EditarEstadoReclamo(Convert.ToInt32(txtidReclamo.Text), "Rechazado");
                }
            }
            if (radRechazar.IsChecked == true)
            {
                manejador.EditarEstadoReclamo(Convert.ToInt32(txtidReclamo.Text), "Rechazado");
            }


            this.Close();


            frmReclamosAdmin.Show();
        }


        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

        }

        private void radRechazar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Usted ha rechazado la petición de reclamo", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void radAprobar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Usted ha aprobado la petición de reclamo", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}