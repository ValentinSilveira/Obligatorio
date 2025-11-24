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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped<ICUAltaUsuario, CUAltaUsuario>();
            builder.Services.AddScoped<ICUAltaGasto, CUAltaGasto>();
            builder.Services.AddScoped<ICUBuscarUsuario, CUBuscarUsuario>();
            builder.Services.AddScoped<ICUBuscarGasto, CUBuscarGasto>();
            builder.Services.AddScoped<ICUBuscarPago, CUBuscarPago>();
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
            builder.Services.AddScoped<ICUListadoPagoPorFecha, CUListadoPagoPorFecha>();
            builder.Services.AddScoped<ICUAuditoria, CUAuditoria>();
            builder.Services.AddScoped<ICUListadoPorPrecio, CUListadoPorPrecio>();
            builder.Services.AddScoped<IRepositorioAuditoria, RepositorioAuditoriaEF>();
            builder.Services.AddScoped<ICUPagosPorUsuario, CUPagosPorUsuario>();
            builder.Services.AddScoped<ICUPagosUnicosConMontoSuperior, CUPagosUnicosConMontoSuperior>();
            builder.Services.AddScoped<ICUCambiarPassword, CUCambiarPassword>();

            builder.Services.AddDbContext<ObligatorioContexto>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MiConexion"))
            );


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt => opt.IncludeXmlComments("WebAPI.xml"));

            var claveSecreta = "ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE=";

            builder.Services.AddAuthentication(aut =>
            {
                aut.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                aut.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(aut =>
            {
                aut.RequireHttpsMetadata = false;
                aut.SaveToken = true;
                aut.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(claveSecreta)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

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

