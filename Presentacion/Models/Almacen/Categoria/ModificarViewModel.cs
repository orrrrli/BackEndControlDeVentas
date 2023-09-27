using System.ComponentModel.DataAnnotations;

namespace Presentacion.Models.Almacen.Categoria
{
    public class ModificarViewModel
    {
        [Required]
        public int IdCategorias { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener menos de 3 caracteres, ni más de 100")]
        public string nombreCategorias { get; set; } = string.Empty;
        [StringLength(250)]
        public string Descripcion { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}
