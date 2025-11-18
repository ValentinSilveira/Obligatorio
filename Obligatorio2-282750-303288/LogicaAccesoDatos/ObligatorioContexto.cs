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
        public DbSet<Auditoria> Auditorias { get; set; }

        public ObligatorioContexto(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //TPH
            modelBuilder.Entity<Pago>()
                .HasDiscriminator<string>("TipoPago")
                .HasValue<Recurrente>("Recurrente")
                .HasValue<Unico>("Unico");

            modelBuilder.Entity<Auditoria>()
                .Property(a => a.Usuario)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Auditoria>()
                .Property(a => a.Entidad)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Auditoria>()
                .Property(a => a.Operacion)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Unico>()
                .HasIndex(u => u.NroRecibo)
                .IsUnique();

            modelBuilder.Entity<Pago>()
            .Property(p => p.Metodo)
            .HasConversion<string>();
        }
    }
}
