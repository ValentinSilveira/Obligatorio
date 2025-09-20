using LogicaNegocio.EntidadesNegocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos
{
    public class ObligatorioContexto : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Gasto> Gastos { get; set; }

        public ObligatorioContexto(DbContextOptions options) : base(options){}


    }
}
