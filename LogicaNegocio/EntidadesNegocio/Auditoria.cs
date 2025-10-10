using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesNegocio
{
    public class Auditoria
    {
        public int Id { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public string Entidad { get; set; } = string.Empty;
        public string Operacion { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;

        public string? Detalle { get; set; }
    }
}
