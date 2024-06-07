using HistoriasClinicas.Models.Modelos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoriasClinicas.DataAccess.Configuracion
{
    public class PacienteConfiguracion : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Nombres).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Apellidos).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Direccion).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Telefono).IsRequired(false).HasMaxLength(40);
            builder.Property(x => x.EpsId).IsRequired();
            builder.Property(x => x.Estado).IsRequired();

            /* Relaciones */
            builder.HasOne(x => x.Eps).WithMany()
                .HasForeignKey(x => x.EpsId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
