using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Registros;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Movimientos
{
    public class MovimientosMap : IEntityTypeConfiguration<Movimiento>
    {
        public void Configure(EntityTypeBuilder<Movimiento> builder)
        {
            builder.ToTable("movimientos")
                .HasKey(t => t.id);
        }
    }
}
