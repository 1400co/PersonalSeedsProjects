using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleNancy.DTO
{
    class RutaAttribute
    {
        public string Ruta { get; set; }

        public RutaAttribute(string ruta)
        {
            this.Ruta = ruta;
        }

        public string g()
        {
            return Ruta;
        }
    }
}
