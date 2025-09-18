using LogicaNegocio.EntidadesNegocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos
{
    public class EmpresaContexto : DbContext
    {
        public EmpresaContexto(DbContextOptions options) : base(options)
        {
        }
    }
}
