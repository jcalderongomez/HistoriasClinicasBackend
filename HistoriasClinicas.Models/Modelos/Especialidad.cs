using System.ComponentModel.DataAnnotations;

namespace HistoriasClinicas.Models.Modelos
{
    public class Especialidad
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "NombreEspecialidad es Requerido")]
        [MaxLength(100, ErrorMessage = "NombreEspecialidad  debe ser Maximo 100 caracteres")]
        public string NombreEspecialidad { get; set; }
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El ESTADO es requerido")]
        public bool Estado { get; set; } = true;
    }
}
