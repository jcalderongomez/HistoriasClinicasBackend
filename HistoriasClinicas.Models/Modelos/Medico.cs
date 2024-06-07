using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HistoriasClinicas.Models.Modelos
{
    public class Medico
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombres es Requerido")]
        [MaxLength(60, ErrorMessage = "Nombres debe ser Maximo 60 caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Apellidos es Requerido")]
        [MaxLength(60, ErrorMessage = "Apellidos debe ser Maximo 60 caracteres")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Direccion es Requerido")]
        [MaxLength(100, ErrorMessage = "Direccion debe ser Maximo 100 caracteres")]
        public string Direccion { get; set; }
        
        [Required(ErrorMessage = "Teléfono es Requerido")]
        [MaxLength(20, ErrorMessage = "Teléfono debe ser Maximo 20 caracteres")]
        public string Telefono { get; set;}

        [Required]
        public int EspecialidadId { get; set;}
        
        [ForeignKey("EspecialidadId ")]
        public Especialidad Especialidad { get; set;}
        public bool Estado { get; set; } = true;
    }
}
