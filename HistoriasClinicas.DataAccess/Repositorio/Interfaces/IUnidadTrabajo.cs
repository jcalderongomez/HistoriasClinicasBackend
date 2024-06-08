using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoriasClinicas.DataAccess.Repositorio.Interfaces
{
    public interface IUnidadTrabajo: IDisposable
    {
        IPacienteRepositorio Paciente { get; }
        IEpsRepositorio Eps { get; }
        IEspecialidadRepositorio Especialidad { get; }

        Task Guardar();
    }
}
