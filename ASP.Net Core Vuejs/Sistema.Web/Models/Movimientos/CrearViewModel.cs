using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Movimientos
{
    public class CrearViewModel
    {
        [Required]
        public int idtrabajador { get; set; }
        [Required]
        public int idrol { get; set; }
        [Required]
        public DateTime fecha { get; set; }
        [Required]
        [Range(typeof(int),"0","9999")]
        public int entregas { get; set; }
    }
}
