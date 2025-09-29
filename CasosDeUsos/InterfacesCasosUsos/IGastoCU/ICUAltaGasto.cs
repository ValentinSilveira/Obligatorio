<<<<<<< HEAD
﻿using CasosDeUsos.DTOs.GastoDTO;
using CasosDeUsos.DTOs.GastosDTO;
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
    public interface ICUAltaGasto
    {
        void Ejecutar(GastoDTO gastoDTO);
    }
}
