using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias.ExcepcionesEntidades
{
    public class PagoException : Exception
    {
        public PagoException() { }

        public PagoException(string message) : base(message) { }

        public PagoException(string message, Exception innerException) : base(message, innerException) { }
    }
}
