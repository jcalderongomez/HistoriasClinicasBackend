using HistoriasClinicas.Models.Modelos;
using HistoriasClinicas.Models.Modelos.Dto;

namespace HistoriasClinicas.DataAccess.Repositorio.Interfaces
{
    public interface IPacienteRepositorio : IRepositorio<Paciente>
    {
        void Actualizar(Paciente paciente);
    }
}
