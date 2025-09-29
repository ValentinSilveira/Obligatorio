<<<<<<< HEAD
﻿using CasosDeUsos.DTOs.UsuariosDTO;
=======
﻿using CasosDeUsos.DTOs.DTOsUsuario;
>>>>>>> origin
using LogicaAplicacion.InterfacesCasosUsos;
using LogicaAplicacion.Mappers;
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
    public class CULogin : ILogin
    {
        public IRepositorioUsuario RepoUsuarios { get; set; }

        public CULogin(IRepositorioUsuario repoUsuarios)
        {
            RepoUsuarios = repoUsuarios;
        }

        public UsuarioLoginDTO Ejecutar(string name, string password)
        {
            Usuario usuario = RepoUsuarios.FindByEmailAndPassword(name, password);
            return MapperUsuario.UsuarioToUsuarioListadoDTO(usuario);
        }
    }
}
