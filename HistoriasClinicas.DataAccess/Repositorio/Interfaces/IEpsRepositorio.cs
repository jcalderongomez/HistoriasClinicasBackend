using HistoriasClinicas.Models.Modelos;
using HistoriasClinicas.Models.Modelos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoriasClinicas.DataAccess.Repositorio.Interfaces
{
    public interface IEpsRepositorio : IRepositorio<Eps>
    {
        void Actualizar(Eps seps);
    }
}
