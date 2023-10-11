using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades.Almacen
{
    public class Articulos
    {
        public int IdArticulos { get; set; }
        public string codigoArticulo { get; set; } = string.Empty;
        public string nombreArticulo { get; set; } = string.Empty;
        public decimal precioArticulo { get; set; }
        public int stock { get; set; }
        public string descripcionArticulo { get; set; } = string.Empty;
        public bool Estado { get; set; }

        // Propiedad de navegación para la relación con Categorias
        [ForeignKey("IdCategorias")] // Nombre de la propiedad de navegación en la entidad relacionada
        public int IdCategorias { get; set; }

        // Propiedad de navegación a la entidad Categorias (puedes cambiar el nombre si es necesario)
        public Categorias Categoria { get; set; }
    }
}
