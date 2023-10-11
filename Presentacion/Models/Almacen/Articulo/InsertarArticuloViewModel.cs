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
        public bool Estado { get; set; }
        [ForeignKey("IdCategorias")] // Nombre de la propiedad de navegación en la entidad relacionada
        public int IdCategorias { get; set; }
      
        // Propiedad de navegación para la relación con Categorias
       
        
    }
}
