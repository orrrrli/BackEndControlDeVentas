using Entidades.Usuarios;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Ventas;

namespace Datos.Ventas
{
    public class PersonasMap : IEntityTypeConfiguration<Personas>
    {
        public void Configure(EntityTypeBuilder<Personas> builder)
        {
            builder.ToTable("Personas").HasKey(t => t.IdPersona);
            builder.Property(t => t.TipoPersona).HasMaxLength(20);
            builder.Property(t => t.NombrePersona).HasMaxLength(150);
            builder.Property(t => t.TipoDocumento).HasMaxLength(20);
            builder.Property(t => t.NumeroDocumento).HasMaxLength(20);
            builder.Property(t => t.DireccionPersona).HasMaxLength(150);
            builder.Property(t => t.TelefonoPersona).HasMaxLength(14);
            builder.Property(t => t.EmailPersona).HasMaxLength(150);
        }
    }
}
