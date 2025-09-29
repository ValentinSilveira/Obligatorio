<<<<<<< HEAD
﻿using CasosDeUsos.DTOs.UsuariosDTO;
=======
﻿using CasosDeUsos.DTOs.DTOsUsuario;
>>>>>>> origin
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUsos.InterfacesCasosUsos.IUsuarioCU
{
    public interface ICUListadoUsuario
    {
        IEnumerable<ListadoUsuarioDTO> Ejecutar();
    }
}
