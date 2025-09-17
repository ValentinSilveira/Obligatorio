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
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Pago> Pago { get; set; }
        public DbSet<Gasto> Gasto { get; set; }
        public DbSet<Equipo> Equipo { get; set; }

        public EmpresaContexto(DbContextOptions options) : base(options)
        {
        }
    }
}
