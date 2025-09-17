using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Web.Models.Roles;

namespace Web.Models.Usuarios
{
    public class UsuarioViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        [DisplayName("Seleccione un Rol")]
        public int RolId { get; set; }
        public IEnumerable<RolListadoViewModel> Roles { get; set; } = new List<RolListadoViewModel>();
    }
}
