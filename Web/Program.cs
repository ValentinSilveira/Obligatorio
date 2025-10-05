
using CasosDeUsos.InterfacesCasosUsos.IGastoCU;
using CasosDeUsos.InterfacesCasosUsos.IPagoCU;
using CasosDeUsos.InterfacesCasosUsos.IUsuarioCU;
using LogicaAccesoDatos;
using LogicaAccesoDatos.Repositorio;
using LogicaAplicacion.CasosUso.CUGasto;
using LogicaAplicacion.CasosUso.CUGastos;
using LogicaAplicacion.CasosUso.CUPago;
using LogicaAplicacion.CasosUso.CUUsuario;
using LogicaAplicacion.CasosUso.CUUsuarios;
using LogicaAplicacion.InterfacesCasosUsos;
using LogicaNegocio.interfacesRepositorios;
using Microsoft.EntityFrameworkCore;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
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
            builder.Services.AddScoped<ICUListadoRol, CUListadoRoles>();
            builder.Services.AddScoped<ILogin, CULogin>();
            builder.Services.AddScoped<ICUAltaPagoUnico, CUAltaPagoUnico>();
            builder.Services.AddScoped<ICUAltaPagoRecurrente, CUAltaPagoRecurrente>();
            builder.Services.AddScoped<IRepositorioPago, PagoRepositorioEF>();
            builder.Services.AddScoped<ICUListadoPago, CUListadoPago>();
            builder.Services.AddScoped<ICUObtenerMetodoPago, CUObtenerMetodoPago>();
            builder.Services.AddScoped<ICUEditarGasto, CUEditarGasto>();
            builder.Services.AddScoped<ICUListadoEquipo, CUListadoEquipos>();
            builder.Services.AddScoped<IRepositorioEquipo, RepositorioEquipoEF>();


            string cadenaConexion = builder.Configuration.GetConnectionString("CadenaConexion");
            builder.Services.AddDbContext<ObligatorioContexto>(options => options.UseSqlServer(cadenaConexion));

            
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession(); 

            app.UseAuthorization();

           
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Login}/{id?}");

            app.Run();
        }
    }
}


