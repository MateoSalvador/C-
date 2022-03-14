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
using System.Data;

namespace FormularioEntregas
{
    /// <summary>
    /// Lógica de interacción para FrmRegistroPedido.xaml
    /// </summary>
    public partial class FrmRegistroPedido : Window
    {
        Directorio directorio = new Directorio();
        FmrFactura factura = new FmrFactura();
        float subt, desc, iva, total;
        public FrmRegistroPedido()
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

        private void Siguiente_Click(object sender, RoutedEventArgs e)
        {

            Cliente2 cliente = directorio.AgregarCliente(txtCedula.Text, txtNombre.Text, txtApellido.Text, txtDireccion.Text, txtemail.Text, txtTelefono.Text);
            if (cliente == null || DetallesPedido.ItemsSource == null)
            {
                MessageBox.Show("Revise que se ha llenado todo " + directorio.StrRetorno, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                //MessageBox.Show(directorio.StrRetorno);
            }
            else
            {
                try
                {
                    subt = 0;
                    foreach (DataRowView fila in DetallesPedido.ItemsSource)
                    {
                        subt += (float)fila["Precio"];
                    }
                    subt = (float)Math.Round(subt, 2);
                    desc = 0;
                    iva = (subt - desc) * (float)0.12;
                    iva = (float)Math.Round(iva, 2);
                    total = subt + iva;
                    total = (float)Math.Round(total, 2);
                    factura.txbCedula.Text = txtCedula.Text;
                    factura.txbNombre.Text = txtNombre.Text;
                    factura.txbApell.Text = txtApellido.Text;
                    factura.txbDireccion.Text = txtDireccion.Text;
                    factura.txbemail.Text = txtemail.Text;
                    factura.txbTelf.Text = txtTelefono.Text;
                    factura.txbsubt.Text = subt.ToString();
                    factura.txbdesc.Text = desc.ToString();
                    factura.txbiva.Text = iva.ToString();
                    factura.txbTotal.Text = total.ToString();
                    factura.ShowDialog();
                    this.Show();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Revise que se ha llenado todo","ERROR",MessageBoxButton.OK, MessageBoxImage.Error);
                    Console.WriteLine("Error" + ex.Message);

                }
            
            }

      
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            
           try
            {
                if (DetallesPedido.DataContext == null)
                {
                    MessageBox.Show("No hay campos por eliminar", "ADVERTENCIA", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    directorio.EliminarDetalle(DetallesPedido.SelectedIndex);
                    DetallesPedido.DataContext = directorio.DetallarPedido().DefaultView;
                    factura.DetalleFactura.DataContext = directorio.DetallarFactura();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Debe seleccionar un elemento", "ADVERTENCIA", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
           
        }

        

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {

            /*txtCedula.Text = null;
            txtNombre.Text = null;
            txtApellido.Text = null;
            txtDireccion.Text = null;
            txtemail.Text = null;
            txtTelefono.Text = null;
            txtObservacion.Text = null;
            CmboxPrenda.Text = null;
            CmboxServicio.Text = null;
            directorio.LimpiarLista();*/
            //Menu fmenu = new Menu();
            this.Close();
            //fmenu.Show();

        }

        private void txtCedula_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                Cliente2 cl = new Cliente2();
                if (cl != null)
                {
                    txtNombre.Text = directorio.ObtenernombreCliente(txtCedula.Text);
                    txtApellido.Text = directorio.ObtenerapellidoCliente(txtCedula.Text);
                    txtDireccion.Text = directorio.ObtenerdireccionCliente(txtCedula.Text);
                    txtemail.Text = directorio.ObtenerEmailCliente(txtCedula.Text);
                    txtTelefono.Text = directorio.ObtenertelefonoCliente(txtCedula.Text);
                }
                else
                {
                    MessageBox.Show(directorio.StrRetorno, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtNombre.Text = null;
                    txtApellido.Text = null;
                    txtDireccion.Text = null;
                    txtemail.Text = null;
                    txtTelefono.Text = null;
                }
            }
        }

  

        private void btnAñadir_Click(object sender, RoutedEventArgs e)
        {

            if (String.IsNullOrEmpty(txtObservacion.Text) || String.IsNullOrEmpty(CmboxServicio.Text) || String.IsNullOrEmpty(CmboxPrenda.Text))
            {
                MessageBox.Show("Campos incompletos", "ADVERTENCIA", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {

                try
                {

                    Cliente2 cliente = directorio.AgregarCliente(txtCedula.Text, txtNombre.Text, txtApellido.Text, txtDireccion.Text, txtemail.Text, txtTelefono.Text);
                    AdministradorPrecios adpre = directorio.AgregarAdministradorPrecios(CmboxServicio.Text, CmboxPrenda.Text, (directorio.MostrarPrecio(CmboxServicio.Text, CmboxPrenda.Text)));
                    DetallePedido detalle = directorio.AgregarDetallePedido(adpre, txtObservacion.Text);
                    DetallesPedido.DataContext = directorio.DetallarPedido().DefaultView;
                    factura.DetalleFactura.DataContext = directorio.DetallarFactura();
                }
                catch (Exception)
                {
                    MessageBox.Show("Se ha producido un error, revise las entradas", "ADVERTENCIA", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
           


        }

    }
}