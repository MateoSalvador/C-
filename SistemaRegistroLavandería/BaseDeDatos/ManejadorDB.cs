using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Directorio_Entidades;
using System.Data.SqlClient;

namespace Directorio_Datos
{

    public class ManejadorDB
    {

        //SqlConnection sqlConexion = new SqlConnection("Data Source=DESKTOP-PAKO38A;Initial Catalog=dbLavanderiaHigiensec;Integrated Security=True;MultipleActiveResultSets=True");
        SqlConnection sqlConexion = new SqlConnection("Data Source=DESKTOP-ABNFG15;Initial Catalog=dbLavanderiaHigiensec;Integrated Security=True;MultipleActiveResultSets=True");
        // FORMULARIO ESTADOS PEDIDO

        public DataTable ListarEstados()
        {
            DataTable dtP = new DataTable();

            string query = "select * from tblEstadosPedido ";
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = new SqlCommand(query, sqlConexion);
            AbrirConexion();
            adapter.Fill(dtP);
            CerrarConexion();

            return dtP;
        }
        public DataTable MostrarPedidoXEstado(int idserv)
        {
            DataTable dtPPS = new DataTable();
            SqlCommand sqlCommand = new SqlCommand("spBuscarPedidoPorEstado", sqlConexion);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@idestado", idserv);

            dtPPS.Columns.Add("Numero de Pedido", typeof(int));
            dtPPS.Columns.Add("Estado Pedido", typeof(string));
            dtPPS.Columns.Add("identificacion", typeof(string));
            dtPPS.Columns.Add("Nombre", typeof(string));
            dtPPS.Columns.Add("Apellido", typeof(string));
            dtPPS.Columns.Add("direccion", typeof(string));
            dtPPS.Columns.Add("email", typeof(string));
            dtPPS.Columns.Add("telefono", typeof(string));

            AbrirConexion();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    dtPPS.Rows.Add(reader["Numero de Pedido"], reader["Estado Pedido"], reader["identificacion"], reader["Nombre"], reader["Apellido"], reader["direccion"], reader["email"], reader["telefono"]);
                }
            }
            CerrarConexion();
            return dtPPS;
        }
        public void EditarEstadoPedido(int _idPedido, int _idEstado)
        {
            SqlCommand sqlCommand = new SqlCommand("spEditarEstadoPedido", sqlConexion);
            try
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idPedido", _idPedido);
                sqlCommand.Parameters.AddWithValue("@idestado", _idEstado);
                AbrirConexion();
                sqlCommand.ExecuteNonQuery();
                CerrarConexion();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        // FORMULARIO ENTREGAS

        public void AbrirConexion()
        {
            if (sqlConexion.State == ConnectionState.Closed)
                sqlConexion.Open();
        }
        public void CerrarConexion()
        {
            if (sqlConexion.State == ConnectionState.Open)
                sqlConexion.Close();
        }
        public Cliente MostrarDatosClienteEntrega(String _id)
        {
            Cliente _cliente = new Cliente();
            SqlCommand _command = new SqlCommand("spMostrarClienteXIDEntrega", sqlConexion);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Parameters.AddWithValue("@idC", _id);
            try
            {
                AbrirConexion();
                using (SqlDataReader _reader = _command.ExecuteReader())
                {
                    while (_reader.Read())
                    {
                        _cliente.Nombre = _reader.GetString(0);
                        _cliente.telfono = _reader.GetString(1);
                        _cliente.direccion = _reader.GetString(2);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
            CerrarConexion();
            return _cliente;
        }
        public int AgregarEntrega(Entrega _entrega)
        {
            int Resulta;
            AbrirConexion();
            SqlCommand _command = new SqlCommand("spAgregarEntrega", sqlConexion);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Parameters.AddWithValue("@idPedido", _entrega.idPedido);
            _command.Parameters.AddWithValue("@fechaEntregado", _entrega.fechaEntregado);
            _command.Parameters.AddWithValue("@obse", _entrega.Observaciones);

            Resulta = _command.ExecuteNonQuery();
            CerrarConexion();
            return Resulta;
        }
        public List<Pedido> MostrarPedidosXClienteEntrega(String _id)
        {
            List<Pedido> Lista = new List<Pedido>();
            SqlCommand _command = new SqlCommand("spMostrarPedidosXClienteListos", sqlConexion);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Parameters.AddWithValue("@idC", _id);
            try
            {
                AbrirConexion();
                using (SqlDataReader _reader = _command.ExecuteReader())
                {
                    Pedido m_ped;
                    while (_reader.Read())
                    {
                        m_ped = new Pedido();
                        int m_temp = _reader.GetInt32(0);
                        m_ped.IDpedido = m_temp.ToString();
                        Lista.Add(m_ped);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
            CerrarConexion();
            return Lista;
        }
        public int EditarPedidoEntregado(string _identrega)
        {
            int Resulta;
            AbrirConexion();
            SqlCommand _command = new SqlCommand("spEditarPedidoEntregado", sqlConexion);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Parameters.AddWithValue("@idPedido", _identrega);
            Resulta = _command.ExecuteNonQuery();
            CerrarConexion();
            return Resulta;
        }

        // FORMULARIO COTIZACION 

        public DataTable listarPrendas()
        {
            DataTable dtP = new DataTable();

            string query = "select * from tblPrenda ";
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = new SqlCommand(query, sqlConexion);
            AbrirConexion();
            adapter.Fill(dtP);
            CerrarConexion();

            return dtP;
        }

        public DataTable ListarServicios()
        {
            DataTable dtS = new DataTable();

            string query = "Select * from tblServicio";
            SqlDataAdapter serv = new SqlDataAdapter();
            serv.SelectCommand = new SqlCommand(query, sqlConexion);
            AbrirConexion();
            serv.Fill(dtS);
            CerrarConexion();

            return dtS;
        }

        public DataTable MostrarPrecioPrendaServicio(int idserv, int idpren)
        {
            DataTable dtPPS = new DataTable();
            SqlCommand sqlCommand = new SqlCommand("spServicioPrenda", sqlConexion);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@idservicio", idserv);
            sqlCommand.Parameters.AddWithValue("@idprenda", idpren);

            dtPPS.Columns.Add("Precio", typeof(float));

            AbrirConexion();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    dtPPS.Rows.Add(reader["Precio"]);

                }
            }
            CerrarConexion();
            return dtPPS;
        }

        // FORMULARIO ADMINISTRACION DE VALORES

        public DataTable CargarPrendasAdmin()
        {
            SqlCommand sqlCommand = new SqlCommand("spMostrarPrendas", sqlConexion);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            dt.Columns.Add("idPrenda");
            dt.Columns.Add("nombrePrenda");
            try
            {
                AbrirConexion();
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dt.Rows.Add(reader["idPrenda"], reader["nombrePrenda"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);

            }
            CerrarConexion();
            return dt;
        }
        public DataTable CargarServicios()
        {
            SqlCommand sqlCommand = new SqlCommand("spMostrarServicios", sqlConexion);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("idServicio");
            dt2.Columns.Add("nombreServicio");
            try
            {
                AbrirConexion();
                using (SqlDataReader reader2 = sqlCommand.ExecuteReader())
                {
                    while (reader2.Read())
                    {
                        dt2.Rows.Add(reader2["idServicio"], reader2["nombreServicio"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);

            }
            CerrarConexion();
            return dt2;

        }
        public float ObtenerPrecio(string _srv, string _prn)
        {

            float price = 0;
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConexion;
                sqlCommand.CommandText = "spObtenerPrecio";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@prenda", _prn);
                sqlCommand.Parameters.AddWithValue("@servicio", _srv);

                sqlCommand.Parameters.Add("@precio", SqlDbType.Float);
                sqlCommand.Parameters["@precio"].Direction = ParameterDirection.Output;
                AbrirConexion();
                sqlCommand.ExecuteNonQuery();
                price = float.Parse(sqlCommand.Parameters["@precio"].Value.ToString());
                return price;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                return price;

            }
            finally
            {
                CerrarConexion();
            }

        }

        public void EditarPrecio(string _srv, string _prn, float _price)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConexion;
                sqlCommand.CommandText = "spEditarPrecio";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@prend", _prn);
                sqlCommand.Parameters.AddWithValue("@serv", _srv);
                sqlCommand.Parameters.AddWithValue("@price", _price);

                AbrirConexion();
                sqlCommand.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);


            }
            finally
            {
                CerrarConexion();
            }

        }

        // Formulario Registro y Formulario Facturacion

        public bool CrearCliente(Cliente2 _cliente)
        {
            bool retorno = true;
            string nquery = "INSERT INTO tblCliente(identificacion, nombreCliente, apellidoCliente, direccion, email, telefono) values('" + _cliente.identificacion + "','" + _cliente.nombreCliente + "','" + _cliente.apellidoCliente + "','" + _cliente.direccion + "','" + _cliente.email + "','" + _cliente.telefono + "')";
            Console.WriteLine(nquery);
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.InsertCommand = new SqlCommand(nquery, sqlConexion);
                AbrirConexion();
                adapter.InsertCommand.ExecuteNonQuery();
                CerrarConexion();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                retorno = false;
            }

            return retorno;

        }


        public int ObtenerIdCliente(string _id)
        {

            int id = 0;
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConexion;
                sqlCommand.CommandText = "ObtenerIdCliente";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@identificacion", _id);


                sqlCommand.Parameters.Add("@id", SqlDbType.Int);
                sqlCommand.Parameters["@id"].Direction = ParameterDirection.Output;
                AbrirConexion();
                sqlCommand.ExecuteNonQuery();
                id = int.Parse(sqlCommand.Parameters["@id"].Value.ToString());
                return id;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                return id;

            }
            finally
            {
                CerrarConexion();
            }

        }


        public bool CrearPedido(Pedido2 pedido)
        {

            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConexion;
                sqlCommand.CommandText = "spAgregarPedido";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@idEstado", pedido.estadoPedido);
                sqlCommand.Parameters.AddWithValue("@idCliente", ObtenerIdCliente(pedido.cliente.identificacion));
                sqlCommand.Parameters.AddWithValue("@fechaEntrega", Convert.ToDateTime(pedido.fechaEntrega));
                sqlCommand.Parameters.AddWithValue("@fecha", Convert.ToDateTime(pedido.fecha));


                AbrirConexion();
                sqlCommand.ExecuteNonQuery();

                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                return false;

            }
            finally
            {
                CerrarConexion();
            }

        }

        public bool CrearFactura(float sub, float desc, float iva, float tot)
        {


            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConexion;
                sqlCommand.CommandText = "spAgregarFactura";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@subt", sub);
                sqlCommand.Parameters.AddWithValue("@iva", iva);
                sqlCommand.Parameters.AddWithValue("@desc", desc);
                sqlCommand.Parameters.AddWithValue("@tot", tot);


                AbrirConexion();
                sqlCommand.ExecuteNonQuery();

                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                return false;

            }
            finally
            {
                CerrarConexion();
            }

        }

   

        public bool CrearDetallePedido(int idp, int idf, string _serv, string _prend, string _descripc)
        {
            bool retorno = true;
            string nquery = "INSERT INTO tblDetallePedido(idPedido, idFactura, idServicioPrenda, descripcion) values(" + idp + "," + idf + "," + ObtenerIdServicioPrenda(_serv, _prend) + ",'" + _descripc + "')";
            Console.WriteLine(nquery);
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.InsertCommand = new SqlCommand(nquery, sqlConexion);
                AbrirConexion();
                adapter.InsertCommand.ExecuteNonQuery();
                CerrarConexion();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                retorno = false;
            }

            return retorno;

        }


        public int BuscarUltimoPedido()
        {
            int num = 0;
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConexion;
                sqlCommand.CommandText = "ObtenerUltimoPedido";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add("@idpedido", SqlDbType.Int);
                sqlCommand.Parameters["@idpedido"].Direction = ParameterDirection.Output;
                AbrirConexion();
                sqlCommand.ExecuteNonQuery();
                num = int.Parse(sqlCommand.Parameters["@idpedido"].Value.ToString());
                return num;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                return num;

            }
            finally
            {
                CerrarConexion();
            }
        }

        public int ObtenerIdServicioPrenda(string _srv, string _prn)
        {

            int id = 0;
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConexion;
                sqlCommand.CommandText = "ObtenerIdServicioPrenda";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@prenda", _prn);
                sqlCommand.Parameters.AddWithValue("@servicio", _srv);

                sqlCommand.Parameters.Add("@ID", SqlDbType.Int);
                sqlCommand.Parameters["@ID"].Direction = ParameterDirection.Output;
                AbrirConexion();
                sqlCommand.ExecuteNonQuery();
                id = int.Parse(sqlCommand.Parameters["@ID"].Value.ToString());
                return id;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                return id;

            }
            finally
            {
                CerrarConexion();
            }
        }



        public string BuscarEmailCliente(string identificacion)
        {

            try
            {

                AbrirConexion();
                string query = "select email from tblCliente where identificacion =" + identificacion;
                SqlCommand cmd = new SqlCommand(query, sqlConexion);
                SqlDataReader cli = cmd.ExecuteReader();
                string mail;

                if (cli.Read())
                {
                    mail = cli["email"].ToString();
                    cli.Close();
                    CerrarConexion();
                    return mail;

                }

                else { CerrarConexion(); return ""; }

             

            }
            catch (Exception ex)
            {
                CerrarConexion();
                Console.WriteLine("ERROR: " + ex.Message);
                return null;
            }
          


        }

        public string BuscarTlfCliente(string identificacion)
        {
            try
            {

                AbrirConexion();
                string query = "select telefono from tblCliente where identificacion =" + identificacion;
                SqlCommand cmd = new SqlCommand(query, sqlConexion);
                SqlDataReader cli = cmd.ExecuteReader();
                string telefono;
                if (cli.Read())
                {
                    telefono = cli["telefono"].ToString();
                    cli.Close(); ;
                    CerrarConexion();
                    return telefono;
                }
                else { CerrarConexion(); return ""; }


            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                return "";
            }
        }

        public string BuscardireccCliente(string identificacion)
        {
            try
            {

                AbrirConexion();
                string query = "select direccion from tblCliente where identificacion =" + identificacion;
                SqlCommand cmd = new SqlCommand(query, sqlConexion);
                SqlDataReader cli = cmd.ExecuteReader();
                string direc;
                if (cli.Read())
                {
                    direc = cli["direccion"].ToString();
                    cli.Close();
                    CerrarConexion();
                    return direc;
                }
                else { CerrarConexion(); return ""; }

            }
            catch (Exception ex)
            {
                CerrarConexion();
                Console.WriteLine("ERROR: " + ex.Message);
                return "";
            }
        }

        public string BuscarNombreCliente(string identificacion)
        {
            try
            {

                AbrirConexion();
                string query = "select nombreCliente from tblCliente where identificacion =" + identificacion;
                SqlCommand cmd = new SqlCommand(query, sqlConexion);
                SqlDataReader cli = cmd.ExecuteReader();
                string nombre;
                if (cli.Read())
                {
                    nombre = cli["nombreCliente"].ToString();
                    cli.Close();
                    CerrarConexion();
                    return nombre;
                }
                else { CerrarConexion(); return ""; }



            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                CerrarConexion();
                return "";
            }
        }

        public string BuscarApellidoCliente(string identificacion)
        {
            try
            {

                AbrirConexion();
                string query = "select apellidoCliente from tblCliente where identificacion =" + identificacion;
                SqlCommand cmd = new SqlCommand(query, sqlConexion);
                SqlDataReader cli = cmd.ExecuteReader();
                string apellido;
                if (cli.Read())
                {
                    apellido = cli["apellidoCliente"].ToString();
                    cli.Close();
                    CerrarConexion();
                    return apellido;
                }
                else { CerrarConexion(); return ""; }


            }
            catch (Exception ex)
            {
                CerrarConexion();
                Console.WriteLine("ERROR: " + ex.Message);
                return "";
            }
        }

        public DataTable CargarPrendas()
        {
            SqlCommand sqlCommand = new SqlCommand("spMostrarPrendas", sqlConexion);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            dt.Columns.Add("idPrenda");
            dt.Columns.Add("nombrePrenda");
            try
            {
                AbrirConexion();
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dt.Rows.Add(reader["idPrenda"], reader["nombrePrenda"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);

            }
            CerrarConexion();
            return dt;

        }
        
        //DETALLE PEDIDO
        public DataTable MostrarPrendasyPedido(int idPedido)
        {
            DataTable dtPPS = new DataTable();
            SqlCommand sqlCommand = new SqlCommand("spPrendasyPedido", sqlConexion);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@idPedido", idPedido);

            dtPPS.Columns.Add("idDetalle", typeof(int));
            dtPPS.Columns.Add("nombreServicio", typeof(string));
            dtPPS.Columns.Add("nombrePrenda", typeof(string));
            dtPPS.Columns.Add("descripcion", typeof(string));


            AbrirConexion();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    dtPPS.Rows.Add(reader["idDetalle"], reader["nombreServicio"], reader["nombrePrenda"], reader["descripcion"]);
                }
            }
            CerrarConexion();
            return dtPPS;
        }

        public string MostrarObservacion(int idPedido)
        {
            try
            {

                AbrirConexion();
                string query = "select observacion from tblEntrega 	 where tblEntrega.idPedido=" + idPedido;
                SqlCommand cmd = new SqlCommand(query, sqlConexion);
                SqlDataReader reg = cmd.ExecuteReader();

                if (reg.Read())
                {
                    return reg["observacion"].ToString();
                    reg.Close();
                }
                else { return "Null"; }

                _ = cmd.ExecuteNonQuery();
                CerrarConexion();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                return "";
            }
        }

        //LOGIN

        public bool validarUsuario(string Usuario, String pass)
        {

            try
            {
                AbrirConexion();
                string query = "SELECT COUNT(1) FROM tblUsuario WHERE NombreUsuario=@NombreUsuario AND Password=@Password";
                SqlCommand command = new SqlCommand(query, sqlConexion);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@NombreUsuario", Usuario);
                command.Parameters.AddWithValue("@Password", pass);
                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                CerrarConexion();
            }

        }

        //PARTE DE RECLAMOS

        //frmReclamosPendientes
        public DataTable MostrarReclamosPendientes()
        {
            DataTable dtPPS = new DataTable();
            SqlCommand sqlCommand = new SqlCommand("spReclamosPendientes", sqlConexion);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            dtPPS.Columns.Add("idReclamo", typeof(int));
            dtPPS.Columns.Add("nombreServicio", typeof(string));
            dtPPS.Columns.Add("nombrePrenda", typeof(string));
            dtPPS.Columns.Add("descripcionReclamo", typeof(string));
            dtPPS.Columns.Add("fechaGeneracion", typeof(string));


            AbrirConexion();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    dtPPS.Rows.Add(reader["idReclamo"], reader["nombreServicio"], reader["nombrePrenda"], reader["descripcionReclamo"], reader["fechaGeneracion"]);
                }
            }
            CerrarConexion();
            return dtPPS;
        }

        //frmConsularReclamo

        //mtodo para guardar las prendas reclamo
        public int GuardarPrendaReclamo(PrendaReclamo pReclamo)
        {
            int resultado = 0;
            AbrirConexion();
            string SQL = "Insert Into tblPrendaReclamo(descripcionReclamo, imagen)";
            SQL += " Values(@descripcion, @imagen)";
            SqlCommand comando = new SqlCommand(SQL, sqlConexion);
            comando.Parameters.AddWithValue("@descripcion", pReclamo.DescripcionReclamo);
            comando.Parameters.AddWithValue("@imagen", pReclamo.Foto);
            resultado = comando.ExecuteNonQuery();
            CerrarConexion();
            return resultado;
        }
        //METODOS PARA FORM CONSULTAR RECLAMOS
        public string BuscaridClienteR(int idReclamo)
        {
            try
            {

                AbrirConexion();
                string query = "select tblCliente.identificacion from tblReclamo inner join tblDetallePedido on tblDetallePedido.idDetalle=tblReclamo.idDetalle inner join tblPedido on tblPedido.idPedido=tblDetallePedido.idPedido inner join tblCliente on tblCliente.idCliente=tblPedido.idCliente where idReclamo=" + idReclamo;
                SqlCommand cmd = new SqlCommand(query, sqlConexion);
                SqlDataReader reg = cmd.ExecuteReader();


                if (reg.Read())
                {
                    return reg["identificacion"].ToString();
                    reg.Close();

                }
                else { return "Null"; }

                _ = cmd.ExecuteNonQuery();

                CerrarConexion();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                return "";
            }

        }


        public string BuscarNombreClienteR(int idReclamo)
        {
            try
            {

                AbrirConexion();
                string query = "select nombreCliente+' '+apellidoCliente as NombreCliente from tblReclamo inner join tblDetallePedido on tblDetallePedido.idDetalle=tblReclamo.idDetalle inner join tblPedido on tblPedido.idPedido=tblDetallePedido.idPedido inner join tblCliente on tblCliente.idCliente=tblPedido.idCliente where idReclamo= " + idReclamo;
                SqlCommand cmd = new SqlCommand(query, sqlConexion);
                SqlDataReader reg = cmd.ExecuteReader();

                if (reg.Read())
                {
                    return reg["nombreCliente"].ToString();
                    reg.Close();
                }
                else { return "Null"; }

                _ = cmd.ExecuteNonQuery();
                CerrarConexion();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                return "";
            }
        }

        public string BuscarTelefonoClienteR(int idReclamo)
        {
            try
            {

                AbrirConexion();
                string query = "select telefono from tblReclamo inner join tblDetallePedido on tblDetallePedido.idDetalle=tblReclamo.idDetalle inner join tblPedido on tblPedido.idPedido=tblDetallePedido.idPedido inner join tblCliente on tblCliente.idCliente=tblPedido.idCliente where idReclamo= " + idReclamo;
                SqlCommand cmd = new SqlCommand(query, sqlConexion);
                SqlDataReader reg = cmd.ExecuteReader();

                if (reg.Read())
                {
                    return reg["telefono"].ToString();
                    reg.Close();
                }
                else { return "Null"; }

                _ = cmd.ExecuteNonQuery();
                CerrarConexion();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                return "";
            }

        }
        public string BuscarEmailClienteR(int idReclamo)
        {
            try
            {

                AbrirConexion();
                string query = "select email from tblReclamo inner join tblDetallePedido on tblDetallePedido.idDetalle=tblReclamo.idDetalle inner join tblPedido on tblPedido.idPedido=tblDetallePedido.idPedido inner join tblCliente on tblCliente.idCliente=tblPedido.idCliente where idReclamo=" + idReclamo;
                SqlCommand cmd = new SqlCommand(query, sqlConexion);
                SqlDataReader reg = cmd.ExecuteReader();

                if (reg.Read())
                {
                    return reg["email"].ToString();
                    reg.Close();
                }
                else { return "Null"; }

                _ = cmd.ExecuteNonQuery();
                CerrarConexion();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                return "";
            }
        }
        public string BuscarFechaR(int idReclamo)
        {
            try
            {
                AbrirConexion();
                string query = "select CONVERT(VARCHAR(10),fechaGeneracion,103) as fechaGeneracion  from tblReclamo inner join tblDetallePedido on tblDetallePedido.idDetalle=tblReclamo.idDetalle inner join tblPedido on tblPedido.idPedido=tblDetallePedido.idPedido inner join tblCliente on tblCliente.idCliente=tblPedido.idCliente where idReclamo=" + idReclamo;
                SqlCommand cmd = new SqlCommand(query, sqlConexion);
                SqlDataReader reg = cmd.ExecuteReader();

                if (reg.Read())
                {
                    return reg["fechaGeneracion"].ToString();
                    reg.Close();
                }
                else { return "Null"; }

                _ = cmd.ExecuteNonQuery();
                CerrarConexion();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                return "";
            }
        }
        //metodo para listar reclamos en frmConsultaREclamos
        public DataTable MostrarReclamosXid(int idReclamo)
        {
            DataTable dtPPS = new DataTable();
            SqlCommand sqlCommand = new SqlCommand("spReclamoConsulta", sqlConexion);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@idReclamo", idReclamo);

            dtPPS.Columns.Add("nombreServicio", typeof(string));
            dtPPS.Columns.Add("nombrePrenda", typeof(string));
            dtPPS.Columns.Add("descripcionReclamo", typeof(string));
            dtPPS.Columns.Add("estado", typeof(string));


            AbrirConexion();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    dtPPS.Rows.Add(reader["nombreServicio"], reader["nombrePrenda"], reader["descripcionReclamo"], reader["estado"]);
                }
            }
            CerrarConexion();
            return dtPPS;
        }

        //FORMULARIO DE RECLAMOS
        //METODOS
        public string BuscaridCliente(int idPedido)
        {
            try
            {

                AbrirConexion();
                string query = "Select tblCliente.identificacion from tblPedido inner join tblCliente on tblCliente.idCliente=tblPedido.idCliente where idPedido=" + idPedido;
                SqlCommand cmd = new SqlCommand(query, sqlConexion);
                SqlDataReader reg = cmd.ExecuteReader();


                if (reg.Read())
                {
                    return reg["identificacion"].ToString();
                    reg.Close();

                }
                else { return "Null"; }

                _ = cmd.ExecuteNonQuery();

                CerrarConexion();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                return "";
            }

        }
        public string BuscarNombreCliente(int idPedido)
        {
            try
            {

                AbrirConexion();
                string query = "select nombreCliente+' '+apellidoCliente as NombreCliente from tblPedido inner join tblCliente on tblCliente.idCliente = tblPedido.idCliente where idPedido = " + idPedido;
                SqlCommand cmd = new SqlCommand(query, sqlConexion);
                SqlDataReader reg = cmd.ExecuteReader();

                if (reg.Read())
                {
                    return reg["nombreCliente"].ToString();
                    reg.Close();
                }
                else { return "Null"; }

                _ = cmd.ExecuteNonQuery();
                CerrarConexion();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                return "";
            }
        }

        public string BuscarTelefonoCliente(int idPedido)
        {
            try
            {

                AbrirConexion();
                string query = "select telefono from tblPedido inner join tblCliente on tblCliente.idCliente=tblPedido.idCliente where idPedido= " + idPedido;
                SqlCommand cmd = new SqlCommand(query, sqlConexion);
                SqlDataReader reg = cmd.ExecuteReader();

                if (reg.Read())
                {
                    return reg["telefono"].ToString();
                    reg.Close();
                }
                else { return "Null"; }

                _ = cmd.ExecuteNonQuery();
                CerrarConexion();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                return "";
            }

        }
        public string BuscarEmailCliente(int idPedido)
        {
            try
            {

                AbrirConexion();
                string query = "select email from tblPedido inner join tblCliente on tblCliente.idCliente=tblPedido.idCliente where idPedido =" + idPedido;
                SqlCommand cmd = new SqlCommand(query, sqlConexion);
                SqlDataReader reg = cmd.ExecuteReader();

                if (reg.Read())
                {
                    return reg["email"].ToString();
                    reg.Close();
                }
                else { return "Null"; }

                _ = cmd.ExecuteNonQuery();
                CerrarConexion();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                return "";
            }
        }
        public string BuscarFechaPedido(int idPedido)
        {
            try
            {
                AbrirConexion();
                string query = "select CONVERT(VARCHAR(10),fecha,103) as fecha  from tblPedido inner join tblCliente on tblCliente.idCliente=tblPedido.idCliente where idPedido=" + idPedido;
                SqlCommand cmd = new SqlCommand(query, sqlConexion);
                SqlDataReader reg = cmd.ExecuteReader();

                if (reg.Read())
                {
                    return reg["fecha"].ToString();
                    reg.Close();
                }
                else { return "Null"; }

                _ = cmd.ExecuteNonQuery();
                CerrarConexion();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                return "";
            }
        }
        //metodo prendaXPedido
        

        //metodo para listar prendas por pedido


        public DataTable MostrarPrendasXPedido(int idPedido)
        {
            DataTable dtPPS = new DataTable();
            SqlCommand sqlCommand = new SqlCommand("spPrendasxPedido", sqlConexion);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@idPedido", idPedido);

            dtPPS.Columns.Add("idDetalle", typeof(int));
            dtPPS.Columns.Add("nombreServicio", typeof(string));
            dtPPS.Columns.Add("nombrePrenda", typeof(string));
            dtPPS.Columns.Add("descripcion", typeof(string));


            AbrirConexion();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    dtPPS.Rows.Add(reader["idDetalle"], reader["nombreServicio"], reader["nombrePrenda"], reader["descripcion"]);
                }
            }
            CerrarConexion();
            return dtPPS;
        }
        //FRM VENTANA PARA GENERAR RECLAMO
        //Metodo para guardar reclamo
        public int AgregarReclamo(int idPedido, int idDetalle, DateTime now)
        {
            int resultado = 0;
            //DateTime fecha = now.Date;
            AbrirConexion();
            SqlCommand _command = new SqlCommand("spRegistrarReclamo", sqlConexion);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Parameters.AddWithValue("@idPedido", idPedido);
            _command.Parameters.AddWithValue("@idDetalle", idDetalle);
            _command.Parameters.AddWithValue("@fechaGen", now);

            resultado = _command.ExecuteNonQuery();
            CerrarConexion();
            return resultado;
        }
        public DataTable MostrarFacturas(string _fecha)
        {
       
            DataTable dtPPS = new DataTable();
            try
            {
                
                SqlCommand sqlCommand = new SqlCommand("spMostrarFacturas", sqlConexion);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@fecha", Convert.ToDateTime(_fecha));

                dtPPS.Columns.Add("idPedido", typeof(int));
                dtPPS.Columns.Add("fecha", typeof(DateTime));
                dtPPS.Columns.Add("Subtotal", typeof(float));
                dtPPS.Columns.Add("descuento", typeof(float));
                dtPPS.Columns.Add("IVA", typeof(float));
                dtPPS.Columns.Add("Total", typeof(float));

                AbrirConexion();
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dtPPS.Rows.Add(reader["idPedido"], reader["fecha"], reader["Subtotal"], reader["descuento"], reader["IVA"], reader["Total"]);
                    }
                }
                CerrarConexion();
                return dtPPS;
            }
            catch
            {
                CerrarConexion();
                return dtPPS;
            }
            
        }

        //new
        public string DescripcionPrendaReclamo(string idReclamo)
        {
            try
            {

                AbrirConexion();
                string query = "select descripcionReclamo from tblReclamo inner join tblPrendaReclamo  on tblPrendaReclamo.idPrendaReclamo=tblReclamo.idReclamo where idReclamo=" + idReclamo;
                SqlCommand cmd = new SqlCommand(query, sqlConexion);
                SqlDataReader cli = cmd.ExecuteReader();
                string nombre;
                if (cli.Read())
                {
                    nombre = cli["descripcionReclamo"].ToString();
                    cli.Close();
                    CerrarConexion();
                    return nombre;
                }
                else { CerrarConexion(); return ""; }



            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                CerrarConexion();
                return "";
            }
        }

        public int EditarEstadoReclamo(int idReclamo, string estado)
        {
            int Resulta;
            AbrirConexion();
            SqlCommand _command = new SqlCommand("spEditarEstadoReclamo", sqlConexion);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Parameters.AddWithValue("@idReclamo", idReclamo);
            _command.Parameters.AddWithValue("@estado", estado);
            Resulta = _command.ExecuteNonQuery();
            CerrarConexion();
            return Resulta;
        }

        public Byte[] RecuperarImagen(int idReclamo)
        {


            AbrirConexion();
            string query = "select imagen from tblReclamo inner join tblPrendaReclamo  on tblPrendaReclamo.idPrendaReclamo=tblReclamo.idReclamo where idReclamo=" + idReclamo;
            SqlCommand cmd = new SqlCommand(query, sqlConexion);
            SqlDataReader cli = cmd.ExecuteReader();
            while (cli.Read())
            {
                PrendaReclamo prendaR = new PrendaReclamo();

                prendaR.Foto = (byte[])cli["imagen"];
                return prendaR.Foto;
            }
            CerrarConexion();
            return null;
        }
    }
}


//for(int i=0;i<_reader.FieldCount;i++)
//{

//    int m_temp = _reader.GetInt32(0);
//    m_ped.IDpedido = m_temp.ToString();
//    //m_ped.AddPedido(m_temp, null);
//    Lista.Add(m_ped);
//    _reader.Read();
//}