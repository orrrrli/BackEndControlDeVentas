
using System.ComponentModel.DataAnnotations;

namespace Presentacion.Models.Usuarios.Roles
{
    public class ModificarRolViewModel
    {
        [Required]
        public int IdRol { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener menos de 3 caracteres, ni más de 100")]
        public string nombreRol { get; set; } = string.Empty;
        [StringLength(250)]
        public string descripcionRol { get; set; } = string.Empty;
        public bool estado { get; set; }
    }
}
