using System.ComponentModel.DataAnnotations;

namespace Presentacion.Models.Ventas.Clientes
{
    public class InsertarPersonaViewModel
    {

        public string TipoPersona { get; set; } = string.Empty;
        public string NombrePersona { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string DireccionPersona { get; set; } = string.Empty;
        public string TelefonoPersona { get; set; } = string.Empty;

        [EmailAddress]
        public string EmailPersona { get; set; } = string.Empty;
    }
}
