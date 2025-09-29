using CasosDeUsos.InterfacesCasosUsos.IUsuarioCU;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios.InterfacesUsuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

<<<<<<< HEAD
namespace LogicaAplicacion.CasosUso.CUUsuarios
=======
namespace LogicaAplicacion.CasosUso.CUUsuario
>>>>>>> origin
{
    public class CUEliminarUsuario : ICUEliminarUsuario
    {
        public IRepositorioUsuario RepoUsuario { get; set; }

        public CUEliminarUsuario(IRepositorioUsuario repoUsuario)
        {
            RepoUsuario = repoUsuario;
        }

        public void Ejecutar(int id)
        {
            Usuario usuario = RepoUsuario.FindById(id);
            if (id > 0)
            {
                if (usuario != null)
                {
                    RepoUsuario.Delete(usuario);
                }
                else
                {
                    throw new UsuarioException("El usuario con ese id no existe");
                }
            }
            else 
            {
                throw new ArgumentException("El id es incorrecto");
            }

        }
    }
}
