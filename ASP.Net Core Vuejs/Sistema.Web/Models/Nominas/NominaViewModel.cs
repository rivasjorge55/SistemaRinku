using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Nominas
{
    public class NominaViewModel
    {
        public int idtrabajador { get; set; }
        public string nombre { get; set; }
        public int tipo { get; set; }
        public int rol { get; set; }
        public decimal salario { get; set; }
        public decimal bono { get; set; }
        public decimal porcentajevale { get; set; }
        public int dias_trabajados { get; set; }
        public int entregas { get; set; }
        public decimal Isalario { get; set; }
        public decimal Ibonos { get; set; }
        public decimal Ientregas { get; set; }
        public decimal Ivales { get; set; }
        public decimal ISR { get; set; }
        public decimal neto { get; set; }
    }
}
