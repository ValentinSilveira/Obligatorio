using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcepcionesPropias.ExcepcionesEntidades;

namespace LogicaNegocio.ValueObjects.Usuario
{
    public record Password
    {
        public string Valor { get; init; }

        public Password(string valor)
        {
            Valor = valor;
            Validar();
        }

        private void Validar()
        {
            if (Valor.Length < 8)
                throw new UsuarioException("La contraseña debe contener al menos 8 caracteres.");
        }
    }
}
