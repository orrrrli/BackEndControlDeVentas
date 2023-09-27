using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Datos;
using Entidades.Almacen;
using Presentacion.Models.Almacen.Categoria;
using Presentacion.Models.Almacen;
using Presentacion.Models;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly BDContext _context;
        public CategoriasController(BDContext context)
        {
            _context = context;
        }

        #region GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categorias>>> GetCategorias()
        {
            if (_context.Categorias == null)
            {
                return NotFound();
            }
            return await _context.Categorias.ToListAsync();
        }
        #endregion

        #region GET: api/Categorias/ListarCategoria
        [HttpGet("[action]")]
        public async Task<IEnumerable<CategoriasViewModel>> ListarCategorias()
        {
            var categoria = await _context.Categorias.ToListAsync();
            return categoria.Select(c => new CategoriasViewModel
            {
                IdCategorias = c.IdCategorias,
                nombreCategorias = c.nombreCategorias,
                Descripcion = c.Descripcion,
                Estado = c.Estado
            });
        }
        #endregion

        #region GET: api/Categorias/ObtenerCategoria/3
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> ÓbteneCategoria([FromRoute] int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(new CategoriasViewModel
            {
                IdCategorias = categoria.IdCategorias,
                nombreCategorias = categoria.nombreCategorias,
                Descripcion = categoria.Descripcion,
                Estado = categoria.Estado
            });
        }
        #endregion

        #region GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categorias>> GetCategoria(int id)
        {
            if (_context.Categorias == null)
            {
                return NotFound();
            }
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }
        #endregion

        #region PUT: api/Categorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, Categorias categoria)
        {
            if (id != categoria.IdCategorias)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        #endregion

        #region POST: api/Categorias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Categorias>> PostCategoria(Categorias categoria)
        {
            if (_context.Categorias == null)
            {
                return Problem("Entity set 'DBContextSistema.Categorias'  is null.");
            }
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoria", new { id = categoria.IdCategorias }, categoria);
        }
        #endregion

        #region DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            if (_context.Categorias == null)
            {
                return NotFound();
            }
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaExists(int id)
        {
            return (_context.Categorias?.Any(e => e.IdCategorias == id)).GetValueOrDefault();
        }
        #endregion

        #region PUT: api/Categorias/5/ModificarCategoria
        // MODIFICAR CATEGORIA//
        [HttpPut("[action]")]
        public async Task<IActionResult> ModificarCategorias([FromBody] ModificarViewModel modelcategoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (modelcategoria.IdCategorias < 0)
            {
                return BadRequest(ModelState);
            }

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.IdCategorias == modelcategoria.IdCategorias);

            if (categoria == null)
            {
                return NotFound();
            }

            categoria.nombreCategorias = modelcategoria.nombreCategorias;
            categoria.Descripcion = modelcategoria.Descripcion;

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

        #region PUT: api/Categorias/5/Insertar

        // INSERTAR CATEGORIA//
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertarCategorias(InsertarViewModel modelcategoria)
        {
            if (!ModelState.IsValid)
            {
                return Problem("Entiry set 'DBContextSistema.Categorias' is null.");
            }

            Categorias categoria = new Categorias
            {
                nombreCategorias = modelcategoria.nombreCategorias,
                Descripcion = modelcategoria.Descripcion,
                Estado = true
            };

            _context.Categorias.Add(categoria);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        #endregion

        #region PUT: api/Categorias/5/Borrar

        // BORRAR CATEGORIA//
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> BorrarCategoria(int id)
        {
            if (_context.Categorias == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return NoContent();
        }

        #endregion

        #region DESACTIVAR CATEGORIA

        // DESACTIVAR CATEGORIA//
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> DesactivarCategoria([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.IdCategorias == id);

            if (categoria == null)
            {
                return NotFound();
            }

            categoria.Estado = false;

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

        #region ACTIVAR CATEGORIA

        // ACTIVAR CATEGORIA//
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> ActivarCategoria([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.IdCategorias == id);

            if (categoria == null)
            {
                return NotFound();
            }

            categoria.Estado = true;

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
    }
}
