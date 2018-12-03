using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFramework
{
    public class Resultado<T>
    {
        public Resultado(T datos, string idMensaje, string modulo)
        {
            Datos = datos;
            IdMensaje = idMensaje;
            Modulo = modulo;
        
        }

        public Resultado(T datos) 
        {
            Datos = datos;
            IdMensaje = string.Empty;
            Modulo = string.Empty;
        }

        public Resultado(string idMensaje, string modulo)
        {
            IdMensaje = idMensaje;
            Modulo = modulo;
        }
        public Resultado() { }

        public T Datos
        {
            get;
            private set;
        }

        public string Modulo
        {
            get;
            private set;
        }

        public string IdMensaje
        {
            get;
            private set;
        }

        public bool TieneMensaje
        {
            get
            {
                return !String.IsNullOrEmpty(IdMensaje) && !String.IsNullOrEmpty(Modulo);
            }
        }

    }
}
