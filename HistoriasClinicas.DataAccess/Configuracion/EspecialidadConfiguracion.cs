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
    public class EspecialidadConfiguracion : IEntityTypeConfiguration<Especialidad>
    {
        public void Configure(EntityTypeBuilder<Especialidad> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.NombreEspecialidad).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Descripcion).IsRequired().HasMaxLength(60);
        }
    }
}
