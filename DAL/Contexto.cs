using Microsoft.EntityFrameworkCore;
using rDetalle.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace rDetalle.DAL
{
   public  class Contexto :DbContext
    {
     public DbSet<Personas> Personas { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = Personas.db");
        }
    }
}
