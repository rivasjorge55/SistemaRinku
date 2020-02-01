using System;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Entidades.Registros
{
    
    public class Movimiento
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public int idrol { get; set; }
        public DateTime fecha { get; set; }
        public int entregas { get; set; }
    }

    public class vw_movimientos
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public string nombre { get; set; }
        public int idrol { get; set; }
        public string rol { get; set; }
        public int idtipo { get; set; }
        public string tipo { get; set; }
        public DateTime fecha { get; set; }
        public int entregas { get; set; }
    }
    
}
