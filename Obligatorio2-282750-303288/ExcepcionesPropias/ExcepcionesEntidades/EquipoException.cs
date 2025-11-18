using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias.ExcepcionesEntidades
{
    public class EquipoException : Exception
    {
        public EquipoException() { }

        public EquipoException(string message) : base(message) { }

        public EquipoException(string message, Exception innerException) : base(message, innerException) { }
    }
}
