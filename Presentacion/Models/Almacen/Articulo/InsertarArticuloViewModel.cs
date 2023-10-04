using Entidades.Almacen;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Presentacion.Models.Almacen.Articulo
{
    public class InsertarArticuloViewModel
    {
 
        public string codigoArticulo { get; set; } = string.Empty;
        public string nombreArticulo { get; set; } = string.Empty;
        public decimal precioArticulo { get; set; }
        public int stock { get; set; }
        public string descripcionArticulo { get; set; } = string.Empty;
        public bool Estado { get; set; } = false;
        public int idCategoria { get; set; }
        public string nombreCategoria { get; set; } // Agrega esta propiedad para almacenar el nombre de la categoría

        // Propiedad de navegación para la relación con Categorias
        [ForeignKey("Categoria")] // Nombre de la propiedad de navegación en la entidad relacionada
        public Categorias Categoria { get; set; }
    }
}
