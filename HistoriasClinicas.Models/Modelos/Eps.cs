using System.ComponentModel.DataAnnotations;

namespace HistoriasClinicas.Models.Modelos
{
    public class Eps
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre EPS es Requerido")]
        [MaxLength(100, ErrorMessage = "Nombre EPS debe ser Maximo 100 caracteres")]
        public string NombreEPS { get; set; }
        public bool Estado { get; set; } = true;

    }
}
