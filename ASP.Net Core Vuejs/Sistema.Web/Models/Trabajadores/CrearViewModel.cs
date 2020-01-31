using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Trabajadores
{
    public class CrearViewModel
    {
        
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe ser de 3 a 100 caracteres")]
        public string nombre { get; set; }
        [Required]
        public int idrol { get; set; }
        [Required]
        public int idtipo { get; set; }
    }
}
