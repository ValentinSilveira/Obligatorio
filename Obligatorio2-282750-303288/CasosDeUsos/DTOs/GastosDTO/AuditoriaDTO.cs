using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUsos.DTOs.GastosDTO
{
    public class AuditoriaDTO
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Entidad { get; set; }
        public string Operacion { get; set; }
        public DateTime Fecha { get; set; }
        public string Detalle { get; set; }
    }
}
