using CasosDeUsos.DTOs.UsuariosDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosUsos
{
    public interface ILogin
    {
        UsuarioLogueadoDTO Ejecutar(UsuarioLoginDTO usuarioLoginDTO);
    }
}
