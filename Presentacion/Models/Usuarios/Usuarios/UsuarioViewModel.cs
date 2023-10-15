using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Presentacion.Models.Usuarios.Usuarios
{
    public class UsuarioViewModel
    {
        public int IdUsuario { get; set; }    
        public int IdRol { get; set; }
        public string nombreRol { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        public bool Estado { get; set; } = true;

    }
}
