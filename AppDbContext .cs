using Microsoft.EntityFrameworkCore;

namespace MiAplicacionEF;

public class AppDbContext : DbContext
{
    public DbSet<Producto> Productos => Set<Producto>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=mi_aplicacion_db;Username=postgres;Password=TU_CONTRASEÑA");
    }
}