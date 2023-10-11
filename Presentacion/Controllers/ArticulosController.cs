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
                Estado = c.Estado,
                descripcionArticulo = c.descripcionArticulo,
                IdCategorias = c.IdCategorias, // Aquí asignamos la propiedad idCategoria
                                             // También puedes acceder a las propiedades de la categoría, por ejemplo:
                nombreCategoria = c.Categoria != null ? c.Categoria.nombreCategorias : string.Empty
            });
        }

        #endregion

        #region POST: api/Articulos/InsertarArticulos
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertarArticulo(InsertarArticuloViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Crear una nueva instancia de la entidad Articulos
            var nuevoArticulo = new Articulos
            {
                codigoArticulo = model.codigoArticulo,
                nombreArticulo = model.nombreArticulo,
                precioArticulo = model.precioArticulo,
                stock = model.stock,
                descripcionArticulo = model.descripcionArticulo,
                Estado = model.Estado,
                IdCategorias = model.IdCategorias // Asignar la categoría por medio del ID
            };

            // Agregar el nuevo artículo al contexto de la base de datos
            _context.Articulos.Add(nuevoArticulo);

            try
            {
                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        #endregion

        #region PUT: api/Articulos/ModificarArticulos
        [HttpPut("[action]")]
        public async Task<IActionResult> ModificarArticulos([FromBody] ModificarArticuloViewModel modelArticulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (modelArticulo.IdArticulos < 0)
            {
                return BadRequest(ModelState);
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.IdArticulos == modelArticulo.IdArticulos);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.IdCategorias = modelArticulo.IdCategorias;
            articulo.codigoArticulo = modelArticulo.codigoArticulo;
            articulo.nombreArticulo = modelArticulo.nombreArticulo;
            articulo.precioArticulo = modelArticulo.precioArticulo;
            articulo.stock = modelArticulo.Stock;
            articulo.descripcionArticulo = modelArticulo.descripcionArticulo;
            articulo.Estado = modelArticulo.estado;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok();
        }
        #endregion

        #region GET: api/Articulos/ObtenerArticulo/
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<ArticulosViewModel>> ObtenerArticuloPorId(int id)
        {
            // Buscar el artículo en la base de datos por su ID
            var articulo = await _context.Articulos
                .Include(a => a.Categoria) // Cargar la propiedad de navegación Categoria
                .FirstOrDefaultAsync(a => a.IdArticulos == id);

            if (articulo == null)
            {
                return NotFound(); // Devolver 404 si no se encuentra el artículo
            }

            // Mapear el artículo a un ArticulosViewModel
            var articuloViewModel = new ArticulosViewModel
            {
                IdArticulos = articulo.IdArticulos,
                codigoArticulo = articulo.codigoArticulo,
                nombreArticulo = articulo.nombreArticulo,
                precioArticulo = articulo.precioArticulo,
                stock = articulo.stock,
                descripcionArticulo = articulo.descripcionArticulo,
                IdCategorias = articulo.IdCategorias,
                nombreCategoria = articulo.Categoria != null ? articulo.Categoria.nombreCategorias : string.Empty
            };

            return articuloViewModel;
        }


        #endregion

        #region Desactivar Categoria
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> DesactivarArticulos([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.IdArticulos == id);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.Estado = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }
        #endregion

        #region ActivarArticulos
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> ActivarArticulos([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.IdArticulos == id);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.Estado = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }
        #endregion

        #region DELETE: api/Articulos/BorrarArticulo
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> BorrarArticulo(int id)
        {
            if (_context.Articulos == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos.FindAsync(id);
            if (articulo == null)
            {
                return NotFound();
            }

            _context.Articulos.Remove(articulo);

            try
            {
                await _context.SaveChangesAsync();
                return NoContent(); // 204 No Content indica que la eliminación fue exitosa
            }
            catch (Exception)
            {
                return BadRequest(); // 400 Bad Request en caso de error
            }
        }

        #endregion
    }
}
