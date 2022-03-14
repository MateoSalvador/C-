using System;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Directorio_Entidades;
using System.Data;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Directorio_Datos;

namespace Directorio_Logica
{
    
    public class Directorio
    {
        ManejadorDB manejador = new ManejadorDB();
        public String StrRetorno = "";

        List<Cotizacion> lstcotizacion = new List<Cotizacion>();

        Cotizacion cotizacion1 = new Cotizacion();


        // FORMULARIO ESTADOS PEDIDO
        public DataTable MostrarEstados()
        {
            return manejador.ListarEstados();
        }
        public void EditarEstadodePedido(int _idPedido, int _idEstado)
        {
            manejador.EditarEstadoPedido(_idPedido, _idEstado);
        }
        public DataTable Idpedido(int par)
        {
            DataTable dtDetallesp = manejador.MostrarPedidoXEstado(par);
            return dtDetallesp;
        }

        // FORMULARIO ENTREGAS
        public int AgregarEntrega(Entrega _entrega)
        {
            Entrega entrega = new Entrega() {idPedido=_entrega.idPedido,fechaEntregado=_entrega.fechaEntregado,Observaciones=_entrega.Observaciones};
            return manejador.AgregarEntrega(_entrega);
        }
        public List<Pedido> MostrarPedidosPorClienteEntrega(String _id)
        {

            return manejador.MostrarPedidosXClienteEntrega(_id);
        }

        public Cliente MostrarDatosClienteEntrega(String _id)
        {
            return manejador.MostrarDatosClienteEntrega(_id);            
        }
       
        public void EditarPedidoEntregado(String _idPedido)
        {
            manejador.EditarPedidoEntregado(_idPedido);
        }

        // FORMULARIO COTIZACION

        public void AgregarLista(Cotizacion cotizacion)
        {
            lstcotizacion.Add(cotizacion);
        }

        public void LimpiarLista()
        {
            lstcotizacion.Clear();
        }

        public void EliminarCotizacion(int indice)
        {
            lstcotizacion.RemoveAt(indice);

        }

        public void ActualizarCotizacion(int indice, String _servicio, String _prenda, int _cantidad, float _preciops, float _PrecioTotal)
        {
            lstcotizacion[indice].Servicio = _servicio;
            lstcotizacion[indice].Prenda = _prenda;
            lstcotizacion[indice].Cantidad = _cantidad;
            lstcotizacion[indice].PrecioPrendaServicio = _preciops;
            lstcotizacion[indice].PrecioTotal = _PrecioTotal;

        }

        public Cotizacion AgregarCotizacion(String _servicio, String _prenda, int _cantidad, float _preciops, float _PrecioTotal)
        {
            Cotizacion cot = new Cotizacion() { Servicio = _servicio, Prenda = _prenda, Cantidad = _cantidad, PrecioPrendaServicio = _preciops, PrecioTotal = _PrecioTotal };
            return cot;
        }

        public DataTable MostrarCotizacion()
        {
            DataTable dtCotizacion = new DataTable();

            dtCotizacion.Columns.Add("Servicio", typeof(String));
            dtCotizacion.Columns.Add("Prenda", typeof(String));
            dtCotizacion.Columns.Add("Cantidad", typeof(int));
            dtCotizacion.Columns.Add("Precio", typeof(float));
            dtCotizacion.Columns.Add("Total", typeof(float));

            foreach (Cotizacion cotiza in lstcotizacion)
            {

                dtCotizacion.Rows.Add(cotiza.Servicio, cotiza.Prenda, cotiza.Cantidad, cotiza.PrecioPrendaServicio, cotiza.PrecioTotal);
            }
            return dtCotizacion;

        }

        public float MostrarPrecioServicioPrenda(int serv, int pren)
        {
            return float.Parse(PrecioServicioPrenda(serv, pren).Rows[0]["Precio"].ToString());

        }

        public DataTable MostrarPrendas()
        {
            return manejador.listarPrendas();
        }

        public DataTable MostrarServicios()
        {
            return manejador.ListarServicios();
        }


        public DataTable PrecioServicioPrenda(int _serv, int _pren)
        {
            return manejador.MostrarPrecioPrendaServicio(_serv, _pren);
        }

        // FORMULARIO ADMINISTRACION DE VALORES

        public DataTable MostrarPrendasAdmin()
        {
            DataTable tablaPrendas = new DataTable();
            tablaPrendas = manejador.CargarPrendasAdmin();
            return tablaPrendas;
        } 
        public ObservableCollection<Prenda> GetPrendas()
        {
            ObservableCollection<Prenda> prendas = new ObservableCollection<Prenda>();

            foreach (DataRow item in MostrarPrendasAdmin().Rows)
            {
                var prenda = new Prenda
                {
                    idPrenda = item["idPrenda"].ToString(),
                    nombrePrenda = item["nombrePrenda"].ToString()
                };

                prendas.Add(prenda);
            }

            return prendas;
        }

        public DataTable MostrarServiciosAdmin()
        {
            DataTable tablaServicios = new DataTable();
            tablaServicios = manejador.CargarServicios();
            return tablaServicios;
        }
        public ObservableCollection<Servicio> GetServicios()
        {
            ObservableCollection<Servicio> servicios = new ObservableCollection<Servicio>();

            foreach (DataRow item in MostrarServiciosAdmin().Rows)
            {
                var servicio = new Servicio
                {
                    idServicio = item["idServicio"].ToString(),
                    nombreServicio = item["nombreServicio"].ToString()
                };

                servicios.Add(servicio);
            }

            return servicios;
        }
        public float MostrarPrecio(string _srv, string _prn)
        {
            return manejador.ObtenerPrecio(_srv, _prn);
        }
        public void EditarPrecio(string _srv, string _prn, float _price)
        {
            manejador.EditarPrecio(_srv, _prn, _price);
        }

        // Formulario Registro y Formulario Facturacion
        List<Cliente2> lstCliente = new List<Cliente2>();
        List<Pedido2> lstPedido = new List<Pedido2>();
        List<Facturas> lstFactura = new List<Facturas>();
        List<DetallePedido> lstDetallePedido = new List<DetallePedido>();
        List<AdministradorPrecios> lstAdPre = new List<AdministradorPrecios>();

        public Cliente2 AgregarCliente(string _identificacion, string _nombre, string _apellido, string _direccion, string _email, string _telefono)
        {
            Cliente2 cliente = new Cliente2() { identificacion = _identificacion, nombreCliente = _nombre, apellidoCliente = _apellido, direccion = _direccion, email = _email, telefono = _telefono };
            if (ValidarCliente(cliente))
            {
                lstCliente.Add(cliente);
                return cliente;
            }
            else
            {
                return null;
            }

        }

        public Cliente2 AgregarClienteFinal(string _identificacion, string _nombre, string _apellido, string _direccion, string _email, string _telefono)
        {
            Cliente2 cliente = new Cliente2() { identificacion = _identificacion, nombreCliente = _nombre, apellidoCliente = _apellido, direccion = _direccion, email = _email, telefono = _telefono };
            if (ValidarCliente(cliente))
            {
                lstCliente.Add(cliente);
                manejador.CrearCliente(cliente);
                return cliente;
            }
            else
            {
                return null;
            }
        }

        public Pedido2 AgregarPedido(int _idpedido, int _estadoP, string _fecha, Cliente2 _cliente, string _fechaE)
        {
            Pedido2 pedido = new Pedido2() { idPedido = _idpedido, estadoPedido = _estadoP, fecha = _fecha, cliente = _cliente, fechaEntrega = _fechaE };
            lstPedido.Add(pedido);

            return pedido;
        }

        public Facturas AgregarFactura(int id, float _subtotal, float _descuento, float _iva, float _total)
        {
            Facturas factura = new Facturas() { idPedido = id, subtotal = _subtotal, descuento = _descuento, iva = _iva, total = _total };
            lstFactura.Add(factura);
            manejador.CrearFactura(_subtotal, _descuento, _iva, _total);
            return factura;
        }

        public DetallePedido AgregarDetallePedido(AdministradorPrecios _AP, string _descripcion)
        {
            DetallePedido detpedido = new DetallePedido() { servicioPrenda = _AP, descripcion = _descripcion };
            lstDetallePedido.Add(detpedido);
            return detpedido;
        }

        public void GuardarDetalle(int idp, int idf, string _serv, string _prend, string _descripc)
        {
            manejador.CrearDetallePedido(idp, idf, _serv, _prend, _descripc);
        }


        public AdministradorPrecios AgregarAdministradorPrecios(string _servicio, string _prenda, float _precio)
        {
            AdministradorPrecios adpre = new AdministradorPrecios() { servicio = _servicio, prenda = _prenda, precio = _precio };
            lstAdPre.Add(adpre);
            return adpre;
        }



        private bool ValidarCliente(Cliente2 _cliente)
        {
            StrRetorno = "";
            if (string.IsNullOrEmpty(_cliente.identificacion))
                StrRetorno += " El campo Cédula/Ruc es Obligatorio. ";
            if (string.IsNullOrEmpty(_cliente.nombreCliente))
                StrRetorno += " El campo Nombre es Obligatorio. ";
            if (string.IsNullOrEmpty(_cliente.apellidoCliente))
                StrRetorno += " El campo Apellido es Obligatorio. ";
            if (string.IsNullOrEmpty(_cliente.direccion))
                StrRetorno += " El campo Direccion es Obligatorio. ";
            if (string.IsNullOrEmpty(_cliente.email))
                StrRetorno += " El campo E-mail es Obligatorio. ";
            if (string.IsNullOrEmpty(_cliente.telefono))
                StrRetorno += " El campo Teléfono es Obligatorio. ";

            return StrRetorno.Length == 0;
        }


        public int MostrarUltimoPedido()
        {
            return manejador.BuscarUltimoPedido();
        }

        public int ObtenerIdCliente(string _id)
        {
            return manejador.ObtenerIdCliente(_id);
        }

        public string ObtenerEmailCliente(string _id)
        {
            return manejador.BuscarEmailCliente(_id);
        }

        public string ObtenernombreCliente(string _id)
        {
            return manejador.BuscarNombreCliente(_id);
        }

        public string ObtenerapellidoCliente(string _id)
        {
            return manejador.BuscarApellidoCliente(_id);
        }

        public string ObtenertelefonoCliente(string _id)
        {
            return manejador.BuscarTlfCliente(_id);
        }

        public string ObtenerdireccionCliente(string _id)
        {
            return manejador.BuscardireccCliente(_id);
        }

        public int ObtenerIdServicioPrenda(string _srv, string _prn)
        {
            return manejador.ObtenerIdServicioPrenda(_srv, _prn);
        }

        public DataTable DetallarFactura()
        {

            DataTable dtDetallesf = new DataTable();
            dtDetallesf.Columns.Add("Servicio", typeof(string));
            dtDetallesf.Columns.Add("Prenda", typeof(string));
            dtDetallesf.Columns.Add("Observación", typeof(string));
            dtDetallesf.Columns.Add("Precio", typeof(float));
            foreach (DetallePedido detalle in lstDetallePedido)
            {
                dtDetallesf.Rows.Add(detalle.servicioPrenda.servicio, detalle.servicioPrenda.prenda, detalle.descripcion, manejador.ObtenerPrecio(detalle.servicioPrenda.servicio, detalle.servicioPrenda.prenda));
            }
            //dtDetallesf.Rows.Add(_detallePedido.servicioPrenda.servicio, _detallePedido.servicioPrenda.prenda, _detallePedido.descripcion, data.ObtenerPrecio(_detallePedido.servicioPrenda.servicio, _detallePedido.servicioPrenda.prenda));
            return dtDetallesf;


        }

        public DataTable DetallarPedido()
        {

            DataTable dtDetallesp = new DataTable();
            dtDetallesp.Columns.Add("Servicio", typeof(string));
            dtDetallesp.Columns.Add("Prenda", typeof(string));
            dtDetallesp.Columns.Add("Observación", typeof(string));
            dtDetallesp.Columns.Add("Precio", typeof(float));
            foreach (DetallePedido detalle in lstDetallePedido)
            {
                dtDetallesp.Rows.Add(detalle.servicioPrenda.servicio, detalle.servicioPrenda.prenda, detalle.descripcion, manejador.ObtenerPrecio(detalle.servicioPrenda.servicio, detalle.servicioPrenda.prenda));
            }
            //dtDetallesp.Rows.Add(_detallePedido.servicioPrenda.servicio, _detallePedido.servicioPrenda.prenda, _detallePedido.descripcion, data.ObtenerPrecio(_detallePedido.servicioPrenda.servicio, _detallePedido.servicioPrenda.prenda));

            return dtDetallesp;

        }

        public void EliminarDetalle(int indice)
        {
            lstDetallePedido.RemoveAt(indice);

        }

        //PARTE DE RECLAMOS
        public bool ValidarCampo(string _mensaje)
        {
            if (String.IsNullOrEmpty(_mensaje))
                StrRetorno += " El campo es obligatorio.";
            return !(_mensaje.Length == 0);
        }
        //metodo para guardar las prendas con reclamo
        public int GuardarPrendaReclamoDir(PrendaReclamo prendaReclamo)
        {
            return manejador.GuardarPrendaReclamo(prendaReclamo);
        }
    }
}
