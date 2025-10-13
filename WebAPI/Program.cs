
using CasosDeUsos.InterfacesCasosUsos.IPagoCU;
using LogicaAccesoDatos;
using LogicaAccesoDatos.Repositorio;
using LogicaAplicacion.CasosUso.CUPago;
using LogicaNegocio.interfacesRepositorios;
using Microsoft.EntityFrameworkCore;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped<ICUBuscarPago, CUBuscarPago>();
            builder.Services.AddScoped<IRepositorioPago,PagoRepositorioEF>();

            string cadenaConexion = builder.Configuration.GetConnectionString("CadenaConexion");
            builder.Services.AddDbContext<ObligatorioContexto>(options => options.UseSqlServer(cadenaConexion));


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt => opt.IncludeXmlComments("WebAPI.xml"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
