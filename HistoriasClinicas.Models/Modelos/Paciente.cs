using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HistoriasClinicas.Models.Modelos
{
    public class Paciente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre es Requerido")]
        [MaxLength(60, ErrorMessage = "Nombre debe ser Maximo 60 caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Apellido es Requerido")]
        [MaxLength(60, ErrorMessage = "Apellido debe ser Maximo 60 caracteres")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Dirección es Requerido")]
        [MaxLength(60, ErrorMessage = "Dirección debe ser Maximo 60 caracteres")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Teléfono es Requerido")]
        [MaxLength(20, ErrorMessage = "Teléfono debe ser Maximo 20 caracteres")]
        public string Telefono { get; set; }

        public int EpsId { get; set; }
        
        [ForeignKey("EpsId")]
        public Eps Eps { get; set; }

        [Required(ErrorMessage = "El ESTADO es requerido")]
        public bool Estado { get; set; } = true;
    }
}
