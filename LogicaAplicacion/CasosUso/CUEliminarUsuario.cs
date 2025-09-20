using CasosDeUsos.InterfacesCasosUsos.IUsuarioCU;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso
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
