using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Datos;
using Entidades.Ventas;
using Entidades.Usuarios;
using Presentacion.Models.Ventas.Clientes;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly BDContext _context;

        public PersonasController(BDContext context)
        {
            _context = context;
        }

        #region Listar. GET: api/Personas/ListarClientes
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonaViewModel>> ListarCliente()
        {
            var persona = await _context.Personas.Where(p => p.TipoPersona == "Cliente").ToListAsync();
            return persona.Select(p => new PersonaViewModel
            {
                IdPersona = p.IdPersona,
                TipoPersona = p.TipoPersona,
                NombrePersona = p.NombrePersona,
                TipoDocumento = p.TipoDocumento,
                NumeroDocumento = p.NumeroDocumento,
                DireccionPersona = p.DireccionPersona,
                TelefonoPersona = p.TelefonoPersona,
                EmailPersona = p.EmailPersona
            });

        }
        #endregion

        #region Listar. GET: api/Personas/ListarClientes
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonaViewModel>> ListarProveedor()
        {
            var persona = await _context.Personas.Where(p => p.TipoPersona == "Proveedor").ToListAsync();
            return persona.Select(p => new PersonaViewModel
            {
                IdPersona = p.IdPersona,
                TipoPersona = p.TipoPersona,
                NombrePersona = p.NombrePersona,
                TipoDocumento = p.TipoDocumento,
                NumeroDocumento = p.NumeroDocumento,
                DireccionPersona = p.DireccionPersona,
                TelefonoPersona = p.TelefonoPersona,
                EmailPersona = p.EmailPersona
            });

        }
        #endregion

        #region POST: api/Personas/InsertarPersonas

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("[action]")]
        public async Task<ActionResult> InsertarPersonas(InsertarPersonaViewModel modelPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.Personas == null)
            {
                return Problem("Entity set BdContextSistema.Personas is null");
            }

            var email = modelPersona.EmailPersona.ToUpper();
            if (await _context.Personas.AnyAsync(u => u.EmailPersona == email))
            {
                return BadRequest("El email de la persona ya existe");
            }

            Personas personas = new()
            {
                TipoPersona = modelPersona.TipoPersona,
                NombrePersona = modelPersona.NombrePersona,
                TipoDocumento = modelPersona.TipoDocumento,
                NumeroDocumento = modelPersona.NumeroDocumento,
                DireccionPersona = modelPersona.DireccionPersona,
                TelefonoPersona = modelPersona.TelefonoPersona,
                EmailPersona = modelPersona.EmailPersona
            };

            _context.Personas.Add(personas);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                string Error = e.Message;
                var inner = e.InnerException;
                return BadRequest();
            }
            return Ok();

        }

        #endregion
        #region PUT: api/Personas/ModificarPersoans
        [HttpPut("[action]")]
        public async Task<ActionResult> ModificarPersonas(ModificarPersonaViewModel modelPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.Personas == null)
            {
                return Problem("Entity set BdContextSistema.Personas is null");
            }

            var persona = await _context.Personas.FirstOrDefaultAsync(p => p.IdPersona == modelPersona.IdPersona);
            if (persona == null)
            {
                return NotFound();
            }
            var email = modelPersona.EmailPersona;

            if (await _context.Personas.AnyAsync(u => u.EmailPersona == email && u.IdPersona != modelPersona.IdPersona))
            {
                return BadRequest("El Email de esta persona ya existe");
            }

            persona.IdPersona = modelPersona.IdPersona;
                persona.TipoPersona = modelPersona.TipoPersona;
            persona.TipoDocumento = modelPersona.TipoDocumento;
                persona.NombrePersona = modelPersona.NombrePersona;
                persona.NumeroDocumento = modelPersona.NumeroDocumento;
                persona.DireccionPersona = modelPersona.DireccionPersona;
                persona.TelefonoPersona = modelPersona.TelefonoPersona;
                persona.EmailPersona = modelPersona.EmailPersona;
   
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                string Error = e.Message;
                var inner = e.InnerException;
                return BadRequest();
            }
            return Ok();

        }

#endregion
    }
}
