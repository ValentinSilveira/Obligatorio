using CasosDeUsos.InterfacesCasosUsos.IPagoCU;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUPago
{
    public class CUObtenerMetodoPago : ICUObtenerMetodoPago
    {
        public string[] Ejecutar()
        {
            return Enum.GetNames(typeof(MetodoPago));
        }
    }
}
