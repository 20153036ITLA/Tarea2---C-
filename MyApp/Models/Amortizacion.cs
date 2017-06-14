using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyApp.Models
{
    public class Amortizacion
    {
        public int Id { get; set; }
        public double montoInicial { get; set; }
        public int Pagos {get; set; }
        public double Interes { get; set; }
        public double Amort { get; set; }
        public double Cuota { get; set; }
        public double montoFinal { get; set; }
        public String Cedula { get; set; }
        public int Idref { get; set; }
    }
}