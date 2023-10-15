using Entidades.Almacen;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Usuarios;

namespace Datos.Usuarios
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios").HasKey(t => t.IdUsuario);
            builder.Property(t => t.NombreUsuario).HasMaxLength(150);
            builder.Property(t => t.TipoDocumento).HasMaxLength(20);
            builder.Property(t => t.NumeroDocumento).HasMaxLength(20);
            builder.Property(t => t.Direccion).HasMaxLength(150);
            builder.Property(t => t.Telefono).HasMaxLength(14);
            builder.Property(t => t.Email).HasMaxLength(150);
            builder.Property(t => t.PasswordHash);
            builder.Property(t => t.PasswordSalt);
            builder.Property(t => t.Estado).HasDefaultValue(true);

            // Configuración de la clave foránea 
            builder.Property(t => t.IdRol);
            builder.HasOne(t => t.Rol)
                   .WithMany()
                   .HasForeignKey(t => t.IdRol)
                   .OnDelete(DeleteBehavior.Restrict); // Opcional: define la acción de eliminación
        }
    }
}
