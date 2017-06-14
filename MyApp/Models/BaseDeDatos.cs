using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyApp.Models
{
    public class BaseDeDatos : DbContext
    {
        public BaseDeDatos() :base("DefaultConnection")
        {

        }
        public DbSet<Amortizacion> amortizacion { get; set; }
        public DbSet<Prestamos> prestamos {get; set;}
        public DbSet<Clientes>  clientes {get; set;}
    }
}