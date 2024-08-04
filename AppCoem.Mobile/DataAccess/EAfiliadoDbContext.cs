using AppCoem.Mobile.Modelos;
using AppCoem.Mobile.Utilidades;
using Microsoft.EntityFrameworkCore;

namespace AppCoem.Mobile.DataAccess
{
    public class EAfiliadoDbContext : DbContext
    {
        public DbSet<EAfiliado> EAfiliados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conexionDB = $"Filename={ConexionDB.DevolverRuta("empleados.db")}";
            optionsBuilder.UseSqlite(conexionDB);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EAfiliado>(entity =>
            {
                entity.HasKey(col => col.IdAfiliado);
                entity.Property(col => col.IdAfiliado).IsRequired().ValueGeneratedOnAdd();
            });
            //using AppCoem.Mobile.DataAccess;
        }
    }
}
