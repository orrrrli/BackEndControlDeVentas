using System.ComponentModel.DataAnnotations;

namespace Presentacion.Models.Usuarios.Usuarios
{
    public class InsertarUsuarioViewModel
    {
        public int IdRol { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty; //este dato es string porque se recibe del frontend
      
    }
}
