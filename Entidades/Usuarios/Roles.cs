using System.ComponentModel.DataAnnotations;

namespace Entidades.Usuarios
{
    public class Roles
    {
        public int IdRol { get; set; }

        [StringLength(30, MinimumLength = 3, ErrorMessage = "El nombre no debe tener menos de tres caracteres, ni mas de 30")]
        public string nombreRol { get; set; } = string.Empty;

        [StringLength(100)]
        public string descripcionRol { get; set; } = string.Empty;
        public bool estado { get; set; } = false;

    }
}