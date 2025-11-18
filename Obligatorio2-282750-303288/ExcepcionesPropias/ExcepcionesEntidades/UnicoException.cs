using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias.ExcepcionesEntidades
{
    public class UnicoException : Exception
    {
        public UnicoException() { }

        public UnicoException(string message) : base(message) { }

        public UnicoException(string message, Exception innerException) : base(message, innerException) { }
    }
}
