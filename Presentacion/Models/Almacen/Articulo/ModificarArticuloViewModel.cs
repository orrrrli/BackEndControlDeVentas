using System.ComponentModel.DataAnnotations;

namespace Presentacion.Models.Almacen.Articulo
{
    
        public class ModificarArticuloViewModel
        {
            public int IdArticulos { get; set; }

            [Required]
            public int IdCategorias { get; set; }


            [StringLength(50, MinimumLength = 3, ErrorMessage = "El código no debe de tener menos de 3 caracteres, ni más de 50")]
            public string codigoArticulo { get; set; } = string.Empty;
            [Required]
            [StringLength(150, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener menos de 3 caracteres, ni más de 150")]
            public string nombreArticulo { get; set; } = string.Empty;

            [Required]
            public decimal precioArticulo { get; set; }
            [Required]
            public int Stock { get; set; }
            [StringLength(250, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener menos de 3 caracteres, ni más de 150")]
            public string descripcionArticulo { get; set; } = string.Empty;

            public bool estado { get; set; }

        }
 
}
