using Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Ventas
{
    public class Personas
    {

        public int IdPersona { get; set; }
        public string TipoPersona { get; set; } = string.Empty;
        public string NombrePersona { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string DireccionPersona { get; set; } = string.Empty;
        public string TelefonoPersona { get; set; } = string.Empty;
        public string EmailPersona { get; set; } = string.Empty;
        
       
    }
}
