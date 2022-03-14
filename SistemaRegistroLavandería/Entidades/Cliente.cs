using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace Directorio_Entidades
{
    public class Cliente : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
      
        

        private void RaiseNotifyChanged(String nombrePropiedad)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nombrePropiedad));
            }
        }
        public string idCliente
        {
            get ; 
            set;
        }
        private string nombre;
        public string Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;
                RaiseNotifyChanged("nombre");
            }

        }
        
        public string apellido
        {
            get;
            set;
        }
        
        public string telfono
        {
            get;
            set;
        }
        
        public string direccion
        {
            get;
            set;
        }
    }
}
