using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias.ExcepcionesEntidades
{
    public class GastoException : Exception
    {
        public GastoException() { }

        public GastoException(string message) : base(message) { }

        public GastoException(string message, Exception innerException) : base(message, innerException) { }
    }
}
