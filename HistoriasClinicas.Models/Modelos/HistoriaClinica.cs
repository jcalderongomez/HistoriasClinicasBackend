using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoriasClinicas.Models.Modelos
{
    public class HistoriaClinica
    {
        [Key]
        public int Id{ get; set; }

        public  int PacienteId { get; set; }
        public  int MedicoId { get; set; }

        [Required(ErrorMessage = "Ei diagnóstico es requerido")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = " EL DIAGNÓSTICOdebe ser mínimo 1 máximo 100 caracteres")]

        public string Diagnostico { get; set; }

        [ForeignKey("PacienteId")]
        public Paciente Paciente { get; set; }
        
        [ForeignKey("MedicoId")]
        public Medico Medico { get; set; }

        [Required(ErrorMessage = "El ESTADO es requerido")]
        public bool Estado { get; set; } = true;
    }
}
