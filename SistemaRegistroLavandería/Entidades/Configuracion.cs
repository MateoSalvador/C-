using System;
using System.Collections.Generic;
using System.Text;

namespace Directorio_Entidades
{
    public class Configuracion
    {
        //*********PRENDA RECLAMO*************
        private int idprendareclamo;
        public int IDprendaReclamo
        {
            get { return idprendareclamo; }
            set
            {
                idprendareclamo = value;
            }
        }
        private string descripcionreclamo;
        public string DescripcionReclamo
        {
            get { return descripcionreclamo; }
            set
            {
                descripcionreclamo = value;
            }
        }
        private byte foto;
        public byte Foto
        {
            get { return foto; }
            set
            {
                foto = value;
            }
        }
    }
}
