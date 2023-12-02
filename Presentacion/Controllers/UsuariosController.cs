using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Datos;
using Entidades.Usuarios;
using Presentacion.Models.Usuarios.Usuarios;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly BDContext _context;
        private readonly IConfiguration _configuracion;

        public UsuariosController(BDContext context, IConfiguration configuracion)
        {
            _context = context;
            _configuracion = configuracion;
        }

        #region GET: api/Usuarios/ListarUsusarios
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<UsuarioViewModel>>> ListarUsuarios()
        {
            var usuarios = await _context.Usuario
                .Include(u => u.Rol) // Incluye la relación con la entidad Rol
                .Select(u => new UsuarioViewModel
                {
                    IdUsuario = u.IdUsuario,
                    IdRol = u.IdRol,
                    NombreUsuario = u.NombreUsuario,
                    TipoDocumento = u.TipoDocumento,
                    NumeroDocumento = u.NumeroDocumento,
                    Direccion = u.Direccion,
                    Telefono = u.Telefono,
                    Email = u.Email,
                    PasswordHash = u.PasswordHash,
                    PasswordSalt = u.PasswordSalt,
                    Estado = u.Estado,
                    nombreRol = u.Rol.nombreRol // Agrega el nombre del rol al ViewModel
                })
                .ToListAsync();

            if (usuarios == null)
            {
                return NotFound();
            }

            return usuarios;
        }
        #endregion

        #region POST: api/Usuarios/InsertarUsuario

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("[action]")]
        public async Task<ActionResult> InsertarUsuario(InsertarUsuarioViewModel modelusuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.Usuario == null)
            {
                return Problem("Entity set BdContextSistema.Usuarios is null");
            }
            CreaPasswordHash(modelusuario.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var email = modelusuario.Email.ToUpper();
            if (await _context.Usuario.AnyAsync(u => u.Email == email))
            {
                return BadRequest("El email del usuario ya existe");
            }

            Usuario usuario = new()
            {
                IdRol = modelusuario.IdRol,
                NombreUsuario = modelusuario.NombreUsuario,
                TipoDocumento = modelusuario.TipoDocumento,
                NumeroDocumento = modelusuario.NumeroDocumento,
                Direccion = modelusuario.Direccion,
                Telefono = modelusuario.Telefono,
                Email = modelusuario.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Estado = true
            };

            _context.Usuario.Add(usuario);
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

        #region PUT: api/Usuarios/ModificarUsuarios
        [HttpPut("[action]")]
        public async Task<ActionResult> ModificarUsuario(ModificarUsuarioViewModel modelusuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (modelusuario.IdUsuario < 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.IdUsuario == modelusuario.IdUsuario);

            if (usuario == null)
            {
                return NotFound();
            }

            var email = modelusuario.Email;

            if (await _context.Usuario.AnyAsync(u => u.Email == email && u.IdUsuario != modelusuario.IdUsuario))
            {
                return BadRequest("El Email de este usuario ya existe");
            }

            usuario.IdRol = modelusuario.IdRol;
            usuario.NombreUsuario = modelusuario.NombreUsuario;
            usuario.TipoDocumento = modelusuario.TipoDocumento;
            usuario.NumeroDocumento = modelusuario.NumeroDocumento;
            usuario.Direccion = modelusuario.Direccion;
            usuario.Telefono = modelusuario.Telefono;
            usuario.Email = email; // Asigna el valor correcto del modelo
            

            if (modelusuario.actualizaPassword == true )
            {
                CreaPasswordHash(modelusuario.Password, out byte[] passwordHash, out byte[] passwordSalt);
                usuario.PasswordHash = passwordHash;
                usuario.PasswordSalt = passwordSalt;
            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
        }



        #endregion

        #region DELETE: api/Usuarios/BorrarUsuarios
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> BorrarUsuarios(int id)
        {
            if (_context.Usuario == null)
            {
                return NotFound();
            }
            var e_Usuarios = await _context.Usuario.FindAsync(id);
            if (e_Usuarios == null)
            {
                return NotFound();
            }

            _context.Usuario.Remove(e_Usuarios);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region PUT: api/Usuarios/DesactivarUsuarios
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> DesactivarUsuarios([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Estado = false;

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


        #region PUT: api/Usuarios/ActivarUsuarios
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> ActivarUsuarios([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Estado = true;

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

        public static void CreaPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private string GenerarToken (List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuracion["Jwt:Key"]));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuracion["Jnt:Issuer"],
                _configuracion["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials:credenciales,
                claims: claims);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        private bool VerificaPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var nuevoPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return new ReadOnlySpan<byte>(passwordHash).SequenceEqual(new ReadOnlySpan<byte>(nuevoPasswordHash));
            }
        }

        #region POST: api/Usuarios/Login
        [HttpPost("[action]")]

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var email = model.Email.ToUpper(); //En otro codigo lo tengo sin el ToUpper
            var usuario = await _context.Usuario.Where(u => u.Estado == true).Include(u => u.Rol).FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null)
            {
                return NotFound();
            }
            var isValido = VerificaPassword(model.Password, usuario.PasswordHash, usuario.PasswordSalt);
            if (!isValido)
            {
                return BadRequest();
            }
            var claim = new List<Claim>
            {
                //Para backend
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim (ClaimTypes.Role, usuario.Rol.nombreRol),

                //FrontEnd
                new Claim ("IdUsuario", usuario.IdUsuario.ToString()),
                new Claim ("Rol", usuario.Rol.nombreRol),
                new Claim("NombreUsuario", usuario.NombreUsuario)

            };

            return Ok(
                new { token = GenerarToken(claim) }
                );
        }

        #endregion

    }

}
