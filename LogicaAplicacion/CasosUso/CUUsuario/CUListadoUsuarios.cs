<<<<<<< HEAD
﻿using CasosDeUsos.DTOs.UsuariosDTO;
=======
﻿using CasosDeUsos.DTOs.DTOsUsuario;
>>>>>>> origin
using CasosDeUsos.InterfacesCasosUsos.IUsuarioCU;
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
    public class CUListadoUsuarios : ICUListadoUsuario
    {
        public IRepositorioUsuario RepoCliente { get; set; }
        public ICUListadoUsuario CUListadoUsuario { get; set; }

        public CUListadoUsuarios(IRepositorioUsuario repoCliente)
        {
            RepoCliente = repoCliente;
        }

        public IEnumerable<ListadoUsuarioDTO> Ejecutar()
        {
            IEnumerable<Usuario> usuarios = RepoCliente.FindAll();
            return MapperUsuario.UsuarioToUsuarioListadoDTO(usuarios);
        }
    }
}

