using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Trabajador;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Trabajadores
{
    public class TrabajadorMap : IEntityTypeConfiguration<Trabajador>
    {
        public void Configure(EntityTypeBuilder<Trabajador> builder)
        {
            builder.ToTable("trabajadores")
                .HasKey(t => t.id);
            builder.Property(c => c.nombre)
                 .HasMaxLength(100);
        }
    }
}
