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
using Directorio_Logica;
using Directorio_Entidades;

namespace FormularioEntregas
{
    /// <summary>
    /// Lógica de interacción para FrmAdminValores.xaml
    /// </summary>
    public partial class FrmAdminValores : Window
    {
        Directorio directorio = new Directorio();
        public FrmAdminValores()
        {
            InitializeComponent();
            InicializaCmbPrenda();
            InicializaCmbServicio();
        }
        public void InicializaCmbPrenda()
        {
            CmboxPrenda.ItemsSource = directorio.GetPrendas();
            this.CmboxPrenda.DisplayMemberPath = "nombrePrenda";
            this.CmboxPrenda.SelectedValuePath = "idPrenda";
        }

        public void InicializaCmbServicio()
        {
            CmboxServicio.ItemsSource = directorio.GetServicios();
            this.CmboxServicio.DisplayMemberPath = "nombreServicio";
            this.CmboxServicio.SelectedValuePath = "idServicio";
        }

        private void txtPrecio_MouseEnter(object sender, MouseEventArgs e)
        {
            txtPrecio.Text = directorio.MostrarPrecio(CmboxServicio.Text, CmboxPrenda.Text).ToString();
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {

            if (String.IsNullOrEmpty(txtPrecio.Text) || String.IsNullOrEmpty(CmboxServicio.Text) || String.IsNullOrEmpty(CmboxPrenda.Text))
            {
                MessageBox.Show("Campos incompletos", "ADVERTENCIA", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {

                try
                {

                    float precio = float.Parse(txtPrecio.Text);


                        if (precio < 0)
                        {
                            MessageBox.Show("Valor de precio no permitido", "ADVERTENCIA", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                        else
                        {

                            directorio.EditarPrecio(CmboxServicio.Text, CmboxPrenda.Text, precio);
                            MessageBox.Show("El precio del servicio fue actualizado", "Importante", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    
                }
                catch (Exception)
                {
                    MessageBox.Show("Campos Imcompletos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //Menu fmenu = new Menu();
            //fmenu.Show();
        }
    }
}
   
