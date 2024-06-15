using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HistoriasClinicas.Models.Modelos
{
    public class Medico
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombres es Requerido")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Nombres debe ser Maximo 60 caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Apellidos es Requerido")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Apellidos debe ser Maximo 60 caracteres")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Direccion es Requerido")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Direccion debe ser Maximo 100 caracteres")]
        public string Direccion { get; set; }
        
        [Required(ErrorMessage = "Teléfono es Requerido")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Teléfono debe ser Maximo 20 caracteres")]
        public string Telefono { get; set;}

        [Required]
        public int EspecialidadId { get; set;}
        
        [ForeignKey("EspecialidadId ")]
        public Especialidad Especialidad { get; set;}

        [Required(ErrorMessage = "El ESTADO es requerido")]
        public bool Estado { get; set; } = true;
    }
}
