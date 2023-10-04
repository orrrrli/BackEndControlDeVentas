using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Datos;
using Entidades.Almacen;
using Presentacion.Models.Almacen;
using Presentacion.Models;
using Presentacion.Models.Almacen.Articulo;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly BDContext _context;
        public ArticulosController(BDContext context)
        {
            _context = context;
        }

        #region GET: api/Articulos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Articulos>>> GetArticulos()
        {
            if (_context.Articulos == null)
            {
                return NotFound();
            }
            return await _context.Articulos.ToListAsync();
        }
        #endregion

        #region GET: api/Articulos/ListarArticulos
        [HttpGet("[action]")]
        public async Task<IEnumerable<ArticulosViewModel>> ListarArticulos()
        {
            var articulos = await _context.Articulos
                .Include(a => a.Categoria) // Cargar la propiedad de navegación Categoria
                .ToListAsync();

            return articulos.Select(c => new ArticulosViewModel
            {
                IdArticulos = c.IdArticulos,
                codigoArticulo = c.codigoArticulo,
                nombreArticulo = c.nombreArticulo,
                precioArticulo = c.precioArticulo,
                stock = c.stock,
                descripcionArticulo = c.descripcionArticulo,
                idCategoria = c.IdCategoria, // Aquí asignamos la propiedad idCategoria
                                             // También puedes acceder a las propiedades de la categoría, por ejemplo:
                nombreCategoria = c.Categoria != null ? c.Categoria.nombreCategorias : string.Empty
            });
        }

        #endregion

    }
}
