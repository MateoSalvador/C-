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

namespace FormularioEntregas
{
    /// <summary>
    /// Lógica de interacción para vtnGenerarReclamo.xaml
    /// </summary>
    public partial class vtnGenerarReclamo : Window
    {
        Directorio directorio = new Directorio();
        ManejadorDB manejador = new ManejadorDB();
        Configuracion config = new Configuracion()
        {

        };
        byte[] imagen;
        BitmapDecoder bitCoder;
        public vtnGenerarReclamo()
        {
            InitializeComponent();
        }
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            DateTime fechaReclamo = Convert.ToDateTime(txtFechaReclamovnt.Text);
            config.DescripcionReclamo = txtDescripcionDefecto.Text;

            if (String.IsNullOrEmpty(txtDescripcionDefecto.Text))
            {
                MessageBox.Show("ERROR--Debe llenar el campo de descripción del defecto de la prenda");
            }

            //Guardar
            RECLAMOS frmReclamos = new RECLAMOS();
            PrendaReclamo _PrendaReclamo = new PrendaReclamo()
            {
                DescripcionReclamo = txtDescripcionDefecto.Text,
                Foto = imagen
            };
            directorio.GuardarPrendaReclamoDir(_PrendaReclamo);
            MessageBox.Show("Registro Guardado");
            //MessageBox.Show(txtFechaReclamovnt.Text);
            txtDescripcionDefecto.Text = string.Empty;
            manejador.AgregarReclamo(Convert.ToInt32(txtNumPedido.Text), Convert.ToInt32(txtidDetalle.Text), fechaReclamo);

            this.Close();
        }

        private void btnAddArchivo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OD = new OpenFileDialog();
            OD.Filter = "Imagenes jpg(*.jpg)| *.jpg | All Files(*.*) | *.*";
            if (OD.ShowDialog() == true)
            {
                using (Stream stream = OD.OpenFile())
                {
                    bitCoder = BitmapDecoder.Create(stream, BitmapCreateOptions.PreservePixelFormat,
                    BitmapCacheOption.OnLoad);
                    Foto.Source = bitCoder.Frames[0];
                    //txtRutaImagen.Text = OD.FileName;
                }
            }
            else
            {
                Foto.Source = null;
            }
            System.IO.FileStream fs;
            fs = new System.IO.FileStream(OD.FileName, System.IO.FileMode.Open);
            imagen = new byte[Convert.ToInt32(fs.Length.ToString())];
            fs.Read(imagen, 0, imagen.Length);


        }


    }
}
