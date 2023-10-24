using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Datos;
using Entidades.Usuarios;
using Presentacion.Models.Usuarios.Roles;
using Entidades.Almacen;
using Presentacion.Models.Almacen.Categoria;
using Datos.Usuarios;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly BDContext _context;

        public RolesController(BDContext context)
        {
            _context = context;
        }

        #region SELECCION ROL
        [HttpGet("[action]")]
        public async Task<IEnumerable<SeleccionaRolViewModel>> SeleccionarRol()
        {
            var rol = await _context.Roles.Where(a => a.estado == true).ToArrayAsync();
            return rol.Select(c => new SeleccionaRolViewModel
            {
                IdRol = c.IdRol,
                NombreRol = c.nombreRol,
            });
        }
        #endregion
        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roles>>> GetRoles()
        {
          if (_context.Roles == null)
          {
              return NotFound();
          }
            return await _context.Roles.ToListAsync();
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Roles>> GetRoles(int id)
        {
          if (_context.Roles == null)
          {
              return NotFound();
          }
            var roles = await _context.Roles.FindAsync(id);

            if (roles == null)
            {
                return NotFound();
            }

            return roles;
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoles(int id, Roles roles)
        {
            if (id != roles.IdRol)
            {
                return BadRequest();
            }

            _context.Entry(roles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolesExists(id))
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

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Roles>> PostRoles(Roles roles)
        {
          if (_context.Roles == null)
          {
              return Problem("Entity set 'BDContext.Roles'  is null.");
          }
            _context.Roles.Add(roles);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoles", new { id = roles.IdRol }, roles);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoles(int id)
        {
            if (_context.Roles == null)
            {
                return NotFound();
            }
            var roles = await _context.Roles.FindAsync(id);
            if (roles == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(roles);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RolesExists(int id)
        {
            return (_context.Roles?.Any(e => e.IdRol == id)).GetValueOrDefault();
        }

        #region GET: api/Categorias/ListarCategoria
        [HttpGet("[action]")]
        public async Task<IEnumerable<RolesViewModel>> ListarRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            return roles.Select(c => new RolesViewModel
            {
                IdRol = c.IdRol,
                nombreRol = c.nombreRol,
                descripcionRol = c.descripcionRol,
                estado = c.estado
            });
        }
        #endregion


        #region GET: api/Roless/ObtenerRol/2
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> ÓbtenerRol([FromRoute] int id)
        {
            var roles = await _context.Roles.FindAsync(id);

            if (roles == null)
            {
                return NotFound();
            }

            return Ok(new RolesViewModel
            {
                IdRol = roles.IdRol,
                nombreRol = roles.nombreRol,
                descripcionRol = roles.descripcionRol,
                estado = roles.estado
            });
        }
        #endregion

        #region PUT: api/Roles/5/Rol
        // MODIFICAR CATEGORIA//
        [HttpPut("[action]")]
        public async Task<IActionResult> ModificarRol([FromBody] ModificarRolViewModel modelRoles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (modelRoles.IdRol < 0)
            {
                return BadRequest(ModelState);
            }

            var roles = await _context.Roles.FirstOrDefaultAsync(c => c.IdRol == modelRoles.IdRol);

            if (roles == null)
            {
                return NotFound();
            }

            roles.nombreRol = modelRoles.nombreRol;
            roles.descripcionRol = modelRoles.descripcionRol;

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

        #region PUT: api/Roles/5/Insertar

        // INSERTAR CATEGORIA//
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertarRoles(InsertarRolViewModel modelRoles)
        {
            if (!ModelState.IsValid)
            {
                return Problem("Entiry set 'DBContextSistema.Roles' is null.");
            }

            Roles roles = new Roles
            {
                nombreRol = modelRoles.nombreRol,
                descripcionRol = modelRoles.descripcionRol,
                estado = true
            };

            _context.Roles.Add(roles);

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

        #region PUT: api/Roles/5/Borrar

        // BORRAR CATEGORIA//
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> BorrarRoles(int id)
        {
            if (_context.Roles == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles.FindAsync(id);
            if (roles == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(roles);

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
        #region DESACTIVAR ROL

        // DESACTIVAR ROL//
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> DesactivarRol([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var roles = await _context.Roles.FirstOrDefaultAsync(c => c.IdRol == id);

            if (roles == null)
            {
                return NotFound();
            }

            roles.estado = false;

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

        #region ACTIVAR ROLES

        // ACTIVAR ROLES//
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> ActivarRol([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var roles = await _context.Roles.FirstOrDefaultAsync(c => c.IdRol == id);

            if (roles == null)
            {
                return NotFound();
            }

            roles.estado = true;

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
