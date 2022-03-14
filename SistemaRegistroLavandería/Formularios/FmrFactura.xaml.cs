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
using Directorio_Datos;
using System.Data;

namespace FormularioEntregas
{
    /// <summary>
    /// Lógica de interacción para FmrFactura.xaml
    /// </summary>
    public partial class FmrFactura : Window
    {
        Directorio directorio = new Directorio();
        ManejadorDB manejador = new ManejadorDB();
        int nf;
        public FmrFactura()
        {
            InitializeComponent();
            nf = directorio.MostrarUltimoPedido() + 1;
            Cliente2 cliente1 = directorio.AgregarCliente(txbCedula.Text, txbNombre.Text, txbApell.Text, txbDireccion.Text, txbemail.Text, txbTelf.Text);
            //Factura factura = directorio.AgregarFactura(1, float.Parse(txbsubt.Text), float.Parse(txbdesc.Text), float.Parse(txbiva.Text), float.Parse(txbTotal.Text));
            Pedido2 pedido = directorio.AgregarPedido(nf, 1, DateTime.Now.ToShortDateString(), cliente1, DateTime.Today.AddDays(3).ToShortDateString());
            txbFacturaN.Text = pedido.idPedido.ToString();
            txbFecha.Text = pedido.fecha.ToString();
            txbFechaentrega.Text = pedido.fechaEntrega.ToString();


        }

        public class Detalle
        {
            public string Servicio { get; set; }
            public string Prenda { get; set; }
            public string Observación { get; set; }
            public float Precio { get; set; }
        }

        Detalle DP = new Detalle();

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            float subto, descu, iva, total;
            subto = float.Parse(txbsubt.Text);
            descu = 0;
            iva = (subto - descu) * (float)0.12;
            total = subto - descu + iva;

            if (directorio.ObtenernombreCliente(txbCedula.Text) != "")
            {
                Facturas factura = directorio.AgregarFactura(nf, subto, 0, iva, total);
                Pedido2 pedido1 = new Pedido2();
                Cliente2 cliente = directorio.AgregarCliente(txbCedula.Text, txbNombre.Text, txbApell.Text, txbDireccion.Text, txbemail.Text, txbTelf.Text);
                //Pedido pedido = directorio.AgregarPedido(nf, 1, DateTime.Now, cliente, DateTime.Today.AddDays(3).Date);
                pedido1.estadoPedido = 1;
                pedido1.cliente = cliente;
                pedido1.fechaEntrega = DateTime.Today.AddDays(3).ToShortDateString();
                pedido1.fecha = DateTime.Today.ToShortDateString();
                manejador.CrearPedido(pedido1);


                foreach (DataRowView fila in DetalleFactura.ItemsSource)
                {
                    directorio.GuardarDetalle(nf, nf, fila["Servicio"].ToString(), fila["Prenda"].ToString(), fila["Observación"].ToString());
                }

            }

            else
            {
                Cliente2 clientef = directorio.AgregarClienteFinal(txbCedula.Text, txbNombre.Text, txbApell.Text, txbDireccion.Text, txbemail.Text, txbTelf.Text);
                Facturas factura = directorio.AgregarFactura(nf, subto, 0, iva, total);
                Pedido2 pedido2 = new Pedido2();
                pedido2.estadoPedido = 1;
                pedido2.cliente = clientef;
                pedido2.fechaEntrega = DateTime.Today.AddDays(3).ToShortDateString();
                pedido2.fecha = DateTime.Today.ToShortDateString();
                manejador.CrearPedido(pedido2);
                //Pedido pedido = directorio.AgregarPedido(nf, 1, DateTime.Now.Date, cliente, DateTime.Today.AddDays(3).Date);

                foreach (DataRowView fila in DetalleFactura.ItemsSource)
                {
                    directorio.GuardarDetalle(nf, nf, fila["Servicio"].ToString(), fila["Prenda"].ToString(), fila["Observación"].ToString());
                }
            }



            MessageBox.Show("Registro Guardado");

            PrintDialog dialog = new PrintDialog();

            MessageBoxResult respuesta = MessageBox.Show("Desea imprimir", "Impresión", MessageBoxButton.YesNoCancel);

            if (respuesta == MessageBoxResult.Yes)
            {
                btnCancelar.Height = 0;
                btnCancelar.Width = 0;
                btnGuardar.Height = 0;
                btnGuardar.Width = 0;
                // Imprimir la pantalla

                if (dialog.ShowDialog() == true)

                {

                    dialog.PrintVisual(this, "Impresión");

                }

            }

            //Menu fmenu = new Menu();
            this.Close();
            //fmenu.Show();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}