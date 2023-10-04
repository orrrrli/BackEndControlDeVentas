using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entidades.Almacen;

namespace Datos.Mapping.Almacen
{
    public class ArticuloMap : IEntityTypeConfiguration<Articulos>
    {
        public void Configure(EntityTypeBuilder<Articulos> builder)
        {
            builder.ToTable("Articulos").HasKey(t => t.IdArticulos);
            builder.Property(t => t.codigoArticulo);
            builder.Property(t => t.nombreArticulo);
            builder.Property(t => t.precioArticulo);
            builder.Property(t => t.descripcionArticulo);
            builder.Property(t => t.Estado);

            // Configuración de la clave foránea IdCategoria
            builder.Property(t => t.IdCategoria);
            builder.HasOne(t => t.Categoria)
                   .WithMany()
                   .HasForeignKey(t => t.IdCategoria)
                   .OnDelete(DeleteBehavior.Restrict); // Opcional: define la acción de eliminación
        }
    }
}
