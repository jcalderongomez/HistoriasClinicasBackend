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
    public class EpsConfiguracion : IEntityTypeConfiguration<Eps>
    {
        public void Configure(EntityTypeBuilder<Eps> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            
            builder.Property(x => x.NombreEPS).IsRequired().HasMaxLength(60);
            
            builder.Property(x => x.Estado).IsRequired();
        }
    }
}
