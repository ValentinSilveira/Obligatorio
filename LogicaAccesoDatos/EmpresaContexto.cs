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
        public DbSet<Usuario> Usuarios { get; set; } // Cambiado de private a public
        public EmpresaContexto(DbContextOptions options) : base(options){}
    }
}
