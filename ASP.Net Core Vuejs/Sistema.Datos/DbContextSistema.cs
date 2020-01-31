using Microsoft.EntityFrameworkCore;
using Sistema.Datos.Mapping.Trabajadores;
using Sistema.Entidades.Trabajador;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos
{
    public class DbContextSistema : DbContext
    {
        public DbSet<Trabajador> Trabajadores { get; set; }
        public DbContextSistema(DbContextOptions<DbContextSistema> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TrabajadorMap());
        }
    }
}
