using CasosDeUsos.DTOs.UsuariosDTO;
using CasosDeUsos.InterfacesCasosUsos.IUsuarioCU;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.interfacesRepositorios;
using LogicaNegocio.ValueObjects.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUCambiarPassword : ICUCambiarPassword
    {
        public IRepositorioUsuario RepoUsuarios { get; set; }

        public CUCambiarPassword(IRepositorioUsuario repoUsuarios)
        {
            RepoUsuarios = repoUsuarios;
        }

        public void Ejecutar(UsuarioCambiarPasswordDTO dto)
        { 
            Usuario usuario = RepoUsuarios.FindById(dto.UsuarioId);
            if (usuario == null)
            {
                throw new Exception("El usuario no existe");
            }
            if (string.IsNullOrWhiteSpace(dto.NuevaPassword))
            {
                throw new Exception("La nueva contraseña no puede estar vacía");
            }
            usuario.Password = new Password(dto.NuevaPassword);
            RepoUsuarios.Update(usuario);
        }
    }
}
