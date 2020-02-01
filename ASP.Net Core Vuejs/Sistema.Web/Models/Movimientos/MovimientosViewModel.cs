using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Movimientos
{
    public class MovimientosViewModel
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public int idrol { get; set; }
        public DateTime fecha { get; set; }
        public int entregas { get; set; }
    }
}
