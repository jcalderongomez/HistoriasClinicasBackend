
namespace HistoriasClinicas.Models.Modelos.Dto
{
    public class PacienteUpsertDto
    {
        public int Id { get; set; }
      
        public string Nombres { get; set; }
       
        public string Apellidos { get; set; }
      
        public string Direccion { get; set; }
      
        public string Telefono { get; set; }
        public int EpsId { get; set; }
    }
}
