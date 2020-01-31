using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Trabajadores
{
    public class TrabajadorViewModel
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int idrol { get; set; }
        public int idtipo { get; set; }
        public bool activo { get; set; }
    }
}
