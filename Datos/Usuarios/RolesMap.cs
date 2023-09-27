using Entidades.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Usuarios
{
    public class RolesMap : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            builder.ToTable("Roles").HasKey(c => c.IdRol);
            builder.Property( c=> c.nombreRol).HasMaxLength(30);
            builder.Property(c=> c.descripcionRol).HasMaxLength(100);
            builder.Property(c => c.estado).HasDefaultValue(false);
        }
    }
}
