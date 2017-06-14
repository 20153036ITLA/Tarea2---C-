using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyApp.Models
{
    public class Prestamos
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public int Cantidad { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public int Pagos { get; set; }
        [Required(ErrorMessage="Este campo es requerido")]
        public String Motivo { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public String Cedula { get; set; }
    }
}