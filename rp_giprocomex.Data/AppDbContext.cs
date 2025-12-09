using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using rp_giprocomex.Core.Models;

namespace rp_giprocomex.Data
{
    // Hereda de IdentityDbContext para incluir IdentityUser + IdentityRole en el modelo
    public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Muy importante: llamar a la implementación base para que EF agregue las tablas AspNet*
            base.OnModelCreating(builder);

            // Aquí puedes añadir configuraciones adicionales para Employee si quieres
            // builder.Entity<Employee>().Property(e => e.NombreCompleto).HasMaxLength(300).IsRequired();
        }
    }
}