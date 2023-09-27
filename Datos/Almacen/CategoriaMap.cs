using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Almacen;

namespace Datos.Mapping.Almacen
{
    public class CategoriaMap : IEntityTypeConfiguration<Categorias>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Categorias> builder)
        //builder = es una instancia del IEntityTypeConfiguration
        {
            builder.ToTable("Categorias").HasKey(t => t.IdCategorias);
            builder.Property(t => t.nombreCategorias).HasMaxLength(100);
            builder.Property(t => t.Descripcion).HasMaxLength(250);
            builder.Property(t => t.Estado).HasDefaultValue(false);
        }
    }
}
