using Datos.Mapping.Almacen;
using Datos.Usuarios;
using Entidades.Almacen;
using Entidades.Usuarios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Datos
{
    

    public class BDContext: DbContext
    {
        public DbSet<Categorias> Categorias { get; set; } = null;
        public DbSet<Roles> Roles { get; set; } = null; 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Conexion");
            }
        }
        public BDContext()
        {

        }
        public BDContext(DbContextOptions<BDContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new RolesMap());

        }


    }

}
