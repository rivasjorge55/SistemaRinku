using Microsoft.EntityFrameworkCore;
using Sistema.Datos.Mapping.Trabajadores;
using Sistema.Datos.Movimientos;
using Sistema.Datos.Mapping.Nominas;
using Sistema.Entidades.Registros;
using Sistema.Entidades.Trabajador;
using Sistema.Entidades.Nominas;

using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos
{
    public class DbContextSistema : DbContext
    {
        public DbSet<Trabajador> Trabajadores { get; set; }
        public DbQuery<vw_trabajadores> vw_Trabajadores { get; set; }

        public DbSet<Movimiento> Movimientos { get; set; }
        public DbQuery<vw_movimientos> vw_movimientos { get; set; }

        //public DbSet<Nomina> nominas { get; set; }
        //public List<Nomina> nominas { get; set; }
        //public DbSet<Nomina> Nominas { get; set; }


        public DbContextSistema(DbContextOptions<DbContextSistema> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TrabajadorMap());
            modelBuilder.Query<vw_trabajadores>().ToView("vw_trabajadores").Property(v => v.id).HasColumnName("id");

            modelBuilder.ApplyConfiguration(new MovimientosMap());
            modelBuilder.Query<vw_movimientos>().ToView("vw_movimientos").Property(v => v.id).HasColumnName("id");

            modelBuilder.Query<Nomina>();
            //modelBuilder.ApplyConfiguration(new NominaMap());
        }

        


    }
}
