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

namespace FormularioEntregas
{
    /// <summary>
    /// Lógica de interacción para frmReclamosAdmin.xaml
    /// </summary>
    public partial class frmReclamosAdmin : Window
    {
        DataTable data = new DataTable();
        Directorio directorio = new Directorio();
        ManejadorDB manejador = new ManejadorDB();
        public frmReclamosAdmin()
        {
            InitializeComponent();
            dgReclamosPendientes.DataContext = manejador.MostrarReclamosPendientes().DefaultView;


        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Menu menu = new Menu();
            menu.Show();
        }


        private void btnDetalles_Click(object sender, RoutedEventArgs e)
        {
            DataTable data = new DataTable();

            if (dgReclamosPendientes.SelectedItem != null)
            {

                data = ((DataView)dgReclamosPendientes.ItemsSource).ToTable();
                frmRevisarReclamo frmDetalle = new frmRevisarReclamo();
                frmDetalle.txtServicio.Text = data.Rows[dgReclamosPendientes.SelectedIndex]["nombreservicio"].ToString();
                frmDetalle.txtPrenda.Text = data.Rows[dgReclamosPendientes.SelectedIndex]["nombreprenda"].ToString();

                frmDetalle.txtDescripcionDefecto.Text = manejador.DescripcionPrendaReclamo(Convert.ToString(data.Rows[dgReclamosPendientes.SelectedIndex]["idReclamo"].ToString()));
                frmDetalle.txtidReclamo.Text = data.Rows[dgReclamosPendientes.SelectedIndex]["idReclamo"].ToString();
                
                PrendaReclamo prenda = new PrendaReclamo();
                frmReclamosAdmin frmAd = new frmReclamosAdmin();
               
                byte[] arrimg = manejador.RecuperarImagen(Convert.ToInt32(data.Rows[dgReclamosPendientes.SelectedIndex]["idReclamo"].ToString()));
                Image Foto = new Image();
                using (MemoryStream stream = new MemoryStream(arrimg))
                {
                    Foto.Source = BitmapFrame.Create(stream,
                                                      BitmapCreateOptions.None,
                                                      BitmapCacheOption.OnLoad);
                }
                frmDetalle.imgFoto.Source = Foto.Source;
                frmDetalle.Show();
            }
            else
            {
                MessageBox.Show("Debe Seleccionar una preda para generar un reclamo", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}