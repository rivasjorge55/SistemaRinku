using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Entidades.Nominas
{
    public class Nomina
    {
        public int idtrabajador { get; set; }
        public string nombre { get; set; }
        public decimal Isalario { get; set; }
        public decimal Ibono { get; set; }
        public decimal Ientregas { get; set; }
        public decimal Ivales { get; set; }
        public decimal ISR { get; set; }
        public decimal neto { get; set; }
    }
}
