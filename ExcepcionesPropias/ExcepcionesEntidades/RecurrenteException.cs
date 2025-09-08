using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias.ExcepcionesEntidades
{
    public class RecurrenteException : Exception
    {
        public RecurrenteException() { }

        public RecurrenteException(string message) : base(message) { }

        public RecurrenteException(string message, Exception innerException) : base(message, innerException) { }
    }
}
