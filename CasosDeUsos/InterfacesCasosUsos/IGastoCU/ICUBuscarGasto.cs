<<<<<<< HEAD
﻿using CasosDeUsos.DTOs.GastoDTO.GastoDTO;
=======
﻿using CasosDeUsos.DTOs.DTOsGasto;
>>>>>>> origin
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUsos.InterfacesCasosUsos.IGastoCU
{
    public interface ICUBuscarGasto
    {
        DetalleGastoDTO Ejecutar(int id);
    }
}
