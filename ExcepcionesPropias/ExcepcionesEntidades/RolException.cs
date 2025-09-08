using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias.ExcepcionesEntidades
{
    public class RolException : Exception
    {
        public RolException() { }

        public RolException(string message) : base(message) { }

        public RolException(string message, Exception innerException) : base(message, innerException) { }
    }
}
