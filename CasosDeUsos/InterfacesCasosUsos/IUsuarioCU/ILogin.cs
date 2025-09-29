<<<<<<< HEAD:LogicaAplicacion/InterfacesCasosUsos/ILogin.cs
﻿using CasosDeUsos.DTOs.UsuariosDTO;
=======
﻿using CasosDeUsos.DTOs.DTOsUsuario;
>>>>>>> origin:CasosDeUsos/InterfacesCasosUsos/IUsuarioCU/ILogin.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosUsos
{
    public interface ILogin
    {
        UsuarioLoginDTO Ejecutar(string name, string password);
    }
}
