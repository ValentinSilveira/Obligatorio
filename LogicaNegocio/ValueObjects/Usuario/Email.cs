using ExcepcionesPropias.ExcepcionesEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.ValueObjects.Usuario
{
    /*
        El email se genera automáticamente combinando las primeras
        tres letras del nombre y las primeras tres letras del apellido, seguido por el dominio @laEmpresa.com.
        Si el nombre o el apellido tienen menos de tres letras, se utilizan completos. En caso de que el email
        generado ya exista, se le agrega un número al final para evitar duplicados. Los caracteres con tildes y
        otras alteraciones (eñes, vocales con tildes, diéresis, etc.) deberán remplazarse por sus versiones sin
        la alteración.
        Por ejemplo, si el nombre fuera Juan Núñez, el correo generado debería ser
        juanun@laempresa.com y si ya existiera otro igual debería ser juanun1234@laempresa.com. El número (en el
        ejemplo 1234) deberá generarse en forma aleatoria.
     */
    public class Email
    {
        public string Nombre { get; private set; }
        public string Apellido {  get; private set; }
        public string Dominio = "@laEmpresa.com";

        public Email(string nombre, string apellido)
        {
            Nombre = nombre;
            Apellido = apellido;
            Validar();
        }

        private void Validar()
        {
            /* Pendiente: Hay que verificar si se le pasa por parametros el nombre y el apellido
               del usuario ya que eso modifica tambien el constructor del VO del email en la clase Usuario
            */
        }
    }
}
