using System.ComponentModel.DataAnnotations;
namespace Sistema.Entidades.Trabajador
{
    public class Trabajador
    {
        public int id { get; set; }
        [Required]
        [StringLength(100,MinimumLength =3,ErrorMessage ="El nombre debe ser de 3 a 100 caracteres")]
        public string nombre { get; set; }
        public int idrol { get; set; } 
        public int idtipo { get; set; }
        public bool activo { get; set; }
    }

    public class vw_trabajadores
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int idrol { get; set; }
        public string rol { get; set; }
        public int idtipo { get; set; }
        public string tipo { get; set; }
        public bool activo { get; set; }
    }
}
