using HistoriasClinicas.Models.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HistoriasClinicas.DataAccess
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        {
            
        }
        public DbSet<Paciente> Pacientes{ get; set; }
        public DbSet<Medico> Medicos{ get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Eps> Epss{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
