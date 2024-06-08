
using AutoMapper;
using HistoriasClinicas.Models.Modelos;
using HistoriasClinicas.Models.Modelos.Dto;

namespace HistoriasClinicas.API.Helpers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Eps, EpsDto>().ReverseMap();
            CreateMap<Paciente, PacienteUpsertDto>().ReverseMap();

            CreateMap<Paciente, PacienteReadDto>()
                .ForMember(p => p.Eps, m => m.MapFrom(e => e.Eps.NombreEPS));
            
            CreateMap<Especialidad, EspecialidadDto>().ReverseMap();
        }
            
    }
}
