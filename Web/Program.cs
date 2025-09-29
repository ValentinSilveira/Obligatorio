
using CasosDeUsos.InterfacesCasosUsos.IGastoCU;
<<<<<<< HEAD
using CasosDeUsos.InterfacesCasosUsos.IPagoCU;
using CasosDeUsos.InterfacesCasosUsos.IUsuarioCU;
using LogicaAccesoDatos;
using LogicaAccesoDatos.Repositorio;
using LogicaAplicacion.CasosUso.CUGastos;
using LogicaAplicacion.CasosUso.CUPago;
using LogicaAplicacion.CasosUso.CUUsuarios;
=======
using CasosDeUsos.InterfacesCasosUsos.IUsuarioCU;
using LogicaAccesoDatos;
using LogicaAccesoDatos.Repositorio;
using LogicaAplicacion.CasosUso.CUGasto;
using LogicaAplicacion.CasosUso.CUUsuario;
>>>>>>> origin
using LogicaAplicacion.InterfacesCasosUsos;
using LogicaNegocio.interfacesRepositorios.InterfacesGastos;
using LogicaNegocio.interfacesRepositorios.InterfacesUsuarios;
using Microsoft.EntityFrameworkCore;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 🔧 Registro de servicios
            builder.Services.AddScoped<ICUAltaUsuario, CUAltaUsuario>();
            builder.Services.AddScoped<ICUAltaGasto, CUAltaGasto>();
            builder.Services.AddScoped<ICUBuscarUsuario, CUBuscarUsuario>();
            builder.Services.AddScoped<ICUBuscarGasto, CUBuscarGasto>();
            builder.Services.AddScoped<ICUListadoUsuario, CUListadoUsuarios>();
            builder.Services.AddScoped<ICUListadoGasto, CUListadoGasto>();
            builder.Services.AddScoped<ICUEliminarUsuario, CUEliminarUsuario>();
            builder.Services.AddScoped<ICUEliminarGasto, CUEliminarGasto>();
            builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuarioEF>();
            builder.Services.AddScoped<IRepositorioGasto, GastoRepositorioEF>();
            builder.Services.AddScoped<IRepositorioRol, RepositorioRolEF>();
            builder.Services.AddScoped<IListadoRoles, ListadoRoles>();
            builder.Services.AddScoped<ILogin, CULogin>();
<<<<<<< HEAD
            builder.Services.AddScoped<ICUAltaPagoUnico, CUAltaPagoUnico>();
            builder.Services.AddScoped<ICUAltaPagoRecurrente, CUAltaPagoRecurrente>();
            builder.Services.AddScoped<IRepositorioPago, PagoRepositorioEF>();
=======
            builder.Services.AddScoped<ICUModificarGasto, CUModificarGasto>();
>>>>>>> origin

            // 🔧 Configuración de EF Core
            string cadenaConexion = builder.Configuration.GetConnectionString("CadenaConexion");
            builder.Services.AddDbContext<ObligatorioContexto>(options => options.UseSqlServer(cadenaConexion));

            // 🔧 Configuración de sesión
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // 🔧 MVC
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // 🔧 Middleware
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession(); // ✅ Habilita el uso de sesiones

            app.UseAuthorization();

            // 🔧 Rutas
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Login}/{id?}");

            app.Run();
        }
    }
}


